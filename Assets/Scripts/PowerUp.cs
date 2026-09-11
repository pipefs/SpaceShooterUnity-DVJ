using UnityEngine;

public enum PowerUpTypes
{
    SPEED,
    PROTECTION
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpTypes tipoPowerUp = PowerUpTypes.SPEED; // velocidad por defecto

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // por ahora
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
