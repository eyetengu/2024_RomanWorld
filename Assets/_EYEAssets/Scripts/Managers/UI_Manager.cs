using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _introPanel;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _extraText;
    [SerializeField] private TMP_Text _playerMessageText;

    [SerializeField] private GameObject _overlayPanel;
    [SerializeField] private GameObject _playerMessageBG;

    [SerializeField] private TMP_Text _gameCondition;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _coinText;

    [SerializeField] private RawImage _equippedObjectImage;


    //Built-In Functions
    private void OnEnable()
    {
        Game_Manager.win += DisplayPlayerWin;
        Game_Manager.walkAround += RemovePlayerWinScreen;
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

    public void DisplayPlayerMessage(string message)
    {
        StopCoroutine(ClearPlayerMessage());
        StartCoroutine(ClearPlayerMessage());

        _playerMessageBG.SetActive(true);
        _playerMessageText.text = message;
    }

    public void UpdateEndGameMessage(string message)
    {
        _extraText.text = message;
    }

    public void TurnOffIntroPanel()
    {
        _introPanel.SetActive(false);
    }


    //Player Values
    public void UpdateExperience(int value)
    {

    }

    public void UpdateGold(int value) 
    { 
        _coinText.text = value.ToString();
    }

    public void UpdateHealth(int value) { }

    public void UpdateScore(int value) 
    { 
        _scoreText.text = "Score: " + value;
    }


    //UI Visuals
    public void ShowEquippedItem(Texture equippedItem)
    {
        if(equippedItem != null)
            _equippedObjectImage.GetComponent<RawImage>().texture = equippedItem;
    }

    //Game Conditions
    public void DisplayPlayerWin()
    {
        _overlayPanel.SetActive(true);
        _gameCondition.text = "YOU WIN!\nPress 'T' To Continue Exploring\n Press 'R' To Restart";
    }

    public void RemovePlayerWinScreen()
    {
        _gameCondition.text = "";
        _overlayPanel.SetActive(false);
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
        _playerMessageBG.SetActive(false);

        _playerMessageText.text = "";
    }
}
