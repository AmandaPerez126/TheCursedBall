using UnityEngine;

public class TriggerLatidos : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasBeenTriggered = true;

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirLatidos();

        Destroy(gameObject);
    }
}