using System;
using System.ComponentModel.Design;
using UnityEngine;

public enum GameState
{
    Intro,
    Playing,
    GameOver,
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject buildingObject;
    public GameObject EnemyObject;
    public GameObject FoodObject;
    public GameObject GoldenFoodObject;
    public GameState CurrentGameState = GameState.Intro;

    public const int MAX_LIVES = 3;
    public int lives = MAX_LIVES;



    [Header("References")]
    public GameObject IntroUI;
    public Player PlayerScript;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        IntroUI.SetActive(true);
    }

    void Update()
    {
        if (CurrentGameState == GameState.Intro && PressSpace())
        {
            CurrentGameState = GameState.Playing;
            IntroUI.SetActive(false);
            SetActiveGameObjects(true);
        }

        if (CurrentGameState == GameState.Playing && lives == 0)
        {
            PlayerScript.KillPlayer();
            SetActiveGameObjects(false);
            CurrentGameState = GameState.GameOver;
        }

    }

    bool PressSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public void SetActiveGameObjects(bool active)
    {
        buildingObject.SetActive(active);
        EnemyObject.SetActive(active);
        FoodObject.SetActive(active);
        GoldenFoodObject.SetActive(active);
    }
}
