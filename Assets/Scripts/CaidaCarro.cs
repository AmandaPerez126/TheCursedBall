using UnityEngine;
using System.Collections;

// El carro se vuelve físico y hace caer carro hacia el jugador
public class CaidaCarro : MonoBehaviour
{
    public Rigidbody Carro;
    public float rotacionObjetivoX = 90f; //Rotacion final en X
    public float velocidadRotacion = 90f; //Velocidad de rotación
    public float tiempoAntesCaida = 0.2f; //Pausa antes de caer
    private bool activado = false;

    void Start()
    {
        if (Carro != null)
        {
            //// Inicialmente el carro no responde a física
            Carro.isKinematic = true;
            Carro.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;
        if (Carro == null) return;

        activado = true;
        // Sonido
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.TriggerObjetoCaer();

        StartCoroutine(CaerConRotacion());
    }

    //Activa gravedad y rota el carro
    private IEnumerator CaerConRotacion()
    {
        yield return new WaitForSeconds(tiempoAntesCaida);
        //Activa físicas
        Carro.isKinematic = false;
        Carro.useGravity = true;
        //calcula rotación final
        Quaternion rotInicial = Carro.transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(rotacionObjetivoX, rotInicial.eulerAngles.y, rotInicial.eulerAngles.z);
        float duracion = rotacionObjetivoX / velocidadRotacion;
        float tiempo = 0f;

        // Interpola la rotación suavemente
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;
            Carro.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, progreso);
            yield return null;
        }

        gameObject.SetActive(false); //Desactiva el trigger
    }
}