using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    [SerializeField] List<string> _activeQuests = new List<string>();
    [SerializeField] List<string> _completedQuests = new List<string>();

    [SerializeField] Game_Manager _gameManager;
    [SerializeField] UI_Manager _uiManager;
    [SerializeField] int _numberOfQuestsToWin;
    bool _gameWon;

    private void Start()
    {
        _uiManager = GameObject.FindObjectOfType<UI_Manager>();
    }

    void Update()
    {
       
        if (_completedQuests.Count >= _numberOfQuestsToWin)
        {
            _gameManager.YouWin();
            var message = $"You have completed all " + _numberOfQuestsToWin + " quests!";
            _uiManager.UpdateEndGameMessage(message);
        }
    }

    public void AddActiveQuest(string quest)
    {
        _activeQuests.Add(quest);
    }

    public void AddCompletedQuest(string quest)
    {
        _activeQuests.Remove(quest);
        _completedQuests.Add(quest);
    }
}
