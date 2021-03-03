using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("UIManager is NULL.");
            return _instance;
        }
    }

    [Header("Audio sources")]
    [SerializeField] private AudioSource _mainMenuMusic;
    [SerializeField] private AudioSource sfx;

    [Header("Sliders")]
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    [Header("Menus")]
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _pauseMenu;

    [Header("UIs")]
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _mainMenuUI;

    [Header("Buttons")]
    [SerializeField] private GameObject _resumeButton;

    [Header("Positions")]
    [SerializeField] private Transform _mainMenuPos;
    [SerializeField] private Transform _fightPos;
    private Vector3 _lastCameraPos;
    private int lastMusicIndex;



    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        ChangeAudioValueBySlider(musicSlider, _mainMenuMusic);
        ChangeListenerValueBySlider(mainSlider);
        ChangeAudioValueBySlider(SFXSlider, sfx);

        if(Input.GetKeyDown(KeyCode.Escape))
            PauseMenu();
            
            
    }

    #region Sliders


    public float GetSliderValue(Slider slider)
    {
        return slider.value;
    }

    public void ChangeAudioValueBySlider(Slider slider, AudioSource audioSource)
    {
        audioSource.volume = slider.value;
    }

    public void ChangeListenerValueBySlider(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    //public void ChangeAllSFX()
    //{
    //    foreach(var s in sfxs)
    //    {
    //        ChangeAudioValueBySlider(SFXSlider, s.GetComponent<AudioSource>());
    //    }
    //}

    #endregion

    #region Menus

    public void OptionsMenu()
    {
        _optionsMenu.SetActive(!_optionsMenu.activeSelf);
    }

    public void PauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        if(_pauseMenu.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void CreditsMenu()
    {
        _creditsMenu.SetActive(!_creditsMenu.activeSelf);
    }


    #endregion

    #region Buttons

    public void PlayButton()
    {
        GameManager.Instance.CameraToLab();
        InGameUI();
        MainMenuUI();
        Time.timeScale = 1;
    }

    public void ResumeButton()
    {
        Camera.main.transform.position = _lastCameraPos;
        InGameUI();
        MainMenuUI();
        MusicChanger.Instance.LoadMusic(lastMusicIndex);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        _lastCameraPos = Camera.main.transform.position;
        Camera.main.transform.position = _mainMenuPos.position;
        _resumeButton.SetActive(true);
        InGameUI();
        MainMenuUI();
        PauseMenu();
        _mainMenuMusic.Play();
        Time.timeScale = 0;
    }

    public void CloseButton()
    {
        PauseMenu();
    }

    #endregion

    #region UIs

    public void InGameUI()
    {
        _inGameUI.SetActive(!_inGameUI.activeSelf);
    }

    public void MainMenuUI()
    {
        _mainMenuUI.SetActive(!_mainMenuUI.activeSelf);
    }

    #endregion


}
