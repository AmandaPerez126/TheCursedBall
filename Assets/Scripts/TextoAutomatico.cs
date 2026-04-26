using UnityEngine;
using TMPro;

public class TextoAutomatico : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float duracionVisible = 3f;
    public float velocidadFade = 1f;

    private CanvasGroup canvasGroup;
    private static bool textoMostrado = false;

    void Start()
    {
        if (textoMostrado)
        {
            Destroy(gameObject);
            return;
        }

        if (texto == null)
            texto = GetComponent<TextMeshProUGUI>();

        canvasGroup = texto.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = texto.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;
        Invoke("ComenzarFade", duracionVisible);
    }

    void ComenzarFade()
    {
        StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }

        textoMostrado = true;
        Destroy(gameObject);
    }
}