using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int score;
    //int scoreMultipyer = 1;
    public float timer = 30;
    public float timePlus = 5;

    UIManager _UI;

    private void Start()
    {
        //Setup();
        score = 0;
        timer = 30;
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            if (score > 0)
            {
                timer -= Time.deltaTime;
                _UI.UpdateTimer(timer);
            }
            else
            {
                timer = 0;
                gameState = GameState.GameOver;
                _UI.UpdateTimer(timer);
            }

        }
    }

    //public void Setup()
    //{
    //    switch(difficulty)
    //    {
    //        case Difficulty.Easy:
    //            scoreMultipyer = 1;
    //            break;

    //        case Difficulty.Medium:
    //            scoreMultipyer = 2;
    //            break;

    //        case Difficulty.Hard:
    //            scoreMultipyer = 3;
    //            break;

    //    }
    //}

    public void AddScore(int _score)
    {
        score += _score;
        _UI.UpdateScore(score);
    }

    public void OnEnemyDie()
    {
        AddScore(10);
    }

    public void AddTimer()
    {
        timer = timer + timePlus;
    }
}
