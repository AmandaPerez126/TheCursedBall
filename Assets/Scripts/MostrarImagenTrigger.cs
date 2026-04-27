using UnityEngine;
using System.Collections;

public class MostrarImagenTrigger : MonoBehaviour
{
    public GameObject[] sprites;
    public float duracionVisible = 2f;
    public bool aparecerEnSecuencia = false;
    public float tiempoEntreSprites = 0.5f;

    private bool activado = false;

    void Start()
    {
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
    }

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
    }
}