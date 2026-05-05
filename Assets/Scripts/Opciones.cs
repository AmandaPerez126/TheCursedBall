using UnityEngine;
using UnityEngine.SceneManagement;

//Botón para volver al menú desde la escena de opciones
public class Opciones : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}