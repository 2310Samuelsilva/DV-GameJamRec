using UnityEngine;

public class UIGameOverManager : MonoBehaviour
{
    public static UIGameOverManager Instance { get; private set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // public void Show()
    // {
    //     this.enabled = true;
    // }

    // public void Hide()
    // {
    //     this.enabled = false;
    // }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
    
    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }
}
