using UnityEngine;
using UnityEngine.UI;

//Control para el volumen en MainScene
//Deja ajustar el volumen con la rueda del ratón
public class SliderVolumenMain : MonoBehaviour
{
    private Slider slider; // Referencia al Slider de la UI

    void Start()
    {
        // Obtiene el componente Slider del mismo GameObject
        slider = GetComponent<Slider>();
        if (slider == null) return;

        // Carga el volumen guardado en las preferencias del jugador
        // Si es la primera vez usa 70% como valor por defecto
        float volumen = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        slider.value = volumen;

        // Limpia cualquier evento anterior y da el nuevo
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnVolumenCambiado);

        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(volumen);
    }

    void Update()
    {
        //Cambia el volumen con la rueda del ratón
        // Rueda hacia arriba, sube volumen, rueda hacia abajo, baja volumen
        float rueda = Input.GetAxis("Mouse ScrollWheel");
        if (rueda != 0f)
        {
            float nuevo = slider.value + rueda * 0.05f;
            nuevo = Mathf.Clamp01(nuevo);
            slider.value = nuevo;
        }
    }
    private void OnVolumenCambiado(float valor)
    {
        // Guarda el nuevo volumen
        PlayerPrefs.SetFloat("MusicVolume", valor);
        PlayerPrefs.Save();

        // Actualiza el volumen en el ManejadorMusica
        if (ManejadorMusica.Instancia != null)
            ManejadorMusica.Instancia.CambiarVolumen(valor);
    }
}