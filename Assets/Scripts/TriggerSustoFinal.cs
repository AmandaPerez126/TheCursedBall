using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSustoFinal : MonoBehaviour
{
    private bool activado = false;

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;

        if (GuardarProgreso.Instancia != null)
            GuardarProgreso.Instancia.RegistrarTrigger(gameObject.name);

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirSustoFinal();

        if (GuardarProgreso.Instancia != null)
            GuardarProgreso.Instancia.Limpiar();

        SceneManager.LoadScene("MenuScene");
    }
}