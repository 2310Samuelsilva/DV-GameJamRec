using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody rb;
    private bool hasHit = false;

    [SerializeField] private float rotationOffset = -90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(DestroyProjectile), 5f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = rb.linearVelocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
        }
    }

    private void DestroyProjectile()
    {
        if (!hasHit)
        {
            Destroy(gameObject);
        }
    }

    public void OnHitTarget(Transform target)
    {
        hasHit = true;
        CancelInvoke(nameof(DestroyProjectile));

        // Stop physics
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Stick to the target
        //transform.SetParent(target);
    }

}
