using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController Instance { get; private set; }

    [Header("Level Info")]
    [SerializeField] private TMP_Text levelText;

    [Header("Charge UI")]
    [SerializeField] private Slider chargeSlider;
    [SerializeField] private TMP_Text forceText;

    [Header("Arrow Info")]
    [SerializeField] private TMP_Text arrowCountText;

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

    public void UpdateArrowCount(int count)
    {
        arrowCountText.text = $"Arrows: {count}";
    }
}