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
    private RessourcesManager ressourcesManager;
    public GameObject scpObjectPrefab;

    private void Awake()
    {
        new Registry().Register<GameManager>(this);
        ressourcesManager = new RessourcesManager(12, 10000);
    }

    private void Start()
    {
        SoundManager.instance.PlaySound("GameMusic");
    }


}
