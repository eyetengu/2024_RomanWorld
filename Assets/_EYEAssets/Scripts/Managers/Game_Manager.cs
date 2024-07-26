using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] int _speedIndex;

    private bool _isCursorLocked;
    [SerializeField] private bool _isPaused;

    float[] _speedValues = { 0.0f, 0.5f, 1.0f, 2.0f };
    [SerializeField] float timer;
    [SerializeField] float _timeScale;

    public delegate void WinCondition();
    public static event WinCondition win;


    //BUILT-IN FUNCTIONS
    void Start()
    {
        LockCursorInvisible();
        _timeScale = 2;
        GameSpeed();
    }

    void Update()
    {
        UserInput();        
        GameTimer();

        Debug.Log("Time Scale: " + _timeScale);
    }

    //USER INPUTS
    void UserInput()
    {        
        //SPEED INDEX MANIPULATOR
        if (Input.GetKeyDown(KeyCode.P))
            PauseGame();

        //CURSOR VISIBILITY
        if (Input.GetKey(KeyCode.Escape))
           UnlockCursorVisible();   
    }

    //GAME CONDITIONS
    public void YouWin()
    {
        _timeScale = 0;
        GameSpeed();
        win();
        Debug.Log("YOU WIN!");
    }

    public void YouLose()
    {
        _timeScale = 0;
        GameSpeed();
        Debug.Log("YOU LOSE!");
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            _timeScale = 0;
            GameSpeed();
        }
        else
        {
            _timeScale = 2;
            GameSpeed();
        }        
    }
    
    //GAME FUNCTIONS
    void GameTimer()
    {
        timer += Time.deltaTime;
        //Debug.Log("Timer: " + timer);
        Debug.Log("Speed Index: " + _speedIndex);
    }

    void GameSpeed()
    {
        //_timeScale = _speedValues[_speedIndex];
        Time.timeScale = _timeScale;
    }

    //CURSOR FUNCTIONS
    void LockCursorInvisible()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursorVisible()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
