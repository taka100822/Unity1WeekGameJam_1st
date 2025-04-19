using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject startBox;
    [SerializeField] GameObject ruleBox;
    [SerializeField] GameObject ruleWindow;

    private bool isStart = true;

    void Start()
    {
        ruleBox.SetActive(false);
        ruleWindow.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (isStart)
            {
                case true:
                    SceneManager.LoadScene("MainScene");
                    break;

                case false:
                    if(ruleWindow)
                    {
                        ruleWindow.SetActive(false);
                    }
                    else
                    {
                        ruleWindow.SetActive(true);
                    }
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
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
