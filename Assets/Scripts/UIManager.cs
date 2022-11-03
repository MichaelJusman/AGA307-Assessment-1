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

    GameManager _GM;

    public void UpdateTimer(float _timer)
    {
        timerText.text = "Time Remaining:" + _timer.ToString("F2");
        timerText.color = _timer <10f ? Color.red : Color.green;
    }

    public void UpdateTargetCount(int _target)
    {
        enemyCountText.text = "Enemy Count:" + _target;
    }

    public void UpdateDifficulty(Difficulty _difficulty)
    {
        difficultyText.text = _difficulty.ToString();
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score:" + _score;
    }

}
