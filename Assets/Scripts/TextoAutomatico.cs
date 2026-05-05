using UnityEngine;
using TMPro;

// Al inicio de MainScene muestra un texto que se desvanece después de unos segundos
public class TextoAutomatico : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float duracionVisible = 3f;
    public float velocidadFade = 1f;

    private CanvasGroup canvasGroup;
    private static bool textoMostrado = false; // Compartido entre todas las instancias

    void Start()
    {
        // Si ya se mostró este texto antes, se destruye
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

        textoMostrado = true; // Marca que ya se mostró
        Destroy(gameObject);
    }
}