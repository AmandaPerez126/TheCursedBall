using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    public Button botonReanudar;

    void Start()
    {
        string escenaGuardada = PlayerPrefs.GetString("ultimaEscena", "");

        if (botonReanudar != null)
        {
            if (escenaGuardada == "MainScene" || escenaGuardada == "CinematicaScene")
            {
                botonReanudar.interactable = true;
            }
            else
            {
                botonReanudar.interactable = false;
            }
        }
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}