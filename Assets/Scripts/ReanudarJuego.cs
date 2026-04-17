using UnityEngine;
using UnityEngine.SceneManagement;

public class ReanudarJuego : MonoBehaviour
{
    public void Reanudar()
    {
        string ultimaEscena = PlayerPrefs.GetString("ultimaEscena", "MainScene");
        SceneManager.LoadScene(ultimaEscena);
    }
}