using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirOpcionesEsc : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.GuardarProgresoActual();

            PlayerPrefs.SetString("ultimaEscena", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            SceneManager.LoadScene("OpcionesScene");
        }
    }
}