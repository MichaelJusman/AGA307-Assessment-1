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


public class GameManager : MonoBehaviour
{
    public GameState gameState;
}
