using UnityEngine;

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

    private System.Collections.IEnumerator CaerConRotacion()
    {
        yield return new WaitForSeconds(0.2f);
        Carro.isKinematic = false;
        Quaternion rotInicial = Carro.transform.rotation;
        Quaternion rotFinal = Quaternion.Euler(90f, rotInicial.eulerAngles.y, rotInicial.eulerAngles.z);
        float progreso = 0f;

        while (progreso < 1f)
        {
            progreso += Time.deltaTime * 1f;
            Carro.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, progreso);
            yield return null;
        }
    }
}