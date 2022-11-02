using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text enemyCountText;
    public TMP_Text difficultyText;
    public TMP_Text timerText;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score:" + _score;
    } 
    
    public void UpdateEnemyCount(int _count)
    {
        enemyCountText.text = "Enemy Count:" + _count;
    }

    public void UpdateDifficultyCount(Difficulty _difficulty)
    {
        difficultyText.text = _difficulty.ToString();
    }

    public void UpdateTimer(float _timer)
    {
        timerText.text = "Time:" + _timer.ToString("F2");
        timerText.color = _timer < 5f ? Color.red : Color.green;
    }
}
