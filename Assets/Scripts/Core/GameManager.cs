using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] bool levelPassed;
    [SerializeField] bool gameOver;
    [SerializeField] int numBricks;
    [SerializeField] int numLives;
    [SerializeField] int score;
    [SerializeField] int currentLevel;

    [SerializeField] TMP_Text livesText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Transform GameOverPanel;
    [SerializeField] Transform LoadingLevelPanel;

    [SerializeField] Ball mainBall;
    [SerializeField] List<GameObject> allLevels;
    GameObject currentLevelObject;
    GameObject[] allBricks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLevel();
        livesText.text = "Lives: " + numLives;
        scoreText.text = score + " Points";
    }

    void CountInitialBricks()
    {
        allBricks = GameObject.FindGameObjectsWithTag("Brick");
        print(allBricks.Length);

        for (int i = 0; i < allBricks.Length; i++)
        {
            var infiniteBrick = allBricks[i].GetComponent<InfiniteBrick>();

            if (!infiniteBrick)
            {
                print(allBricks[i].name);
                numBricks++;
            }
        }
    }

    public void UpdateBrickNum()
    {
        numBricks--;

        livesText.text = "Lives: " + numLives;
        scoreText.text = score + " Points";

        if (numBricks == 0)
        {
            // Level Passed
            LevelCleared();
            
            if (currentLevel < allLevels.Count)
            {
                Invoke("LoadLevel", 3f);
            }
            else
            {
                // Game Over
            }
            // If there are more levels
            // Load next level
            // Else
            // You beat the game
        }
    }

    void CleanupLevel()
    {
        currentLevelObject.SetActive(false);
    }

    private void LoadLevel()
    {
        currentLevelObject = Instantiate(allLevels[currentLevel], Vector2.zero, Quaternion.identity);
        levelPassed = false;
        LoadingLevelPanel.gameObject.SetActive(false);
        CountInitialBricks();
    }

    private void LevelCleared()
    {
        levelPassed = true;
        CleanupLevel();

        currentLevel++;
        LoadingLevelPanel.gameObject.SetActive(true);
        LoadingLevelPanel.GetComponentInChildren<TMP_Text>().text = "Get Outta' Here! Go to Level " + (currentLevel + 1) + "!";
        //mainBall.ResetBall();
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score + " Points";
        print(score);
    }

    public void UpdateLiveNum(int value = -1)
    {
        numLives += value;

        if (numLives == 0)
        {
            // Game Over
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        GameOverPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
