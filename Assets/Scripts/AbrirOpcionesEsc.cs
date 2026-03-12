using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirOpcionesEsc : MonoBehaviour
{
    void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("ultimaEscena", nombreEscena);
        PlayerPrefs.Save();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Abrimos OpcionesScene
            SceneManager.LoadScene("OpcionesScene");
        }
    }
}