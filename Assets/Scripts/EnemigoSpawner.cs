using UnityEngine;
using System.Collections;

public class EnemigoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private float xMin = -8f, xMax = 8f;
    [SerializeField] private float ySpawn = 6f;
    [SerializeField] private float intervalo = 1.5f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float x = Random.Range(xMin, xMax);
            Instantiate(enemigoPrefab, new Vector2(x, ySpawn), Quaternion.identity);
            yield return new WaitForSeconds(intervalo);
        }
    }
}
