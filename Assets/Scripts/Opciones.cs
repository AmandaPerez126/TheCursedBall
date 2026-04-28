using UnityEngine;
using UnityEngine.SceneManagement;

public class Opciones : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}