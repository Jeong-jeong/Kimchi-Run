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
    public GameObject GameObject;
    public GameObject EnemyObject;
    public GameObject FoodObject;
    public GameObject GoldenFoodObject;
    public GameState CurrentGameState = GameState.Intro;

    [Header("References")]
    public GameObject IntroUI;
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
            ActiveGameObjects();
        }
    }

    bool PressSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public void ActiveGameObjects()
    {
        GameObject.SetActive(true);
        EnemyObject.SetActive(true);
        FoodObject.SetActive(true);
        GoldenFoodObject.SetActive(true);
    }
}
