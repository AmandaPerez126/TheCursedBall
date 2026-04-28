using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinalSceneManager : MonoBehaviour
{
    public TextMeshProUGUI textoFinal;
    public string[] frases;
    public float tiempoEntreFrases = 2f;

    void Start()
    {
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

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.DetenerTodosLosSonidos();

        SceneManager.LoadScene("MenuScene");
    }
}