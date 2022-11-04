using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitle : Singleton<UITitle>
{
    GameManager _GM;

    private void Start()
    {
        _GM = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
        _GM.ChangeGameState(GameState.Playing);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
