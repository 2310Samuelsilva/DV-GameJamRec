using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        GameManager.Instance.LoadLevel(0); // Load first level
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
}