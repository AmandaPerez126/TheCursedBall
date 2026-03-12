using UnityEngine;
using UnityEngine.InputSystem;

public class ParpadeoLuces : MonoBehaviour
{
    public Light luz;
    public float minTiempo = 0.05f;  //Mínimo tiempo entre parpadeos
    public float maxTiempo = 0.3f;   //Máximo tiempo entre parpadeos

    void Start()
    {
        StartCoroutine(Parpadear());
    }

    System.Collections.IEnumerator Parpadear()
    {
        while (true)
        {
            luz.enabled = !luz.enabled;
            yield return new WaitForSeconds(Random.Range(minTiempo, maxTiempo));
        }
    }
}