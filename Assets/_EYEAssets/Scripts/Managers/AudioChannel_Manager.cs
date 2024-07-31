using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChannel_Manager : MonoBehaviour
{
    [SerializeField] private UI_MusicManager _musicUI;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _musicClips;
    private AudioClip _currentTrack;

    [SerializeField] private bool _music, _ambient, _gameAudio;
    private bool _isPaused;

    private int _trackID;
    private string _trackName;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        _musicUI = FindObjectOfType<UI_MusicManager>();

        _audioSource = GetComponent<AudioSource>();

        _currentTrack = _musicClips[_trackID];
        _trackName = _currentTrack.name;

        PlayCurrentTrack();
        DisplayTrackName();
        CurrentTrackTime();
    }

    void Update()
    {
        //if the audio source is neither playing NOR paused
        if (_audioSource.isPlaying == false && _isPaused == false)
            ChooseNextTrack();

        //UserInput
        if (Input.GetKeyDown(KeyCode.Space))
            ChooseNextTrack();

        DisplayTrackName();
        CurrentTrackTime();
    }

    //CORE FUNCTIONS
    private void ChooseNextTrack()
    {
        _trackID++;

        if (_trackID > _musicClips.Length - 1)
            _trackID = 0;

        _currentTrack = _musicClips[_trackID];
        _audioSource.clip = _currentTrack;

        if (_music)
            _trackName = _currentTrack.name;
        if (_ambient)
            _trackName = _currentTrack.name;
        if (_gameAudio)
            _trackName = _currentTrack.name;

        _audioSource.time = 0;

        PlayCurrentTrack();
    }
    private void PlayCurrentTrack()
    {
        _audioSource.clip = _musicClips[_trackID];
        _audioSource.Play();
    }
    private void DisplayTrackName()
    {
        if (_music)
            _musicUI.DisplayTrackName(_trackName, 0);
        if (_ambient)
            _musicUI.DisplayTrackName(_trackName, 1);
        if (_gameAudio)
            _musicUI.DisplayTrackName(_trackName, 2);
    }
    private void CurrentTrackTime()
    {
        var currentTime = _audioSource.time;
        var shortForm = string.Format("{0:#.00}", currentTime);

        if (_music)
            _musicUI.DisplayCurrentTrackTime(shortForm, 0);

        else if (_ambient)
            _musicUI.DisplayCurrentTrackTime(shortForm, 1);

        else if (_gameAudio)
            _musicUI.DisplayCurrentTrackTime(shortForm, 2);
    }

    //BUTTON ACTIONS
    public void RestartTrack()
    {
        _audioSource.Play();
    }
    public void PauseTrack()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
            _audioSource.Pause();
        else
            _audioSource.Play();
    }
    public void SkipTrack()
    {
        _audioSource.Stop();
    }
    public void SkipAheadFiveSeconds()
    {
        var skipTime = 5.0f;
        _audioSource.time += skipTime;
    }
    public void MuteChannel()
    {
        _audioSource.mute = !_audioSource.mute;
    }
}
