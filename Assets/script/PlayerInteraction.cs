using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text peopleWantLoveText; // NPCのセリフを表示するUI
    public Text interactionText; // NPCのセリフを表示するUI
    public GameObject talkWindow; // TalkWindow UI
    public GameObject gameOverWindow;
    public GameObject ClearWindow;
    public bool isclear = false;

    private int giveHeartNum = 0;
    public int peopleWantLoveNum = 3; // 愛を欲している人の数
    private NPCDialogSO.NPCDialog currentDialog; // 現在のNPCのセリフ

    void Start()
    {
        // 最初はTalkWindowを非表示にする
        talkWindow.SetActive(false);
        peopleWantLoveText.text = $"残り{peopleWantLoveNum}人";
    }

    public void SetActiveWindow()
    {
        talkWindow.SetActive(true); // TalkWindowを表示
    }

    // NPCが近づいてきたときに呼ばれる関数
    public void ShowDialog(NPCDialogSO.NPCDialog dialog, int nowdialog)
    {
        interactionText.text = dialog.Dialog[nowdialog];
    }

    public void EndDialog()
    {
        talkWindow.SetActive(false);
    }

    public void SetActiveGameOverWindow()
    {
        gameOverWindow.SetActive(true); // TalkWindowを表示
    }

    public void ReducePeopleWantLoveNum(float reduceHeartSize)
    {
        if(reduceHeartSize > 0)
        {
            --peopleWantLoveNum;
            peopleWantLoveText.text = $"残り{peopleWantLoveNum}人";
            if(peopleWantLoveNum == 0)
            {
                isclear = true;
                ClearGame();
            } 
        }
    }

    private void ClearGame()
    {
        ClearWindow.SetActive(true);
        GetComponent<PlayerMoveController>().isPaused = true;
    }
}
