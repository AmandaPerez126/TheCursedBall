using System;
using UnityEngine;

public class CaidaCarro : MonoBehaviour
{
    public Rigidbody Carro;
    public float rotacionObjetivoX = 90f;
    public float velocidadRotacion = 90f;
    public float tiempoAntesCaida = 0.2f;

    private bool triggerActivado = false;

    private void Start()
    {
        // Asegurarse que el objeto está quieto al inicio
        if (Carro != null)
        {
            Carro.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Carro != null)
        {
            triggerActivado = true;
            //Caida con rotacion
            StartCoroutine(CaerConRotacion());
        }
    }
    private System.Collections.IEnumerator CaerConRotacion()
    {
        // Pequeño retardo si quieres
        yield return new WaitForSeconds(tiempoAntesCaida);

        Carro.isKinematic = false; // Activar gravedad

        // Rotación progresiva
        Quaternion rotInicial = Carro.transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(rotacionObjetivoX, rotInicial.eulerAngles.y, rotInicial.eulerAngles.z);
        float progreso = 0f;

        while (progreso < 1f)
        {
            progreso += Time.deltaTime * (velocidadRotacion / rotacionObjetivoX);
            Carro.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, progreso);
            yield return null;
        }
    }
}