using System;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
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

            if (lives < MAX_LIVES)
            {
                SetFoodActive(true);
            } else {
                SetFoodActive(false);
            }
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

    public float CalculateGameSpeed()
    {
        const float BASE_SPEED = 10f;
        const float INTERVAL_SECONDS = 7f;
        if (CurrentGameState != GameState.Playing)
        {
            return BASE_SPEED;
        }
        float increasedSpeed = BASE_SPEED + (0.5f * Mathf.Floor(CalculateScore() / INTERVAL_SECONDS));
        return increasedSpeed;
    }

    bool PressSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public void SetActiveGameObjects(bool active)
    {
        buildingObject.SetActive(active);
        EnemyObject.SetActive(active);
        GoldenFoodObject.SetActive(active);
    }

    private void SetFoodActive(bool active)
    {
        FoodObject.SetActive(active);
    }
}
