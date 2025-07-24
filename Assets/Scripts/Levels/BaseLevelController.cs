using UnityEngine;

public class BaseLevelController : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnPoint;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected LevelData levelData;


    protected bool levelCompleted = false;

    public virtual void StartLevel()
    {
        Debug.Log($"Level started: {levelData.levelName}");
        UIGameplayManager.Instance.UpdateLevelUI(levelData.levelNumber);       
       
        spawnPlayer();
    }
    

    private void spawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
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