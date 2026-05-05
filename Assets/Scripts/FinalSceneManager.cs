using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

//Muestra fade con frases finales y regresa a MenuScene reiniciando
public class FinalSceneManager : MonoBehaviour
{
    public TextMeshProUGUI textoFinal;
    public string[] frases;
    public float tiempoEntreFrases = 2f;

    void Start()
    {
        // Reproduce música
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.ReproducirMusicaFinal();

        textoFinal.text = "";
        StartCoroutine(MostrarFrasesYMenu());
    }

    IEnumerator MostrarFrasesYMenu()
    {
        for (int i = 0; i < frases.Length; i++)
        {
            textoFinal.text = frases[i];
            yield return new WaitForSeconds(tiempoEntreFrases);
        }

        // Detiene toda la música antes de cambiar de escena
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.DetenerTodosLosSonidos();

        SceneManager.LoadScene("MenuScene");
    }
}