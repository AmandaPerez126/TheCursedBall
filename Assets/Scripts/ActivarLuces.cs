using UnityEngine;
using System.Collections;

// Activa las luces del techo al entrar el jugador, una tras otra
public class ActivarLuces : MonoBehaviour
{
    public Light[] luces;
    public float tiempoEntreLuces = 0.5f; //Tiempo entre cada luz
    private bool activado = false;

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;

        // Notifica al manejador de música
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.TriggerLucesEncienden();

        StartCoroutine(EncenderLuces());
    }

    private IEnumerator EncenderLuces()
    {
        foreach (Light l in luces)
        {
            if (l != null) l.enabled = true;
            yield return new WaitForSeconds(tiempoEntreLuces);
        }
        gameObject.SetActive(false); // Desactiva el trigger
    }
}