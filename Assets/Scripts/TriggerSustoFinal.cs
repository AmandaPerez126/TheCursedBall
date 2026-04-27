using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerSustoFinal : MonoBehaviour
{
    public Image jumpscareImage;
    public float displayDuration = 1.5f;

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

    private System.Collections.IEnumerator SustoFinal()
    {
        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(true);

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirSustoFinal();

        yield return new WaitForSeconds(displayDuration);

        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(false);

        if (GuardarProgreso.Instancia != null)
            GuardarProgreso.Instancia.Limpiar();

        SceneManager.LoadScene("MenuScene");
    }
}