using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    private WeaponState weaponState;
    [SerializeField] private Animator playerAnimator;
    //[SerializeField] private ParticleSystem shootEffect;
    private LineRenderer lineRenderer;
    private float lineWidth = 0.05f;
    private float lineWidthEndFactor = 0.5f;
    public Color lineColor = Color.green;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponState = GetComponent<WeaponState>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // Line properties
        lineRenderer.material.color = lineColor;
        //lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth / lineWidthEndFactor;
        lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponState.IsCharging())
        {
            playerAnimator.SetBool("isCharging", true);
        }
        else
        {
            playerAnimator.SetBool("isCharging", false);
        }
    }

    public void DisableChargeLine()
    {
        if (lineRenderer == null) return;

        lineRenderer.enabled = false;
    }

    public void DrawChargeCursosLine(Vector3 screenStart, Vector3 screenEnd)
    {
        if (lineRenderer == null) return;
        lineRenderer.enabled = true;
        // Convert screen space to world space
        screenStart.z = Mathf.Abs(Camera.main.transform.position.z);
        screenEnd.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(screenStart);
        Vector3 worldEnd = Camera.main.ScreenToWorldPoint(screenEnd);

        lineRenderer.SetPosition(0, worldStart);
        lineRenderer.SetPosition(1, worldEnd);

    }
    
    public void PlayShootEffect()
    {
        //shootEffect.Play();
    }
}
