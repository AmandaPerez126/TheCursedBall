using UnityEngine;
using System.Collections;

public class ActivarLucesSecuencia : MonoBehaviour
{
    public Light[] luces;         // Array con las luces de techo
    public float tiempoEntreLuces = 0.5f; // Tiempo entre cada luz

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(EncenderLuces());
        }
    }

    private IEnumerator EncenderLuces()
    {
        foreach (Light l in luces)
        {
            if (l != null)
            {
                l.enabled = true;  // Encender la luz
            }
            yield return new WaitForSeconds(tiempoEntreLuces); // Espera de tiempo entre luces 
        }
    }
}