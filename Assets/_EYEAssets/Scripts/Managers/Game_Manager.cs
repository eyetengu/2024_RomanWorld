using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game_Manager : MonoBehaviour
{
    //----------DELEGATES & EVENTS----------
    public delegate void GamePause();
    public static event GamePause pauseGame;

    public delegate void GameUnPaused();
    public static event GameUnPaused unPauseGame;

    //public delegate void GameWon();
    //public static event GameWon gameWon;

    public delegate void GameLost();
    public static event GameLost gameLost;

    public delegate void WinCondition();
    public static event WinCondition win;

    public delegate void WalkAround();
    public static event WalkAround walkAround;


    //---------------FIELDS---------------
    [SerializeField] int _speedIndex;

    private bool _isCursorLocked;

    [Header("Game Conditions")]
    [SerializeField] private bool _isPaused;
    [SerializeField] private bool _gameOver;

    [Header("Time Scale")]
    [SerializeField] float timer;
    [SerializeField] float _timeScale;
    float[] _speedValues = { 0.0f, 0.5f, 1.0f, 2.0f };


    //BUILT-IN FUNCTIONS
    void Start()
    {
        //LockCursorInvisible();
        _timeScale = 1.0f;
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
        //Restart Game
        if(_gameOver && Input.GetKeyDown(KeyCode.R))
        {
            _gameOver = false;
            //_isPaused = false;
            //PauseGame();
            _timeScale = 1.0f;
            GameSpeed();
            SceneManager.LoadScene(0);
        }

        //Unpause And Keep Game Over
        if(_gameOver && Input.GetKeyDown(KeyCode.T))
        {
            walkAround();

            _timeScale = 1.0f;            
            GameSpeed();
        }

        //SPEED INDEX MANIPULATOR
        if (Input.GetKeyDown(KeyCode.P))
            PauseGame();

        //CURSOR VISIBILITY
        if (Input.GetKey(KeyCode.Escape))
           UnlockCursorVisible();   
    }

    void GameTimer()
    {
        timer += Time.deltaTime;
        Debug.Log("Speed Index: " + _speedIndex);
    }

    //GAME CONDITIONS
    public void YouWin()
    {
        win();        

        _gameOver = true;

        _timeScale = 0;
        Debug.Log("YOU WIN!");
        GameSpeed();
    }

    public void YouLose()
    {
        pauseGame();

        _gameOver = true;
        _timeScale = 0;
        GameSpeed();
        Debug.Log("YOU LOSE!");
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
     
        if (_isPaused)
        {
           //pauseGame();

            _timeScale = 0;
            GameSpeed();
        }
        else
        {
            //unPauseGame();

            _timeScale = 1;
            GameSpeed();
        }        
    }
    
    //GAME FUNCTIONS
    void GameSpeed()
    {
        Debug.Log("GameSpeed Adjusting");
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
