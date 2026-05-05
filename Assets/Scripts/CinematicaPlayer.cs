using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// Mueve al jugador automáticamente hacia adelante durante una cinemática y luego cambia de escena
public class CinematicaPlayer : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Cinemática")]
    public float duracionCinematica = 20f; //tiempo hasta cambiar de escena

    private float tiempo = 0f;

    void Update()
    {
        //Avanza hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        tiempo += Time.deltaTime;

        //Cuando termina el tiempo, carga siguiente escena
        if (tiempo >= duracionCinematica)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}