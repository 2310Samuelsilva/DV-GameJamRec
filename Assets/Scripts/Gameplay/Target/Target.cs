using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{

    private bool isEnergized = false;

    [SerializeField] private ParticleSystem energizedEffect;
    [SerializeField] private AudioSource electricSound;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                Energize();
                projectile.OnHitTarget(transform);
                BaseLevelController.Instance.TargetHit(this);
            }
        }
    }

    

    public void Energize()
    {   
        Debug.Log("Energizing target");
        if (isEnergized) return;

        isEnergized = true;

        // Visual feedback
        if (energizedEffect != null)
        {
            Debug.Log("Playing energized effect");
            energizedEffect.Play();
        }

        // Play sound
        if (electricSound != null && !electricSound.isPlaying)
        {
            electricSound.Play();
        }
            

        Debug.Log($"{gameObject.name} was energized!");
    }

    public bool IsEnergized()
    {
        return isEnergized;
    }

}