using UnityEngine;

public class UIPauseManager : MonoBehaviour
{
    public static UIPauseManager Instance { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    

    public void ReturnToGame()
    {
        BaseLevelController.Instance.UnPauseGame();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
    
    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }
}
