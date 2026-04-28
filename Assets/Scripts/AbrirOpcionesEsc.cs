using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AbrirOpcionesEsc : MonoBehaviour
{
    private bool opcionesAbiertas = false;

    private struct EstadoCamara
    {
        public Camera cam;
        public CameraClearFlags clearFlags;
        public Color backgroundColor;
    }

    private List<EstadoCamara> estadosCamaras = new List<EstadoCamara>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !opcionesAbiertas && SceneManager.GetActiveScene().name == "MainScene")
        {
            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.Guardar();

            estadosCamaras.Clear();
            Camera[] todasLasCamaras = FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (Camera cam in todasLasCamaras)
            {
                EstadoCamara estado = new EstadoCamara();
                estado.cam = cam;
                estado.clearFlags = cam.clearFlags;
                estado.backgroundColor = cam.backgroundColor;
                estadosCamaras.Add(estado);

                cam.clearFlags = CameraClearFlags.SolidColor;
                cam.backgroundColor = Color.black;
            }

            SceneManager.LoadScene("OpcionesScene", LoadSceneMode.Additive);
            opcionesAbiertas = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CerrarOpciones()
    {
        if (!opcionesAbiertas) return;

        SceneManager.UnloadSceneAsync("OpcionesScene");
        opcionesAbiertas = false;

        foreach (EstadoCamara estado in estadosCamaras)
        {
            if (estado.cam != null)
            {
                estado.cam.clearFlags = estado.clearFlags;
                estado.cam.backgroundColor = estado.backgroundColor;
            }
        }
        estadosCamaras.Clear();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}