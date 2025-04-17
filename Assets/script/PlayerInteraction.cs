using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text interactionText; // NPCのセリフを表示するUI
    public GameObject talkWindow; // TalkWindow UI

    private NPCDialogSO.NPCDialog currentDialog; // 現在のNPCのセリフ

    void Start()
    {
        // 最初はTalkWindowを非表示にする
        talkWindow.SetActive(false);
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
}
