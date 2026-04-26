using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JumpScareFinal : MonoBehaviour
{
    public Image jumpscareImage;
    public float displayDuration = 1.5f;

    private bool hasBeenTriggered = false;

    void Start()
    {
        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTriggered && other.CompareTag("Player"))
        {
            hasBeenTriggered = true;

            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.RegistrarTrigger(gameObject.name);

            StartCoroutine(TriggerJumpScareFinal());
        }
    }

    System.Collections.IEnumerator TriggerJumpScareFinal()
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