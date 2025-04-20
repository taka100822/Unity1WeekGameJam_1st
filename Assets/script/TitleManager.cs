using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject startBox;
    [SerializeField] GameObject ruleBox;
    [SerializeField] GameObject ruleWindow;
    [SerializeField] GameObject rule1;
    [SerializeField] GameObject rule2;
    private AudioSource audioSource;
    public AudioClip goSE;
    public AudioClip changeSE;
    private bool notInputSpace = false;

    private bool isStart = true;

    void Start()
    {
        ruleBox.SetActive(false);
        ruleWindow.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(goSE);
            switch (isStart)
            {
                case true:
                    SceneManager.LoadScene("MainScene");
                    break;

                case false:
                    if(!rule1.activeSelf && !rule2.activeSelf) //rule1もrule2もどちらも表示されていないなら
                    {
                        notInputSpace = true; // SPACEキーの入力を受け付けないをon
                        ruleWindow.SetActive(true);
                        rule1.SetActive(true);
                    }
                    else if(rule1.activeSelf && !rule2.activeSelf) //rule1は表示されていてrule2が表示されていないなら
                    {
                        ruleWindow.SetActive(true);
                        rule1.SetActive(false);
                        rule2.SetActive(true);
                    }
                    else if(!rule1.activeSelf && rule2.activeSelf) //rule2は表示されていてrule1が表示されていないなら
                    {
                        notInputSpace = false; // SPACEキーの入力を受け付けないをoff
                        rule2.SetActive(false);
                        ruleWindow.SetActive(false);
                    }
                    break;
            }
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.PageUp) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && !notInputSpace)
        {
            audioSource.PlayOneShot(changeSE);
            isStart = !isStart;
            switch (isStart)
            {
                case true:
                    startBox.SetActive(true);
                    ruleBox.SetActive(false);
                    break;

                case false:
                    startBox.SetActive(false);
                    ruleBox.SetActive(true);
                    break;
            }
        }
    }
}
