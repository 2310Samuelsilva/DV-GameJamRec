using Unity.VisualScripting;
using UnityEngine;

public class Bow : MonoBehaviour
{

    private BowState bowState;
    private BowEffects bowEffects;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;

    public float maxForce = 10f;
    public float minForce = 2f;

    public float forcePerPixel = 0.25f;

    private Vector3 chargeStartScreenPosition;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bowState = GetComponent<BowState>();
        bowEffects = GetComponent<BowEffects>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentMousePosition = Input.mousePosition;
        float chargeForce = CalculateChargeForce(currentMousePosition);

        // Start charging Bow
        if (Input.GetButton("Fire1"))
        {
            if (!bowState.IsCharging())
            {
                chargeStartScreenPosition = Input.mousePosition;
                bowState.StartCharging();
            }
        }

        // Stop charging Bow -> Fire projectile
        if (Input.GetButtonUp("Fire1") && bowState.IsCharging())
        {

            // Direction: Following mouse path
            //Vector3 shootDirection = -(chargeEndScreenPosition - chargeStartScreenPosition).normalized;
            Vector3 shootDirection = -firePoint.forward; //.right to shoot on the X axys
            Vector3 spawnPosition = firePoint.position;
            spawnPosition.z = GameManager.Instance.levelPositionZ;

            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, firePoint.rotation);
            projectile.transform.parent = null;
            projectile.GetComponent<Rigidbody>().AddForce(shootDirection * chargeForce, ForceMode.Impulse);

            bowState.StopCharging();
        }


        // Actions dependent on BowState
        if (bowState.IsCharging())
        {
            bowEffects.DrawChargeCursosLine(chargeStartScreenPosition, currentMousePosition);
        }
        else
        {
            bowEffects.DisableChargeLine();
        }

        //UIManager.Instance.UpdateChargeBar(chargeForce, maxForce);
    }


    private float CalculateChargeForce(Vector3 chargeEndScreenPosition)
    {
        if (!bowState.IsCharging() || chargeEndScreenPosition == null || chargeStartScreenPosition == null)
        {
            return 0f;

        }

        //Vector3 chargeEndScreenPosition = Input.mousePosition;
        float chargeDistance = Vector3.Distance(chargeStartScreenPosition, chargeEndScreenPosition);
        float chargeForce = Mathf.Clamp(chargeDistance * forcePerPixel, minForce, maxForce);
        //Debug.Log("Calculating charge force: " + chargeDistance + " * " + forcePerPixel + " = " + (chargeDistance * forcePerPixel));
        
        return chargeForce;
    }
}
