using UnityEngine;
using UnityEngine.UI;

//Control para el volumen en la escena de opciones
public class SliderVolumenOpciones : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null) return;

        float volumen = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        slider.value = volumen;

        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnVolumenCambiado);

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(volumen);
    }

    private void OnVolumenCambiado(float valor)
    {
        PlayerPrefs.SetFloat("MusicVolume", valor);
        PlayerPrefs.Save();

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(valor);
    }
}