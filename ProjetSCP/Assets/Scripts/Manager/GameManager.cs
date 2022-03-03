using SCP.Ressources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum GameState
    {
        GAME,
        BUILDING
    }

public class GameManager : MonoBehaviour
{

    public GameState gameState = GameState.GAME;

    private void Awake()
    {
        new Registry().Register<GameManager>(this);
        var ressourcesManager = new RessourcesManager();

    }

    private void Start()
    {
        
    }


}
