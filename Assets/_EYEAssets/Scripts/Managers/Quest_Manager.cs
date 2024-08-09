using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    [SerializeField] List<string> _activeQuests = new List<string>();
    [SerializeField] List<string> _completedQuests = new List<string>();

    Game_Manager _gameManager;
    UI_Manager _uiManager;
    [SerializeField] int _numberOfQuestsToWin;
    bool _gameWon;


    //BUILT-IN FUNCTIONS
    private void OnEnable()
    {
        _gameManager = FindObjectOfType<Game_Manager>();
        _uiManager = FindObjectOfType<UI_Manager>();

        //Game_Manager.pauseGame += PauseActivity;
        //Game_Manager.unPauseGame += UnPauseActivity;
    }

    private void Start()
    {
        _uiManager = GameObject.FindObjectOfType<UI_Manager>();
    }

    void Update()
    {
       
        if (_completedQuests.Count >= _numberOfQuestsToWin && _gameWon == false)
        {
            _gameWon = true;
            _gameManager.YouWin();

            var message = $"You have completed all " + _numberOfQuestsToWin + " quests!";
            _uiManager.UpdateEndGameMessage(message);
        }
    }

    private void OnDisable()
    {
        //Game_Manager.gameWon -= PauseActivity;
        //Game_Manager.unPauseGame -= UnPauseActivity;
    }

    //CORE FUNCTIONS
    public void AddActiveQuest(string quest)
    {
        _activeQuests.Add(quest);
    }

    public void AddCompletedQuest(string quest)
    {
        _activeQuests.Remove(quest);
        _completedQuests.Add(quest);
    }

    void PauseActivity()
    {
        //Debug.Log("Quest_ Paused");
    }

    void UnPauseActivity()
    {
        //Debug.Log("Quest_ UnPaused");
    }
}
