using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

public class BaseLevelController : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnPoint;
    [SerializeField] protected CinemachineCamera cinemachineCam;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected LevelData levelData;

    [SerializeField] protected GameObject gameOverScreen;
    [SerializeField] protected GameObject pauseScreen;
    [SerializeField] protected GameObject winScreen;
    protected bool timerIsRunning = false;
    protected float timeRemaining;

    protected bool levelCompleted = false;
    private bool gamePaused = false;

    private List<Target> targets = new List<Target>();

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

        HideWinScreen();
        HideGameOverScreen();
        UnPauseGame();

        levelCompleted = false;

        // Auto-find all active targets in the scene
        targets = new List<Target>(FindObjectsByType<Target>(FindObjectsSortMode.None));

        UIGameplayManager.Instance.UpdateLevelUI(levelData.levelNumber);
        SpawnPlayer();
        StartTimer();
    }

    private void StartTimer()
    {
        timeRemaining = levelData.timeToCompleteLevel;
        timerIsRunning = true;
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        cinemachineCam.Follow = player.transform;
        cinemachineCam.LookAt = player.transform;

        EnvironmentScroller.Instance.player = player.transform;
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
        //if (levelCompleted) return;

        levelCompleted = true;
        PauseGameplay();
        ShowWinScreen();
        //GameManager.Instance.LoadNextLevel();
    }

    public virtual void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

    public virtual void HideWinScreen()
    {
        winScreen.SetActive(false);
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
        UnpauseGameplay();
        pauseScreen.SetActive(false);
    }

    public void PauseGame()
    {
        gamePaused = true;
        PauseGameplay();
        pauseScreen.SetActive(true);
    }

    void PauseGameplay()
    {
        Time.timeScale = 0f;
    }

    void UnpauseGameplay()
    {
        Time.timeScale = 1f;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }

    // ---------- Target Handling ----------
    public void RegisterTarget(Target target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    public void TargetHit(Target target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);

            if (targets.Count == 0)
            {
                CompleteLevel();
            }
        }
    }
}