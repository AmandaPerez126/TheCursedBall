using UnityEngine;
using System.Collections;

public class DesactivarLuces : MonoBehaviour
{
    public Light[] luces;
    public float tiempoEntreLuces = 0.5f;
    private bool activado = false;
    private bool lucesApagadas = false;

    void Start()
    {
        if (GuardarProgreso.Instancia != null && GuardarProgreso.Instancia.TriggerFueActivado(gameObject.name))
        {
            activado = true;
            if (!lucesApagadas)
            {
                foreach (Light l in luces)
                {
                    if (l != null) l.enabled = false;
                }
                lucesApagadas = true;
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
                ManejadorMusica.Instancia.TriggerLucesApagan();

            StartCoroutine(ApagarLuces());
        }
    }

    public void ReactivarEstado(bool estado)
    {
        activado = estado;
        if (activado && !lucesApagadas)
        {
            foreach (Light l in luces)
            {
                if (l != null) l.enabled = false;
            }
            lucesApagadas = true;
        }
    }

    private IEnumerator ApagarLuces()
    {
        foreach (Light l in luces)
        {
            if (l != null)
            {
                l.enabled = false;
            }
            yield return new WaitForSeconds(tiempoEntreLuces);
        }
        lucesApagadas = true;
    }
}