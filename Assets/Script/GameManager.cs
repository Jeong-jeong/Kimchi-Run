using System;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float PlayStartTime;

    public const int MAX_LIVES = 3;
    public int lives = MAX_LIVES;



    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public Player PlayerScript;
    public TMP_Text scoreText;



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
            PlayStartTime = Time.time;

        }

        if (CurrentGameState == GameState.Playing) {
            scoreText.text = "Score: " + CalculateScore();
            SaveHighScore();
        }

        if (CurrentGameState == GameState.Playing && lives == 0)
        {
            PlayerScript.KillPlayer();
            SetActiveGameObjects(false);
            CurrentGameState = GameState.GameOver;
            DeadUI.SetActive(true);
        }

        if (CurrentGameState == GameState.GameOver) {
            scoreText.text = "High Score: " + GetHighScore();
        }


        if (CurrentGameState == GameState.GameOver && PressSpace())
        {
            SceneManager.LoadScene("main");
        }

    }

    int CalculateScore()
    {
        return Mathf.FloorToInt(Time.time - PlayStartTime);
    }

    void SaveHighScore()
    {
        int score = CalculateScore();
        int currentHighScore = GetHighScore();
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
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
