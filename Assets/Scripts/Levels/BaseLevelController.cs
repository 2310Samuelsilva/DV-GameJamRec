using UnityEngine;

public class BaseLevelController : MonoBehaviour
{
    protected bool levelCompleted = false;

    public virtual void StartLevel()
    {
        Debug.Log("Level started.");
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