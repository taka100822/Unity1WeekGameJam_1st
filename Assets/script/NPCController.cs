using UnityEngine;
using UnityEngine.UI;  // UIのTextを使用するため

public class NPCController : MonoBehaviour
{
    public NPCDialogSO npcDialog; // ScriptableObjectで管理されたNPCのセリフ
    public int npcId;
    private Transform player;
    private int nowdialog = 0;
    private bool isdialog = false;

    public float interactionRange = 3f; // プレイヤとの距離で判定

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // プレイヤが範囲内に入ると会話開始
        if (distanceToPlayer < interactionRange)
        {
            isdialog = true;
            player.GetComponent<PlayerInteraction>().SetActiveWindow();
            
            if(nowdialog == 0)
            {
                player.GetComponent<PlayerInteraction>().ShowDialog(npcDialog.npcNum[npcId], nowdialog);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowdialog = 1;
                // プレイヤ側にセリフを渡す
                player.GetComponent<PlayerInteraction>().ShowDialog(npcDialog.npcNum[npcId], nowdialog);
            }
        }
        else if(distanceToPlayer > interactionRange && isdialog)
        {
            isdialog = false;
            player.GetComponent<PlayerInteraction>().EndDialog();
        }
    }
}
