using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private CharacterController characterController;
    private Transform cameraTransform;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float gravity = 9.8f;

    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

    // カメラのX軸回転（上下）を初期値として保存
    cameraPitch = cameraTransform.localEulerAngles.x;

        // カーソルを画面中央にロック
        Cursor.lockState = CursorLockMode.Locked;
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
}
