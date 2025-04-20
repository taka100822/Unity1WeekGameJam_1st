using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] private GameObject headHeart; // ← ハート型オブジェクト


    private CharacterController characterController;
    private Transform cameraTransform;
    private Animator animator;
    private Coroutine shrinkCoroutine;
    private float sizeGetsmaller;  // 小さくなるサイズ
    public bool isdead = false;
    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;

    public float ratio; // ハートが小さくなる割合
    public bool isPaused = false;
    public bool isWalking;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        sizeGetsmaller = headHeart.transform.localScale.x * ratio;

        animator.SetBool("Idle",true);

        // カメラのX軸回転（上下）を初期値として保存
        cameraPitch = cameraTransform.localEulerAngles.x;

        // カーソルを画面中央にロック
        Cursor.lockState = CursorLockMode.Locked;
        if (shrinkCoroutine == null)
        {
            shrinkCoroutine = StartCoroutine(ShrinkHeart());
        }
        StartCoroutine(ShrinkHeart());
    }

    void Update()
    {
        if(!isdead && !GetComponent<PlayerInteraction>().isclear)
        {
            RotateView();
            MovePlayer();
        }
        if(Input.GetKeyDown(KeyCode.Space) && isdead)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
        
        isWalking = moveX != 0 || moveZ != 0;

        if(!isdead){
            animator.SetBool("Walk", isWalking);
            animator.SetBool("Idle", !isWalking);
        }

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

            Destroy(hit.gameObject);
        }
    }

    private IEnumerator ShrinkHeart()
    {
        while (true)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            if (headHeart.transform.localScale.x > 0)
            {
                headHeart.transform.localScale -= new Vector3(sizeGetsmaller, sizeGetsmaller, sizeGetsmaller);
            }
            else if(!isdead && !GetComponent<PlayerInteraction>().isclear)
            {
                isdead = true;
                headHeart.SetActive(false);
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Death", true);
                GetComponent<PlayerInteraction>().SetActiveGameOverWindow();
            }
            yield return null;
        }
    }

    public void MakeHeartSmaller(float amount)
    {
        StartCoroutine(ModifyHeartTemporarily(amount));
    }

    private IEnumerator ModifyHeartTemporarily(float amount)
    {
        isPaused = true; // 一時停止

        if (headHeart != null)
        {
            Vector3 currentScale = headHeart.transform.localScale;

            float newX = Mathf.Max(0f, currentScale.x - amount);
            float newY = Mathf.Max(0f, currentScale.y - amount);
            float newZ = Mathf.Max(0f, currentScale.z - amount);

            headHeart.transform.localScale = new Vector3(newX, newY, newZ);

            // 処理が即時完了なら少し待ってから再開
            yield return new WaitForSeconds(0.1f);
        }
        isPaused = false; // 再開
    }
}
