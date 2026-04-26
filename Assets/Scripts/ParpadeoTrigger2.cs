using UnityEngine;
using System.Collections;

public class ParpadeoTrigger2 : MonoBehaviour
{
    public Light luz;
    public float duracionParpadeo = 2f;
    public float minTiempo = 0.05f;
    public float maxTiempo = 0.3f;
    public bool apagarAlFinal = true;

    private bool activado = false;
    private bool parpadeoCompletado = false;

    void Start()
    {
        if (GuardarProgreso.Instancia != null && GuardarProgreso.Instancia.TriggerActivado(gameObject.name))
        {
            activado = true;
            parpadeoCompletado = true;
            if (luz != null) luz.enabled = !apagarAlFinal;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;

            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.RegistrarTrigger(gameObject.name);

            if (ManejadorMusica.Instancia != null)
                ManejadorMusica.Instancia.ReproducirParpadeo2();

            StartCoroutine(Parpadeo());
        }
    }

    public void ReactivarEstado(bool estado)
    {
        activado = estado;
        if (activado && !parpadeoCompletado)
        {
            parpadeoCompletado = true;
            if (luz != null) luz.enabled = !apagarAlFinal;
        }
    }

    private IEnumerator Parpadeo()
    {
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < duracionParpadeo)
        {
            if (luz != null)
                luz.enabled = !luz.enabled;
            yield return new WaitForSeconds(Random.Range(minTiempo, maxTiempo));
        }

        if (luz != null)
            luz.enabled = !apagarAlFinal;

        parpadeoCompletado = true;
    }
}