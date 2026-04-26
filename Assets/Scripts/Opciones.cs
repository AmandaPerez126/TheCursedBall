using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    public Button botonReanudar;

    void Start()
    {
        if (botonReanudar != null)
        {
            if (GuardarProgreso.Instancia != null && !string.IsNullOrEmpty(GuardarProgreso.Instancia.ultimaEscena))
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