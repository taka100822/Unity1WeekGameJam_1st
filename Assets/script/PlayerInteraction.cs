using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject SE;
    public Text peopleWantLoveText; // NPCのセリフを表示するUI
    public Text interactionText; // NPCのセリフを表示するUI
    public GameObject talkWindow; // TalkWindow UI
    public GameObject gameOverWindow;
    public GameObject ClearWindow;
    public bool isclear = false;

    private int giveHeartNum = 0;
    public int peopleWantLoveNum = 3; // 愛を欲している人の数
    private NPCDialogSO.NPCDialog currentDialog; // 現在のNPCのセリフ
    private PlayerMoveController playerMoveController;
    private SEManager seManager;
    public Volume volume;

    void Start()
    {
        // 最初はTalkWindowを非表示にする
        talkWindow.SetActive(false);
        peopleWantLoveText.text = $"残り {peopleWantLoveNum}人";

        playerMoveController = GetComponent<PlayerMoveController>();
        seManager = SE.GetComponent<SEManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (isclear || playerMoveController.isdead))
        {
            seManager.SoundSE("retry");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetActiveWindow()
    {
        talkWindow.SetActive(true); // TalkWindowを表示
    }

    // NPCが近づいてきたときに呼ばれる関数
    public void ShowDialog(NPCDialogSO.NPCDialog dialog, int nowdialog)
    {
        interactionText.text = dialog.Dialog[nowdialog].Replace("\\n", "\n");
    }

    public void EndDialog()
    {
        talkWindow.SetActive(false);
    }

    public void SetActiveGameOverWindow()
    {
        seManager.SoundSE("gameover");
        gameOverWindow.SetActive(true); // TalkWindowを表示
    }

    public void ReducePeopleWantLoveNum(float reduceHeartSize)
    {
        if(reduceHeartSize > 0)
        {
            --peopleWantLoveNum;
            seManager.SoundSE("give");
            peopleWantLoveText.text = $"残り {peopleWantLoveNum}人";
            if(peopleWantLoveNum == 0)
            {
                isclear = true;
                ClearGame();
            } 
        }
        else
        {
            seManager.SoundSE("recieve");
        }
    }

    private void ClearGame()
    {
        ClearWindow.SetActive(true);
        playerMoveController.isPaused = true;
        seManager.SoundSE("clear");
    }
}
