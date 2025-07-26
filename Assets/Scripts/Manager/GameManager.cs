using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelList levelList; // Assign via inspector
    public int currentLevelIndex = 0;
    public int levelPositionZ = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       Debug.Log("GameManager started.");
    }

    public void LoadLevel(int index)
    {   
        Debug.Log($"Loading level {index}");
        if (index >= 0 && index < levelList.levels.Count)
        {
            currentLevelIndex = index;
            SceneLoader.Instance.LoadSceneByName(levelList.levels[index].sceneName);
        }
        // else
        // {
        //     EndGame();
        // }
    }

    public void LoadNextLevel()
    {   
        if (currentLevelIndex + 1 >= levelList.levels.Count)
        {
            EndGame();
            //return;
        }

        LoadLevel(currentLevelIndex + 1);
    }

    public void RestartLevel()
    {
        Debug.Log($"Restarting level {currentLevelIndex}");
        LoadLevel(currentLevelIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneLoader.Instance.LoadSceneByName("MainMenu");
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        SceneLoader.Instance.LoadSceneByName("GameWin");
    }
}