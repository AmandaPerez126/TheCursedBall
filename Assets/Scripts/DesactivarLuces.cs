using UnityEngine;
using System.Collections;


// Apaga array de luces secuencialmente
public class DesactivarLuces : MonoBehaviour
{
    public Light[] luces;
    public float tiempoEntreLuces = 0.5f;
    private bool activado = false;

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.TriggerLucesApagan();

        StartCoroutine(ApagarLuces());
    }

    private IEnumerator ApagarLuces()
    {
        foreach (Light l in luces)
        {
            if (l != null) l.enabled = false; //Apaga cada luz 
            yield return new WaitForSeconds(tiempoEntreLuces);
        }
        gameObject.SetActive(false);
    }
}