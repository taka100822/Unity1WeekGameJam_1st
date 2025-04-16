using UnityEngine;
using System;
using System.Collections;

public class PlayerMoveController : MonoBehaviour
{
    private CharacterController characterController;
    private Transform cameraTransform;
    private Animator animator;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] private GameObject headHeart; // ← ハート型オブジェクト
    public float shrinkDuration = 2f;  // 縮小する時間
    public Vector3 originalScale;  // 初期のスケール
    private bool isHeart = false;

    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();

        animator.SetBool("Idle",true);

        // カメラのX軸回転（上下）を初期値として保存
        cameraPitch = cameraTransform.localEulerAngles.x;

        // カーソルを画面中央にロック
        Cursor.lockState = CursorLockMode.Locked;
        // headHeart.SetActive(true);
        StartCoroutine(ShrinkHeart());
    }

    void Update()
    {
        RotateView();
        MovePlayer();
    }

    void RotateView()
    {
        // マウスの入力を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // キャラの左右回転
        transform.Rotate(Vector3.up * mouseX);

        // 上下回転（カメラのみ）
        cameraPitch = Mathf.Clamp(cameraPitch, -89f, 89f); // 上下の制限

        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // ←ここを Raw に
        float moveZ = Input.GetAxisRaw("Vertical");   // ←ここも Raw に

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= moveSpeed;
        
        bool isWalking = moveX != 0 || moveZ != 0;
        animator.SetBool("Walk", isWalking);
        animator.SetBool("Idle", !isWalking);

        // 重力処理
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Item"))
        {
            // HeadHeartを表示
            if (headHeart != null)
            {
                headHeart.SetActive(true);
            }

            isHeart = true;
            Destroy(hit.gameObject);
        }
    }

    private IEnumerator ShrinkHeart()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            // スケールを徐々に縮小
            headHeart.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最終的に非表示にする
        headHeart.SetActive(false);
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Death",true);
    }
}
