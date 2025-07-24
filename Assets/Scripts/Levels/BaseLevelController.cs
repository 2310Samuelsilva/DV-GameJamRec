using UnityEngine;
using Unity.Cinemachine;

public class BaseLevelController : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnPoint;
    [SerializeField] protected CinemachineCamera cinemachineCam;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected LevelData levelData;

    protected bool timerIsRunning = false;
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
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        cinemachineCam.Follow = player.transform;
        cinemachineCam.LookAt = player.transform;
    }

    void UpdateTimerDisplay(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);
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

        if (levelCompleted) return;

        levelCompleted = true;
        GameManager.Instance.RestartLevel();
    }
}