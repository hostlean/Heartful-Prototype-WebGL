using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXChanger : MonoBehaviour
{
    private static SFXChanger _instance;
    public static SFXChanger Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("SFX Changer is NULL");
            return _instance;
        }
    }

    [SerializeField] private List<AudioClip> sfx = new List<AudioClip>();

    AudioSource audioSource;

    private void Awake()
    {
        _instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    public void PlayBattleWon()
    {
        audioSource.PlayOneShot(sfx[1]);
    }

    public void PlayBattleLost()
    {
        audioSource.PlayOneShot(sfx[0]);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(sfx[3]);
    }

    public void PlayHurt()
    {
        audioSource.PlayOneShot(sfx[2]);
    }

    public void PlayPowerUp()
    {
        audioSource.PlayOneShot(sfx[4]);
    }
}
