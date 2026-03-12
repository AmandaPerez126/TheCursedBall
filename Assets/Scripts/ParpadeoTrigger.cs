using UnityEngine;
using System.Collections;

public class ParpadeoTrigger : MonoBehaviour
{
    public Light luz;
    public float minTiempo = 0.05f; // Tiempo mínimo entre parpadeos
    public float maxTiempo = 0.3f;  // Tiempo máximo entre parpadeos

    private Coroutine parpadeoCoroutine;

    // Este método se llama desde un trigger
    public void ActivarParpadeo()
    {
        if (parpadeoCoroutine == null)
        {
            parpadeoCoroutine = StartCoroutine(Parpadeo());
        }
    }
    private IEnumerator Parpadeo()
    {
        while (true)
        {
            luz.enabled = !luz.enabled;
            yield return new WaitForSeconds(Random.Range(minTiempo, maxTiempo));
        }
    }
}