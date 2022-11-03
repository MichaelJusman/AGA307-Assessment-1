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


public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    public int Score;

    Target _TG;

    public void AddScore(int _score)
    {
        Score += _score;
    }

    public void OnEnemyDie()
    {
        AddScore(10);
    }

}
