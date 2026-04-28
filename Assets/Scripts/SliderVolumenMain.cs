using UnityEngine;
using UnityEngine.UI;

public class SliderVolumenMain : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null) return;

        // Cargar volumen guardado
        float volumen = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        slider.value = volumen;

        // Conectar evento manual (arrastrar)
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // Aplicar volumen inicial si ManejadorMusica ya existe
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(volumen);
        else
            Debug.LogWarning("ManejadorMusica no encontrado al inicio. El volumen se aplicará cuando esté disponible.");
    }

    void Update()
    {
        float rueda = Input.GetAxis("Mouse ScrollWheel");
        if (rueda != 0)
        {
            float nuevoValor = slider.value + rueda * 0.05f;
            nuevoValor = Mathf.Clamp01(nuevoValor);
            slider.value = nuevoValor;  // Esto dispara el evento OnSliderValueChanged
        }
    }

    private void OnSliderValueChanged(float valor)
    {
        // Guardar en PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", valor);
        PlayerPrefs.Save();

        // Aplicar volumen si ManejadorMusica existe
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(valor);
        else
            Debug.LogWarning("ManejadorMusica no disponible, el volumen se aplicará más tarde.");
    }
}