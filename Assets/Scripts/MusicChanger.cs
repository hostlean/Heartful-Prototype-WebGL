using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    private static MusicChanger _instance;
    public static MusicChanger Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("Music Changer is NULL");
            return _instance;
        }
    }

    [SerializeField] private List<AudioClip> musics = new List<AudioClip>();

    AudioSource audioSource;

    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }



    public void LoadMusic(int musicsIndex)
    {
        audioSource.clip = musics[musicsIndex];
        audioSource.Play();
    }
}
