using UnityEngine;
using System.Collections;

//luz comienza a parpadear durante un tiempo determinado

public class ParpadeoTrigger1 : MonoBehaviour
{
    public Light luz;
    public float duracionParpadeo = 2f;
    public float minTiempo = 0.05f;
    public float maxTiempo = 0.3f;
    public bool apagarAlFinal = true; //Si true, la luz queda apagada

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirParpadeo1();

        StartCoroutine(Parpadeo());
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
    }
}