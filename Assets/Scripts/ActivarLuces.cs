using UnityEngine;
using System.Collections;

public class ActivarLuces : MonoBehaviour
{
    public Light[] luces;
    public float tiempoEntreLuces = 0.5f;
    private bool activado = false;

    void Start()
    {
        // No hay restauración de estado
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;

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
    }
}