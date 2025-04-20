using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    public AudioClip walkSE;
    private PlayerMoveController playerMoveController;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMoveController = GetComponent<PlayerMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveController.isWalking)
        {
            if (!audioSource.isPlaying)
            {
                Debug.Log("a");
                audioSource.clip = walkSE;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
