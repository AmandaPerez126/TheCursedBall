using UnityEngine;

public class MonstruoIntermitenteTrigger : MonoBehaviour
{
    public GameObject monstruo;
    public float velocidad = 5f;
    public float distanciaRecorridaMax = 22f;
    public float distanciaDeteccion = 2f;

    private Transform jugador;
    private bool activado = false;
    private Vector3 posicionInicial;
    private Quaternion rotacionOriginal;
    private Vector3 escalaOriginal;
    private bool moviendo = false;
    private bool colisionado = false;
    private float distanciaRecorrida = 0f;

    void Start()
    {
        if (monstruo != null)
        {
            posicionInicial = monstruo.transform.position;
            rotacionOriginal = monstruo.transform.rotation;
            escalaOriginal = monstruo.transform.localScale;
            monstruo.SetActive(false);
        }
    }

    void Update()
    {
        if (!activado || !moviendo || colisionado) return;

        Vector3 nuevaPosicion = monstruo.transform.position;
        nuevaPosicion.x -= velocidad * Time.deltaTime;
        monstruo.transform.position = nuevaPosicion;

        distanciaRecorrida += velocidad * Time.deltaTime;

        if (distanciaRecorrida >= distanciaRecorridaMax)
        {
            colisionado = true;
            moviendo = false;
            monstruo.SetActive(false);
            Destroy(monstruo);
            Destroy(gameObject);
        }

        if (jugador != null)
        {
            float distancia = Vector3.Distance(monstruo.transform.position, jugador.position);
            if (distancia <= distanciaDeteccion && !colisionado)
            {
                colisionado = true;
                moviendo = false;
                monstruo.SetActive(false);

                if (ManejadorMusica.Instancia != null)
                    ManejadorMusica.Instancia.ReproducirJumpscare();

                Destroy(monstruo);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;
        jugador = other.transform;
        monstruo.transform.position = posicionInicial;
        monstruo.transform.rotation = rotacionOriginal;
        monstruo.transform.localScale = escalaOriginal;
        monstruo.SetActive(true);
        moviendo = true;
        distanciaRecorrida = 0f;
    }
}