using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTriggered && other.CompareTag("Player"))
        {
            hasBeenTriggered = true;

            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.RegistrarTrigger(gameObject.name);

            if (ManejadorMusica.Instancia != null)
                ManejadorMusica.Instancia.ReproducirPuerta();

            Destroy(gameObject);
        }
    }
}