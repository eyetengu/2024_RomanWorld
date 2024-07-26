using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _introPanel;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _extraText;
    [SerializeField] private TMP_Text _playerMessageText;

    [SerializeField] private GameObject _overlayPanel;
    [SerializeField] private TMP_Text _gameCondition;
    [SerializeField] private TMP_Text _scoreText;

    //Built-In Functions
    private void OnEnable()
    {
        Game_Manager.win += DisplayPlayerWin;
    }

    private void OnDisable()
    {
        Game_Manager.win -= DisplayPlayerWin;
    }

    private void Start()
    {
        ClearPlayerCondition();
    }

    //Message Displays
    public void UpdateUIMessage(string message, int state, int dayOfYear)
    {
        if (state < 10)
            _text.text = "Day: " + dayOfYear + "\nTime: 0" + state + ":00\nState: " + message;
        else
            _text.text = "Day: " + dayOfYear + "\nTime: " + state + ":00\nState: " + message;
    }

    public void UpdateUIMessage(string message)
    {
        _text.text = message;
    }
    public void UpdateExperience(int value)
    {

    }

    public void UpdateGold(int value) { }

    public void UpdateHealth(int value) { }

    public void UpdateScore(int value) 
    { 
        _scoreText.text = "Score: " + value;
    }

    public void DisplayPlayerMessage(string message)
    {
        StopCoroutine(ClearPlayerMessage());
        _playerMessageText.text = message;
        StartCoroutine(ClearPlayerMessage());
    }

    public void UpdateEndGameMessage(string message)
    {
        _extraText.text = message;
    }

    public void TurnOffIntroPanel()
    {
        _introPanel.SetActive(false);
    }

    //Game Conditions
    public void DisplayPlayerWin()
    {
        _overlayPanel.SetActive(true);
        _gameCondition.text = "YOU WIN!";
    }

    void ClearGameConditionText()
    {
        _gameCondition.text = "";
    }

    public void DisplayPlayerLoss()
    {
        ClearGameConditionText();
        _overlayPanel.SetActive(true);
        _gameCondition.text = "YOU LOSE!";
    }

    public void ClearPlayerCondition()
    {
        ClearGameConditionText();
        _overlayPanel.SetActive(false);
        _gameCondition.text = "";
    }


    //Coroutines
    IEnumerator ClearExtraText()
    {
        yield return new WaitForSeconds(3);
        _extraText.text = "";
    }

    IEnumerator ClearPlayerMessage()
    {
        yield return new WaitForSeconds(3);
        _playerMessageText.text = "";
    }
}
