using UnityEngine;

//Reproduce un sonido de latidos al entrar el jugador al trigger
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

        Destroy(gameObject); // El trigger se elimina tras su uso
    }
}