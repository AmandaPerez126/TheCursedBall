using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("ultimaEscena",nombreEscena);
        PlayerPrefs.Save();
    }
    // Método para cargar la escena principal del juego
    public void Jugar()
    {
        SceneManager.LoadScene("CinematicaScene");
    }

    // Método para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); // Para ver en la consola mientras estás en el editor
        Application.Quit(); // Cierra app en build final
    }

    // Método para cargar la escena de opciones desde MenuScene o MainScene
    public void AbrirOpciones()
    {
        // Guardamos la escena actual
        PlayerPrefs.SetString("MenuScene", SceneManager.GetActiveScene().name);

        // Cargamos OpcionesScene
        SceneManager.LoadScene("OpcionesScene");
    }
    

}