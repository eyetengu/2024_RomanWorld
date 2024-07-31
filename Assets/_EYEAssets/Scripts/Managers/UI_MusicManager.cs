using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class UI_MusicManager : MonoBehaviour
{
    [SerializeField] GameObject _mainUICanvas;
    [SerializeField] GameObject _mainCanvasPanels;

    [SerializeField] private GameObject[] _uiMenus;

    [SerializeField] private TMP_Text _musicTrackNameDisplay, _ambientTrackNameDisplay, _gameAusioTrackNameDisplay;
    private  string _musicTrackName, _ambientTrackName, _gameAudioTrackName;
    [SerializeField] private GameObject _trackNameBG;

    [SerializeField] private TMP_Text _musicTimeCurrent, _ambientTimeCurrent, _gameAudioTimeCurrent;

    [SerializeField] private Slider _sliderMaster, _sliderAmbient, _sliderMusic, _sliderGameAudio;
    [SerializeField] private Toggle[] _toggles_audio;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Image _blackOverlay;
    [SerializeField] private Slider _sliderBrightness;

    [SerializeField] private GameObject _splashScreen;
    private bool _isMuteAll;

    [SerializeField] private AudioSource _asMusic, _asAmbience, _asGeneral;


    //BUILT-IN FUNCTIONS
    private void OnEnable()
    {
        _mainUICanvas.SetActive(false);
        _splashScreen.SetActive(true);

        HideAllScreens();
        StartCoroutine(FadeSplashScreen());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            _mainCanvasPanels.SetActive(false);

            foreach (GameObject menu in _uiMenus)
                menu.SetActive(false);
            _uiMenus[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            _mainCanvasPanels.SetActive(false);

            foreach (GameObject menu in _uiMenus)
                menu.SetActive(false);
            _uiMenus[1].SetActive(true);
        }
    }

    //VISUAL
    public void AdjustBrightness()
    {
        var tempColor = _blackOverlay.color;
        tempColor.a = _sliderBrightness.value;
        _blackOverlay.color = tempColor;
    }

    //AUDIO TRACKS
    public void DisplayCurrentTrackTime(string trackTime, int index)
    {
        switch (index)
        {
            case 0:
                _musicTimeCurrent.text = trackTime.ToString();
                break;
            case 1:
                _ambientTimeCurrent.text = trackTime.ToString();
                break;
            case 2:
                _gameAudioTimeCurrent.text = trackTime.ToString();
                break;

            default:
                break;
        }
    }
    
    public void DisplayTrackName(string trackName, int index)
    {
        switch (index)
        {
            case 0:
                _musicTrackNameDisplay.text = trackName;
                break;
            case 1:
                _ambientTrackNameDisplay.text = trackName;
                break;
            case 2:
                _gameAusioTrackNameDisplay.text = trackName;
                break;

            default:
                break;
        }
    }

    //UI MENUS
    private void HideAllScreens()
    {
        foreach (GameObject menu in _uiMenus)
            menu.SetActive(false);
    }

    //VOLUME CONTROLS
    public void AdjustMasterVolume()
    {
        _mixer.SetFloat("MasterVolume", _sliderMaster.value);
    }
    
    public void AdjustAmbientVolume()
    {
        _mixer.SetFloat("Ambiance", _sliderAmbient.value);
        //_asAmbience.volume = _sliderAmbient.value;
    }
    
    public void AdjustMusicVolume()
    {
        _mixer.SetFloat("Music", _sliderMusic.value);
        //_asMusic.volume = _sliderMusic.value;
    }
    
    public void AdjustGameAudio()
    {
        _mixer.SetFloat("GameAudio", _sliderGameAudio.value);
    }

    public void MuteAllChannels()
    {
        _isMuteAll = !_isMuteAll;

        if (_isMuteAll)
            _sliderMaster.value = -80.0f;
        else
            _sliderMaster.value = 0;

        AdjustMasterVolume();
    }

    void SetWalkAroundSpecifications()
    {
        var tempColor = _blackOverlay.color;
        tempColor.a = 0;
        _blackOverlay.color = tempColor;
    }


    //COROUTINES
    IEnumerator FadeSplashScreen()
    {
        yield return new WaitForSeconds(3);
        _splashScreen.SetActive(false);
        _mainUICanvas.SetActive(true);
    }
}
