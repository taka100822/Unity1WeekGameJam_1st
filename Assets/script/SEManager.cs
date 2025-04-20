using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioClip giveSE;
    public AudioClip recieveSE;
    public AudioClip clearSE;
    public AudioClip gameoverSE;
    public AudioClip retrySE;
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundSE(string state)
    {
        switch (state)
        {
            case "give":
            audioSource.PlayOneShot(giveSE);
            break;
            case "recieve":
            audioSource.PlayOneShot(recieveSE);
            break;
            case "clear":
            audioSource.PlayOneShot(clearSE);
            break;
            case "gameover":
            audioSource.PlayOneShot(gameoverSE);
            break;
            case "retry":
            Debug.Log("retry");
            audioSource.PlayOneShot(retrySE);
            break;
        }
    }
}
