using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using System;

public class BaseLevelController : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnPoint;
    [SerializeField] protected CinemachineCamera cinemachineCam;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected LevelData levelData;

    [SerializeField] protected GameObject gameOverScreen;
    [SerializeField] protected GameObject pauseScreen;
    protected bool timerIsRunning = false;
    protected float timeRemaining;

    protected bool levelCompleted = false;

    private bool gamePaused = false;


    public static BaseLevelController Instance { get; private set; }
    protected virtual void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                TimerEnded();
            }
        }
    }




    public virtual void StartLevel()
    {
        Debug.Log($"Level started: {levelData.levelName}");

        HideGameOverScreen();
        UnPauseGame();

        levelCompleted = false;

        UIGameplayManager.Instance.UpdateLevelUI(levelData.levelNumber);
        spawnPlayer();
        startTimer();
    }


    private void startTimer()
    {
        timeRemaining = levelData.timeToCompleteLevel;
        timerIsRunning = true;
    }


    private void spawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        cinemachineCam.Follow = player.transform;
        cinemachineCam.LookAt = player.transform;
    }

    void UpdateTimerDisplay(float time)
    {
        int totalSeconds = Math.Max(Mathf.FloorToInt(time), 0);

        UIGameplayManager.Instance.UpdateTimerUI(totalSeconds.ToString());
    }

    void TimerEnded()
    {
        Debug.Log("Time Ended!");
        FailLevel();
    }
    public virtual void CompleteLevel()
    {
        if (levelCompleted) return;

        levelCompleted = true;
        GameManager.Instance.LoadNextLevel();
    }

    public virtual void FailLevel()
    {
        ShowGameOverScreen();
        if (levelCompleted) return;

    }



    public void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void UnPauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }
    
    public bool IsGamePaused()
    {
        return gamePaused;
    }
}