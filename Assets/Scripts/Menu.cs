using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        if (ManejadorMusica.Instancia != null)
        {
            ManejadorMusica.Instancia.CambiarMusica("MenuScene");
        }
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
        SceneManager.LoadScene("OpcionesScene");
    }
}