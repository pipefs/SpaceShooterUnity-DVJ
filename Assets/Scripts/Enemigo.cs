using System.Collections;
using UnityEngine;

public enum EnemyType
{
    ASTEROIDE,
    NAVE
}

public class Enemigo : MonoBehaviour
{
    [SerializeField] private EnemyType tipoEnemigo = EnemyType.ASTEROIDE; // Asteoride por defecto
    [SerializeField] private GameObject[] powerUpPrefabArray;

    [Header("Asteroide")]
    [SerializeField] private float velocidadCaida = 3f;

    [Header("Nave")]
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadBala = 12f;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (tipoEnemigo == EnemyType.ASTEROIDE)
            rb.linearVelocity = new Vector2(Mathf.Sin(Time.time), -velocidadCaida);
        else if (tipoEnemigo == EnemyType.NAVE)
            StartCoroutine(NaveEnemiga());
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private IEnumerator NaveEnemiga()
    {
        // Entra
        while (transform.position.y > 4.2f)
        {
            Vector2 destino = new Vector2(transform.position.x, 4f);
            transform.position = Vector2.Lerp(transform.position, destino, Time.deltaTime * 1f);
            yield return null;
        }

        Debug.Log("Termino de moverse.");

        // Dispara
        int vecesDisparadas = 0;
        int cantidadDisparos = Random.Range(1, 3);

        while (cantidadDisparos >= vecesDisparadas)
        {
            Disparar();
            vecesDisparadas += 1;
            yield return null;
        }

        Debug.Log("Termino de disparar.");
    }

    private void Disparar()
    {
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        bala.GetComponent<Rigidbody2D>().linearVelocity = -transform.up * velocidadBala;
        Destroy(bala, 3f);
    }

    private void OnDestroy()
    {
        int probSpawnPowerUp = 10;

        if (Random.Range(0, 100) <= probSpawnPowerUp)
        {
            Instantiate(powerUpPrefabArray[0], transform.position, transform.rotation);
        }
    }
}
