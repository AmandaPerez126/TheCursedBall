using UnityEngine;
using System.Collections;

//Muestra sprites al entrar el jugador, juntos o en secuencia, yo he elegido secuencia
public class MostrarImagenTrigger : MonoBehaviour
{
    public GameObject[] sprites;
    public float duracionVisible = 2f;
    public bool aparecerEnSecuencia = false; //Se muestran uno tras otro 
    public float tiempoEntreSprites = 0.5f;

    private bool activado = false;

    void Start()
    {
        //todos los sprites empiezan ocultos
        foreach (GameObject sprite in sprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        activado = true;

        if (aparecerEnSecuencia)
            StartCoroutine(MostrarEnSecuencia());
        else
            StartCoroutine(MostrarTodosJuntos());
    }

    // Muestra todos los sprites a la vez
    IEnumerator MostrarTodosJuntos()
    {
        foreach (GameObject sprite in sprites)
        {
            if (sprite != null)
                sprite.SetActive(true);
        }

        yield return new WaitForSeconds(duracionVisible);

        foreach (GameObject sprite in sprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    // Muestra los sprites uno detrás de otro
    IEnumerator MostrarEnSecuencia()
    {
        foreach (GameObject sprite in sprites)
        {
            if (sprite != null)
                sprite.SetActive(true);
            yield return new WaitForSeconds(tiempoEntreSprites);
        }

        yield return new WaitForSeconds(duracionVisible);

        foreach (GameObject sprite in sprites)
        {
            if (sprite != null)
                sprite.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}