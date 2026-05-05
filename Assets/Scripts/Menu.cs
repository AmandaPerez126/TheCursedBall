using UnityEngine;
using UnityEngine.SceneManagement;

//Controla los botones del menú principal
public class Menu : MonoBehaviour
{
    void Start()
    {
        //Música menú
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarMusica("MenuScene");
    }

    public void Jugar()
    {
        SceneManager.LoadScene("CinematicaScene");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void AbrirOpciones()
    {
        SceneManager.LoadScene("OpcionesScene");
    }
}