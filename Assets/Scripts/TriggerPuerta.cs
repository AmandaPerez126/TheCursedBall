using UnityEngine;

//Reproduce sonido al entrar el jugador al trigger
public class TriggerPuerta : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasBeenTriggered = true;

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirPuerta();

        Destroy(gameObject); //El trigger se elimina tras su uso
    }
}