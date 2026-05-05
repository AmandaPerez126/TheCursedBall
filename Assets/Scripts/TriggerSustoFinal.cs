using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

//Al entrar el jugador al trigger, muestra una imagen, reproduce un sonido y luego carga la escena final
public class TriggerSustoFinal : MonoBehaviour
{
    public Image jumpscareImage; // Imagen que aparece en pantalla
    public float displayDuration = 1.5f; // Duración

    private bool activado = false;

    void Start()
    {
        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;
        StartCoroutine(SustoFinal());
    }

    private IEnumerator SustoFinal()
    {
        // Muestra la imagen
        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(true);

        if (ManejadorMusica.Instancia != null)
        {
            ManejadorMusica.Instancia.ReproducirSustoFinal();
            yield return new WaitForSeconds(0.5f);
            ManejadorMusica.Instancia.DetenerTodosLosSonidos();
        }

        yield return new WaitForSeconds(displayDuration - 0.5f);

        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(false);

        SceneManager.LoadScene("FinalScene");
    }
}