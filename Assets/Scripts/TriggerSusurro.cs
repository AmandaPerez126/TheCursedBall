using UnityEngine;

//Reproduce sonido al entrar el jugador al trigger
public class TriggerSusurro : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasBeenTriggered = true;

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirSusurro();

        Destroy(gameObject); //El trigger se elimina tras su uso
    }
}