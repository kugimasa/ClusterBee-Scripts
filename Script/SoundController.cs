using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioClip SelectSE;
    public AudioClip MoveCursorSE;
    public AudioClip GameWinSE;
    public AudioClip GameLoseSE;
    public AudioClip WhistleSE;
    private AudioSource audioSource;
    private bool gameOverPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume()
    {
        audioSource.volume = GameObject.Find("Slider").GetComponent<Slider>().normalizedValue;
    }

    public void Initialize()
    {
        gameOverPlayed = false;
    }

    public void PlaySelect()
    {
        audioSource.PlayOneShot(SelectSE);
    }
    
    public void PlayMoveCursor()
    {
        audioSource.PlayOneShot(MoveCursorSE);
    }

    public void PlayWhistleSE()
    {
        audioSource.PlayOneShot(WhistleSE);
    }

    public void PlayGameWin()
    {
        if (!gameOverPlayed)
        {
            audioSource.PlayOneShot(GameWinSE);
        }
        gameOverPlayed = true;
    }

    public void PlayGameLose()
    {
        if (!gameOverPlayed)
        {
            audioSource.PlayOneShot(GameLoseSE);
        }
        gameOverPlayed = true;
    }

}
