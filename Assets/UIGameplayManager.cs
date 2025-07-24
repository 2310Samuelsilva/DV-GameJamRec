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

    public void UpdateChargeUI(float currentForce, float maxForce)
    {
        chargeSlider.value = currentForce / maxForce;
        forceText.text = $"{currentForce:F2}";
    }

    public void UpdateProjectileCount(int count)
    {
        projectileCountText.text = $"Projectiles: {count}";
    }

    public void ShowPauseMenu()
    {
        //
        Debug.Log("Show Pause Menu");
    }
}