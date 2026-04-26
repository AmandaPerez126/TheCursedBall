using UnityEngine;
using UnityEngine.UI;

public class JumpscareTrigger1 : MonoBehaviour
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

            StartCoroutine(TriggerJumpscare());
        }
    }

    System.Collections.IEnumerator TriggerJumpscare()
    {
        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(true);

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirJumpscare();

        yield return new WaitForSeconds(displayDuration);

        if (jumpscareImage != null)
            jumpscareImage.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}