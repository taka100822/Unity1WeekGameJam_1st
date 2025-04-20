using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPCDialogSO npcDialog; // ScriptableObjectで管理されたNPCのセリフ
    public int npcId;
    [SerializeField] ParticleSystem heartPS;

    private Transform player;
    private int nowdialog = 0;
    private float interactionRange = 3f; // プレイヤとの距離で判定
    private bool isdialog = false;
    private bool isheart = false;


    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        GetComponent<Animator>().SetBool("Idle", false);
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // プレイヤが範囲内に入ると会話開始
        if (distanceToPlayer < interactionRange)
        {
            // プレイヤの方を向く
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; // Y軸方向は無視して水平回転のみにする
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // スムーズに回転
            }

            player.GetComponent<PlayerMoveController>().isPaused = true;

            isdialog = true;
            player.GetComponent<PlayerInteraction>().SetActiveWindow();
            
            if(nowdialog == 0)
            {
                player.GetComponent<PlayerInteraction>().ShowDialog(npcDialog.npcNum[npcId], nowdialog);
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && !isheart)
            {
                nowdialog = 1;
                // プレイヤ側にセリフを渡す
                player.GetComponent<PlayerInteraction>().ShowDialog(npcDialog.npcNum[npcId], nowdialog);
                player.GetComponent<PlayerInteraction>().ReducePeopleWantLoveNum(npcDialog.npcNum[npcId].HeartSize);
                isheart = true;
                Instantiate(heartPS, transform.position, Quaternion.identity);
                player.GetComponent<PlayerMoveController>().MakeHeartSmaller(npcDialog.npcNum[npcId].HeartSize);
            }
            else if(isheart)
            {
                player.GetComponent<PlayerInteraction>().ShowDialog(npcDialog.npcNum[npcId], nowdialog);
            }
        }
        else if(distanceToPlayer > interactionRange && isdialog)
        {
            player.GetComponent<PlayerMoveController>().isPaused = false;
            isdialog = false;
            player.GetComponent<PlayerInteraction>().EndDialog();
        }
    }
}
