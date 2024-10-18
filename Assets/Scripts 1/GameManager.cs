using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private const string highScoreKey = "HighScore";

    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(highScoreKey,0);
        }
        set
        {
            PlayerPrefs.SetInt(highScoreKey, value);
        }
    }

    public int CurrentScore { get; set; }
    public bool IsInitialized { get; set; }


    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }

    public const string Evolution_3_Menu = "Evolution_3_Menu";
    public const string Evolution_3_Gameplay = "Evolution_3_Gameplay";
    public const string Game_Completed = "Game_Completed";

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Evolution_3_Menu);
    }

    public void GoToGameplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Evolution_3_Gameplay);
    }
    public void GameCompleted()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(Game_Completed);
    }
}
