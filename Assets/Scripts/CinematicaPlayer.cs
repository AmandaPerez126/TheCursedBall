using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AutoWalkAndLoad : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;  // Velocidad del player andando 
    [Header("Cinemática")]
    public float duracionCinematica = 20f;  // Tiempo en segundos que dura la cinemática

    private float tiempo = 0f;

    void Update()
    {
        // Mueve player hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        // Cuenta el tiempo
        tiempo += Time.deltaTime;

        // Cuando el tiempo supera la duración, cambia de escena
        if (tiempo >= duracionCinematica)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}