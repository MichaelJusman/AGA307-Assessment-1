using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,
    Playing,
    Paused,
    GameOver
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}


public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;

    public int Score;
    public float maxTime = 30;
    public float timer = 30;
    public int scoreMultiplier = 1;
    public float bonusTime = 5;
    



    UIManager _UI;
    Target _TG;

    

    private void Start()
    {
        _UI = FindObjectOfType<UIManager>();
        _TG = FindObjectOfType<Target>();

     
    }

    public void ChangeGameState(GameState _gameState)
    {
        gameState = _gameState;
    }

    public void ChangeDifficulty(int _difficulty)
    {
        difficulty = (Difficulty)_difficulty;
        Setup();
        _UI.UpdateDifficulty(difficulty);
    }

    public void Update()
    {
       if(gameState == GameState.Playing)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, maxTime);
            _UI.UpdateTimer(timer);
        }

       if(Input.GetKeyDown(KeyCode.J))
        {
            difficulty = Difficulty.Easy;
            _UI.UpdateDifficulty(difficulty);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            difficulty = Difficulty.Medium;
            _UI.UpdateDifficulty(difficulty);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            difficulty = Difficulty.Hard;
            _UI.UpdateDifficulty(difficulty);
        }

    }

    public void AddScore(int _score)
    {
        Score += _score * scoreMultiplier;
        _UI.UpdateScore(Score);
    }

    public void OnTargetHit()
    {
        Debug.Log("hit");
    }

    public void OnTargetDie()
    {
        AddScore(10);
    }

    public void UpdateBonusTime()
    {
        timer = timer + bonusTime;
        _UI.UpdateTimer(timer);
    }

    public void Setup()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                
                scoreMultiplier = 1;
                break;

            case Difficulty.Medium:
                scoreMultiplier = 2;
                break;

            case Difficulty.Hard:
                scoreMultiplier = 3;
                break;
        }
           
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
        ChangeGameState(GameState.Playing);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
