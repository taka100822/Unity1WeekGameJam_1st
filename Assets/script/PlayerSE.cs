using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    public AudioClip walkSE;
    private PlayerMoveController playerMoveController;
    private PlayerInteraction playerInteraction;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMoveController = GetComponent<PlayerMoveController>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveController.isWalking && !playerMoveController.isdead && !playerInteraction.isclear)
        {
            if (!audioSource.isPlaying)
            {
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
