using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private WeaponState weaponState;
    private WeaponEffects weaponEffects;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;

    public float maxForce = 10f;
    public float minForce = 2f;

    public float forcePerPixel = 0.25f;

    private Vector3 chargeStartScreenPosition;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponState = GetComponent<WeaponState>();
        weaponEffects = GetComponent<WeaponEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BaseLevelController.Instance.IsGamePaused()) { return; }
        
        Vector3 currentMousePosition = Input.mousePosition;
        float chargeForce = CalculateChargeForce(currentMousePosition);

        // Start charging Weapon
        if (Input.GetButton("Fire1"))
        {
            if (!weaponState.IsCharging())
            {
                chargeStartScreenPosition = Input.mousePosition;
                weaponState.StartCharging();
            }
        }

        // Stop charging Weapon -> Fire projectile
        if (Input.GetButtonUp("Fire1") && weaponState.IsCharging())
        {

            // Direction: Following mouse path
            //Vector3 shootDirection = -(chargeEndScreenPosition - chargeStartScreenPosition).normalized;
            Vector3 shootDirection = -firePoint.forward; //.right to shoot on the X axys
            Vector3 spawnPosition = firePoint.position;
            spawnPosition.z = GameManager.Instance.levelPositionZ;

            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, firePoint.rotation);
            projectile.transform.parent = null;
            projectile.GetComponent<Rigidbody>().AddForce(shootDirection * chargeForce, ForceMode.Impulse);

            weaponState.StopCharging();
            //weaponEffects.PlayShootEffect();
        }


        // Actions dependent on WeaponState
        if (weaponState.IsCharging())
        {
            weaponEffects.DrawChargeCursosLine(chargeStartScreenPosition, currentMousePosition);
        }
        else
        {
            weaponEffects.DisableChargeLine();
        }

        //UIManager.Instance.UpdateChargeBar(chargeForce, maxForce);
    }


    private float CalculateChargeForce(Vector3 chargeEndScreenPosition)
    {
        if (!weaponState.IsCharging() || chargeEndScreenPosition == null || chargeStartScreenPosition == null)
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
