using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.OnHitTarget(transform);
                BaseLevelController.Instance.TargetHit(this); 
            }

            
        }
    }

}