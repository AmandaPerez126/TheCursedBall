using UnityEngine;
using System.Collections;

public class ApagarLucesSecuencia : MonoBehaviour
{
    public Light[] luces;                 // Array con las luces de techo
    public float tiempoEntreLuces = 0.5f; // Tiempo entre apagar cada luz

    // Para que solo se ejecute una vez
    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(ApagarLuces());
        }
    }

    private IEnumerator ApagarLuces()
    {
        foreach (Light l in luces)
        {
            if (l != null)
            {
                l.enabled = false;  // Apagar la luz
                Debug.Log("Luz apagada: " + l.name);
            }
            yield return new WaitForSeconds(tiempoEntreLuces);
        }
    }
}