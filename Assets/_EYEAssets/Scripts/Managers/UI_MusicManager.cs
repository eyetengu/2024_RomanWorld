using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class UI_MusicManager : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Game_Manager _gameManager;
    [SerializeField] private AudioMixer _mixer;

    [Header("UI GENERAL")]
    [SerializeField] GameObject _mainUICanvas;
    [SerializeField] GameObject _mainCanvasPanels;
    [SerializeField] private GameObject _splashScreen;
    [SerializeField] private GameObject _trackNameBG;
    [SerializeField] private GameObject[] _uiMenus;

    [Header("TEXT FIELDS")]
    [SerializeField] private TMP_Text _musicTrackNameDisplay;
    [SerializeField] private TMP_Text _ambientTrackNameDisplay;
    [SerializeField] private TMP_Text _gameAudioTrackNameDisplay;
    [SerializeField] private TMP_Text _musicTimeCurrent;
    [SerializeField] private TMP_Text _ambientTimeCurrent;
    [SerializeField] private TMP_Text _gameAudioTimeCurrent;
    
    [Header("SLIDERS")]
    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderAmbient;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderGameAudio;
    [SerializeField] private Slider _sliderBrightness;

    [Header("TOGGLES")]
    [SerializeField] private Toggle[] _toggles_audio;
    [SerializeField] private Image _blackOverlay;

    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource _asMusic;
    [SerializeField] private AudioSource _asAmbience;
    [SerializeField] private AudioSource _asGeneral;

    private string _musicTrackName;
    private string _ambientTrackName;
    private string _gameAudioTrackName;
    private bool _isMuteAll;



    //BUILT-IN FUNCTIONS
    private void OnEnable()
    {
        _mainUICanvas.SetActive(false);
        _splashScreen.SetActive(true);

        Game_Manager.win += PlayGameOverAudio;

        HideAllScreens();
        StartCoroutine(FadeSplashScreen());
    }

    private void OnDisable()
    {
        Game_Manager.win -= PlayGameOverAudio;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            _gameManager.ShowAndConfineCursor();
                
            _mainCanvasPanels.SetActive(false);

            foreach (GameObject menu in _uiMenus)
                menu.SetActive(false);
            _uiMenus[0].SetActive(true);
            _gameManager.PausePlayer();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //_mainCanvasPanels.SetActive(false);

            //foreach (GameObject menu in _uiMenus)
                //menu.SetActive(false);
            //_uiMenus[1].SetActive(true);
        }
    }

    //VISUAL
    public void AdjustBrightness()
    {
        var tempColor = _blackOverlay.color;
        tempColor.a = Mathf.Clamp(_sliderBrightness.value, 0, 0.97f);
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
                _gameAudioTrackNameDisplay.text = trackName;
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


    //IN-GAME AUDIO FUNCTIONS
    public void PlayGeneralAudioClip(AudioClip clip)
    {
        _asGeneral.PlayOneShot(clip);
    }
    
    public void PlayGameOverAudio()
    {

    }

    //COROUTINES
    IEnumerator FadeSplashScreen()
    {
        yield return new WaitForSeconds(3);
        _splashScreen.SetActive(false);
        _mainUICanvas.SetActive(true);
        _gameManager.UnpausePlayer();
    }
}
