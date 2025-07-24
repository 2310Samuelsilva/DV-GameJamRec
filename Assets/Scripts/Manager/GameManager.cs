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
        if (index >= 0 && index < levelList.levels.Count)
        {
            currentLevelIndex = index;
            SceneLoader.Instance.LoadSceneByName(levelList.levels[index].sceneName);
        }
    }

    public void LoadNextLevel()
    {
        LoadLevel(currentLevelIndex + 1);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneLoader.Instance.LoadSceneByName("MainMenu");
    }

    public void EndGame()
    {
        //
    }
}

// // Singleton GameManager
// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance { get; private set; }

//     public GameState CurrentState { get; private set; } = GameState.PreGame;

//     private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }

//         Instance = this;
//         DontDestroyOnLoad(gameObject); // Optional
//     }

//     private void Start()
//     {
//         // Start in pre-game mode, show start UI
//         //Time.timeScale = 0f;
//         UIManager.Instance.ShowStartPanel();

//         StartGame();
//     }

//     public void StartGame()
//     {
//         if (CurrentState != GameState.PreGame) return;

//         Debug.Log("Game Started!");
//         CurrentState = GameState.Playing;

//         Time.timeScale = 1f;
//         UIManager.Instance.HideStartPanel();
//         UIManager.Instance.UpdateLevelUI(1);
//     }

//     public void EndGame()
//     {
//         if (CurrentState == GameState.GameOver) return;

//         Debug.Log("Game Over!");
//         CurrentState = GameState.GameOver;

//         //Time.timeScale = 0f;
//         UIManager.Instance.ShowGameOverPanel();
//     }

//     public void RestartGame()
//     {
//         Time.timeScale = 1f;
//         UnityEngine.SceneManagement.SceneManager.LoadScene(
//             UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
//     }
// }