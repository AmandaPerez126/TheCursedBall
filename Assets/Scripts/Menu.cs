using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("ultimaEscena", nombreEscena);
        PlayerPrefs.Save();
    }

    public void Jugar()
    {
        SceneManager.LoadScene("CinematicaScene");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void AbrirOpciones()
    {
        PlayerPrefs.SetString("ultimaEscena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("OpcionesScene");
    }
}