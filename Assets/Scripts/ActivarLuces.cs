using UnityEngine;
using System.Collections;

public class ActivarLuces : MonoBehaviour
{
    public Light[] luces;
    public float tiempoEntreLuces = 0.5f;
    private bool activado = false;
    private bool lucesEncendidas = false;

    void Start()
    {
        if (GuardarProgreso.Instancia != null && GuardarProgreso.Instancia.TriggerActivado(gameObject.name))
        {
            activado = true;
            if (!lucesEncendidas)
            {
                foreach (Light l in luces)
                {
                    if (l != null) l.enabled = true;
                }
                lucesEncendidas = true;
            }
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
                ManejadorMusica.Instancia.TriggerLucesEncienden();

            StartCoroutine(EncenderLuces());
        }
    }

    public void ReactivarEstado(bool estado)
    {
        activado = estado;
        if (activado && !lucesEncendidas)
        {
            foreach (Light l in luces)
            {
                if (l != null) l.enabled = true;
            }
            lucesEncendidas = true;
        }
    }

    private IEnumerator EncenderLuces()
    {
        foreach (Light l in luces)
        {
            if (l != null)
            {
                l.enabled = true;
            }
            yield return new WaitForSeconds(tiempoEntreLuces);
        }
        lucesEncendidas = true;
    }
}