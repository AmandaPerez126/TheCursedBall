using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    public Button Volvermenu;
    void Start()
    {
        string escenaGuardada = PlayerPrefs.GetString("ultimaEscena");
        if (escenaGuardada == "MenuScene")
        {
            Volvermenu.interactable = false;
        }
        else
        {
            Volvermenu.interactable = true;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
