using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIGameplayManager : MonoBehaviour
{
    public static UIGameplayManager Instance { get; private set; }

    [Header("Level Info")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text levelTtimer;

    [Header("Charge UI")]
    [SerializeField] private Slider chargeSlider;
    [SerializeField] private TMP_Text forceText;

    [Header("Projectile Info")]
    [SerializeField] private TMP_Text projectileCountText;
    [SerializeField] protected GameObject pauseScreen;




    // Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdateLevelUI(int level)
    {
        levelText.text = $"Level: {level}";
    }

    public void UpdateTimerUI(string time)
    {
        levelTtimer.text = time;
    }

    
    public void PauseGame()
    {
        BaseLevelController.Instance.PauseGame();
    }
}