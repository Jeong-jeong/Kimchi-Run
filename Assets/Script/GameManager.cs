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
    public AudioSource bgmSource;
    public GameState CurrentGameState = GameState.Intro;
    public float PlayStartTime;

    public const int MAX_LIVES = 3;
    public int lives = MAX_LIVES;



    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public Player PlayerScript;
    public TMP_Text roundText;



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
            roundText.text = "Round: " + CalculateRound();
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
            roundText.text = "High Score Round: " + GetHighScore();
        }


        if (CurrentGameState == GameState.GameOver && PressSpace())
        {
            SceneManager.LoadScene("main");
        }

    }

    int CalculateRound()
    {
        const float INTERVAL_SECONDS = 7f;
        const int FIRST_ROUND = 1;
        Debug.Log($"Round: {Mathf.FloorToInt((Time.time - PlayStartTime) / INTERVAL_SECONDS + FIRST_ROUND)}");
        return Mathf.FloorToInt((Time.time - PlayStartTime) / INTERVAL_SECONDS) + FIRST_ROUND;
    }

    void SaveHighScore()
    {
        int round = CalculateRound();
        PlayerPrefs.SetInt("highScoreRound", round);
        PlayerPrefs.Save();
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScoreRound");
    } 

    public float CalculateGameSpeed()
    {
        const float BASE_SPEED = 10f;
        if (CurrentGameState != GameState.Playing)
        {
            return BASE_SPEED;
        }
        float increasedSpeed = BASE_SPEED + (0.5f * CalculateRound());
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

    public void StopBGM()
    {
        if (bgmSource)
        {
            bgmSource.Stop();
        }
    }
}
