using UnityEngine;

public class WeaponAim : MonoBehaviour
{

    private WeaponState weaponState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform weapon; // The actual weapon to rotate
    public Transform centerPivot; // The shoulder or central pivot
    public float radius = 0.5f;   // Distance from center to weapon


    void Start()
    {
        weaponState = GetComponent<WeaponState>();
    }


    private void Update()
    {
        // if (!weaponState.IsCharging())
        // {
        //     AimWeapon();
        // }
        AimWeapon();
    }

    private void AimWeapon()
    {
        if (centerPivot == null) return;

        // Copy mouse position and set correct Z distance
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(Camera.main.transform.position.z); // Distance from camera to world
        //Debug.Log("Mouse screen position: " + mouseScreenPosition);

        // Convert to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // Stay in 2D
        //Debug.Log("Mouse world position: " + mouseScreenPosition);

        // Center also needs to be flat in 2D
        Vector3 centerForDirection = centerPivot.position;
        centerForDirection.z = 0f;

        // Direction from center to mouse
        Vector3 direction = -(mouseWorldPosition - centerForDirection).normalized;
        //Debug.Log("Direction: " + direction);
        // New position along the orbit
        weapon.position = centerPivot.position + direction * radius;


        // Rotaion - Point to goal
        Vector3 aimDirection = -(mouseWorldPosition - centerPivot.position);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmos()
    {
        if (centerPivot == null)
            return;

        // Set color and draw orbit circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centerPivot.position, radius);
    }
}
