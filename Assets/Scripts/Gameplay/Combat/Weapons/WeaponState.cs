using UnityEngine;

public class BowState : MonoBehaviour
{
    private bool isCharging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCharging = false;
    }

    public bool IsCharging()
    {
        return isCharging;
    }

    public void StartCharging()
    {
        isCharging = true;
        Debug.Log("Start charging bow!");
    }

    public void StopCharging()
    {
        isCharging = false;
    }

}
