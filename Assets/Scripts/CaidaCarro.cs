using UnityEngine;

public class CaidaCarro : MonoBehaviour
{
    public Rigidbody Carro;
    public float rotacionObjetivoX = 90f;
    public float velocidadRotacion = 90f;
    public float tiempoAntesCaida = 0.2f;
    private bool triggerActivado = false;
    private bool haCaido = false;

    void Start()
    {
        if (Carro != null)
        {
            if (GuardarProgreso.Instancia != null && GuardarProgreso.Instancia.TriggerFueActivado(gameObject.name))
            {
                triggerActivado = true;
                haCaido = true;
                Carro.isKinematic = false;
                Carro.transform.rotation = Quaternion.Euler(rotacionObjetivoX, Carro.transform.eulerAngles.y, Carro.transform.eulerAngles.z);
            }
            else
            {
                Carro.isKinematic = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Carro != null && !triggerActivado)
        {
            triggerActivado = true;

            if (GuardarProgreso.Instancia != null)
                GuardarProgreso.Instancia.RegistrarTrigger(gameObject.name);

            if (ManejadorMusica.Instancia != null)
                ManejadorMusica.Instancia.TriggerObjetoCaer();

            StartCoroutine(CaerConRotacion());
        }
    }

    public void ReactivarEstado(bool estado)
    {
        triggerActivado = estado;
        if (triggerActivado && !haCaido && Carro != null)
        {
            haCaido = true;
            Carro.isKinematic = false;
            Carro.transform.rotation = Quaternion.Euler(rotacionObjetivoX, Carro.transform.eulerAngles.y, Carro.transform.eulerAngles.z);
        }
    }

    private System.Collections.IEnumerator CaerConRotacion()
    {
        yield return new WaitForSeconds(tiempoAntesCaida);
        Carro.isKinematic = false;
        Quaternion rotInicial = Carro.transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(rotacionObjetivoX, rotInicial.eulerAngles.y, rotInicial.eulerAngles.z);
        float progreso = 0f;

        while (progreso < 1f)
        {
            progreso += Time.deltaTime * (velocidadRotacion / rotacionObjetivoX);
            Carro.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, progreso);
            yield return null;
        }
        haCaido = true;
    }
}