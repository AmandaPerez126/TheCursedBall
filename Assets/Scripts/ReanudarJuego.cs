using UnityEngine;
using UnityEngine.SceneManagement;

public class ReanudarJuego : MonoBehaviour
{
    public void Reanudar()
    {
        if (GuardarProgreso.Instancia != null && !string.IsNullOrEmpty(GuardarProgreso.Instancia.ultimaEscena))
        {
            SceneManager.LoadScene(GuardarProgreso.Instancia.ultimaEscena);
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}