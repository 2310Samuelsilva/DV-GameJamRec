using UnityEngine;

public class BaseLevelController : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnPoint;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected LevelData levelData;

    protected bool timerIsRunning = true;
    protected float timeRemaining;




    protected bool levelCompleted = false;


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
        Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        UIGameplayManager.Instance.UpdateTimerUI(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    void TimerEnded()
    {
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
        if (levelCompleted) return;

        levelCompleted = true;
        GameManager.Instance.RestartLevel();
    }
}