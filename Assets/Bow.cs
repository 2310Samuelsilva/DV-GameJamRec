using Unity.VisualScripting;
using UnityEngine;

public class Bow : MonoBehaviour
{

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject arrowPrefab;
    public float force = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enter"))
        {
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();

            rb.AddForce(firePoint.forward * force, ForceMode.Impulse);
            
        }
    }
}
