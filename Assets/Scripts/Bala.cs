using UnityEngine;

public class Bala : MonoBehaviour
{
    private NaveController playerRef;

    public void AsignarJugador(NaveController player)
    {
        playerRef = player;
    }

    // Añadir puntaje
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            Destroy(otro.gameObject);
            
            if (playerRef != null)
            {
                playerRef.ActualizarPuntaje(1);
            }
        }
    }
}
