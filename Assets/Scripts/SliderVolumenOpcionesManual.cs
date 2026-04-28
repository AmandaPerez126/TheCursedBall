using UnityEngine;
using UnityEngine.UI;

public class SliderVolumenOpcionesManual : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null) return;

        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(slider.value);
    }

    private void OnSliderValueChanged(float valor)
    {
        PlayerPrefs.SetFloat("MusicVolume", valor);
        PlayerPrefs.Save();

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(valor);
    }
}