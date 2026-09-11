using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaveController : MonoBehaviour
{
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadBala = 12f;

    [SerializeField] private int balasEspeciales = 5;
    [SerializeField] private float anguloTotal = 60f;

    private int puntaje;
    [SerializeField] private TMP_Text puntajeText;

    private Controles controles;
    private Rigidbody2D rb;
    private Vector2 inputMovimiento;

    private void Awake()
    {
        controles = new Controles();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        controles.Nave.Enable();

        controles.Nave.Disparar.performed += ctx => Disparar();
        controles.Nave.Especial.performed += ctx => AtaqueEspecial();
    }
    private void OnDisable() 
    {
        controles.Nave.Disable(); 
    }

    private void Update()
    {
        inputMovimiento = controles.Nave.Mover.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputMovimiento.normalized * velocidad;
    }

    private void Disparar()
    {
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        bala.GetComponent<Rigidbody2D>().linearVelocity = transform.up * velocidadBala;
        bala.GetComponent<Bala>().AsignarJugador(this);
        Destroy(bala, 3f);
    }

    private void AtaqueEspecial()
    {
        float inicio = -anguloTotal / 2f;
        float paso = anguloTotal / (balasEspeciales - 1);

        for (int i = 0; i < balasEspeciales; i++)
        {
            float angulo = inicio + paso * i;
            Quaternion rotacion = puntoDisparo.rotation * Quaternion.Euler(0, 0, angulo);
            GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, rotacion);
            bala.GetComponent<Rigidbody2D>().linearVelocity = rotacion * Vector2.up * velocidadBala;
            bala.GetComponent<Bala>().AsignarJugador(this);
            Destroy(bala, 3f);
        }
    }

    public void ActualizarPuntaje(int nuevoPuntaje)
    {
        puntaje += 1;
        puntajeText.text = $"Puntaje: {puntaje}";
    }

    // Game over
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
