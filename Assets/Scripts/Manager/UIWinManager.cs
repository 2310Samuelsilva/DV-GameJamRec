using UnityEngine;

public class UIWinManager : MonoBehaviour
{
    public static UIWinManager Instance { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    public void PlayNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
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
