using UnityEngine;
using System.Collections;

public class CaidaCarro : MonoBehaviour
{
    public Rigidbody Carro;
    public float rotacionObjetivoX = 90f;
    public float velocidadRotacion = 90f;
    public float tiempoAntesCaida = 0.2f;
    private bool activado = false;

    void Start()
    {
        if (Carro != null)
        {
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

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.TriggerObjetoCaer();

        StartCoroutine(CaerConRotacion());
    }

    private IEnumerator CaerConRotacion()
    {
        yield return new WaitForSeconds(tiempoAntesCaida);
        Carro.isKinematic = false;
        Carro.useGravity = true;
        Quaternion rotInicial = Carro.transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(rotacionObjetivoX, rotInicial.eulerAngles.y, rotInicial.eulerAngles.z);
        float duracion = rotacionObjetivoX / velocidadRotacion;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;
            Carro.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, progreso);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}