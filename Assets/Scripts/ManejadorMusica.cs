// Gestiona toda la música y efectos de sonido del juego
// Persiste entre escenas usando Singleton
// Controla el volumen general y reproduce clips según la escena que esté activa

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManejadorMusica : MonoBehaviour
{
    // Instancia para acceso global
    public static ManejadorMusica Instancia;

    // Música Escenas
    public AudioClip musicaMenu;
    public AudioClip musicaCinematica1;
    public AudioClip musicaCinematica2;
    public AudioClip musicaCinematica3;
    public AudioClip musicaAmbiente;
    public AudioClip musicaFinal;

    // Clips de triggers
    public AudioClip lucesEncienden;
    public AudioClip objetoCaeSuelo;
    public AudioClip lucesApagan;
    public AudioClip sonidoJumpscare;
    public AudioClip sonidoLatidos;
    public AudioClip sonidoSusurro;
    public AudioClip sonidoPuerta;
    public AudioClip sonidoParpadeo1;
    public AudioClip sonidoParpadeo2;
    public AudioClip sonidoSustoFinal;

    // Fuentes de audio para capas
    private AudioSource fuenteMenuYOAmbiente;
    private AudioSource fuenteCinematica1;
    private AudioSource fuenteCinematica2;
    private AudioSource fuenteCinematica3;
    private AudioSource fuenteEfectos;

    private Slider sliderVolumen;   // Referencia al slider de opciones
    private string escenaActual = "";

    void Awake()
    {
        // Solo una instancia en toda la ejecución
        if (Instancia != null)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);  // No destruir al cargar nuevas escenas

        // Crear y configurar las fuentes
        fuenteMenuYOAmbiente = gameObject.AddComponent<AudioSource>();
        fuenteMenuYOAmbiente.loop = true;
        fuenteMenuYOAmbiente.playOnAwake = false;

        fuenteCinematica1 = gameObject.AddComponent<AudioSource>();
        fuenteCinematica1.loop = true;
        fuenteCinematica1.playOnAwake = false;

        fuenteCinematica2 = gameObject.AddComponent<AudioSource>();
        fuenteCinematica2.loop = true;
        fuenteCinematica2.playOnAwake = false;

        fuenteCinematica3 = gameObject.AddComponent<AudioSource>();
        fuenteCinematica3.loop = true;
        fuenteCinematica3.playOnAwake = false;

        fuenteEfectos = gameObject.AddComponent<AudioSource>();
        fuenteEfectos.loop = false;
        fuenteEfectos.playOnAwake = false;

        SceneManager.sceneLoaded += AlCargarEscena;
    }

    void Start()
    {
        // Cargar volumen guardado
        float vol = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        AplicarVolumen(vol);
        escenaActual = SceneManager.GetActiveScene().name;
        CambiarMusica(escenaActual);
    }

    // Se ejecuta cada vez que se carga una escena
    void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        escenaActual = escena.name;

        // Detener efectos al volver al menú principal
        if (escena.name == "MenuScene" || escena.name == "MainScene")
        {
            if (fuenteEfectos != null)
                fuenteEfectos.Stop();
        }

        CambiarMusica(escena.name);
    }

    void Update()
    {
        // Buscar el slider de volumen cuando estemos en la escena de opciones
        if (escenaActual == "OpcionesScene" && sliderVolumen == null)
            BuscarSliderEnEscena();
    }

    // Encuentra el slider en la escena de opciones y lo configura
    void BuscarSliderEnEscena()
    {
        sliderVolumen = FindFirstObjectByType<Slider>();
        if (sliderVolumen != null)
        {
            float volGuardado = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
            sliderVolumen.minValue = 0f;
            sliderVolumen.maxValue = 1f;
            sliderVolumen.value = volGuardado;
            sliderVolumen.onValueChanged.RemoveAllListeners();
            sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        }
    }

    // Cambia música según el nombre de la escena
    public void CambiarMusica(string nombreEscena)
    {
        if (nombreEscena == "MenuScene")
        {
            // Detener todas las músicas y reproducir la del menú solamente
            fuenteMenuYOAmbiente.Stop();
            fuenteCinematica1.Stop();
            fuenteCinematica2.Stop();
            fuenteCinematica3.Stop();
            if (musicaMenu != null)
            {
                fuenteMenuYOAmbiente.clip = musicaMenu;
                fuenteMenuYOAmbiente.Play();
            }
        }
        else if (nombreEscena == "OpcionesScene")
        {
            // En opciones se usa la misma música del menú
            if (musicaMenu != null)
            {
                fuenteMenuYOAmbiente.clip = musicaMenu;
                fuenteMenuYOAmbiente.Play();
            }
        }
        else if (nombreEscena == "CinematicaScene")
        {
            // Capas de Cinematica
            fuenteMenuYOAmbiente.Stop();
            fuenteCinematica1.Stop();
            fuenteCinematica2.Stop();
            fuenteCinematica3.Stop();
            if (musicaCinematica1 != null)
            {
                fuenteCinematica1.clip = musicaCinematica1;
                fuenteCinematica1.Play();
            }
            if (musicaCinematica2 != null)
            {
                fuenteCinematica2.clip = musicaCinematica2;
                fuenteCinematica2.Play();
            }
            if (musicaCinematica3 != null)
            {
                fuenteCinematica3.clip = musicaCinematica3;
                fuenteCinematica3.Play();
            }
        }
        else if (nombreEscena == "MainScene")
        {
            // Música ambiental de la escena de juego
            fuenteMenuYOAmbiente.Stop();
            fuenteCinematica1.Stop();
            fuenteCinematica2.Stop();
            fuenteCinematica3.Stop();
            if (musicaAmbiente != null)
            {
                fuenteMenuYOAmbiente.clip = musicaAmbiente;
                fuenteMenuYOAmbiente.Play();
            }
        }
    }

    // Aplica el mismo volumen a todas las fuentes
    void AplicarVolumen(float v)
    {
        if (fuenteMenuYOAmbiente != null) fuenteMenuYOAmbiente.volume = v;
        if (fuenteCinematica1 != null) fuenteCinematica1.volume = v;
        if (fuenteCinematica2 != null) fuenteCinematica2.volume = v;
        if (fuenteCinematica3 != null) fuenteCinematica3.volume = v;
        if (fuenteEfectos != null) fuenteEfectos.volume = v;
    }

    // Cambia el volumen global y lo guarda en PlayerPrefs
    public void CambiarVolumen(float v)
    {
        AplicarVolumen(v);
        PlayerPrefs.SetFloat("MusicVolume", v);
        PlayerPrefs.Save();
    }

    // Música de la escena final
    public void ReproducirMusicaFinal()
    {
        if (musicaFinal != null)
        {
            fuenteMenuYOAmbiente.Stop();
            fuenteCinematica1.Stop();
            fuenteCinematica2.Stop();
            fuenteCinematica3.Stop();
            fuenteMenuYOAmbiente.clip = musicaFinal;
            fuenteMenuYOAmbiente.loop = true;
            fuenteMenuYOAmbiente.Play();
        }
    }

    // Detiene todos los sonidos
    public void DetenerTodosLosSonidos()
    {
        if (fuenteMenuYOAmbiente != null) fuenteMenuYOAmbiente.Stop();
        if (fuenteCinematica1 != null) fuenteCinematica1.Stop();
        if (fuenteCinematica2 != null) fuenteCinematica2.Stop();
        if (fuenteCinematica3 != null) fuenteCinematica3.Stop();
        if (fuenteEfectos != null) fuenteEfectos.Stop();
    }

    // Métodos públicos para ejecutar sonidos desde cualquier script
    public void TriggerLucesEncienden()
    {
        if (lucesEncienden != null) fuenteEfectos.PlayOneShot(lucesEncienden);
    }

    public void TriggerObjetoCaer()
    {
        if (objetoCaeSuelo != null) fuenteEfectos.PlayOneShot(objetoCaeSuelo);
    }

    public void TriggerLucesApagan()
    {
        if (lucesApagan != null) fuenteEfectos.PlayOneShot(lucesApagan);
    }

    public void ReproducirJumpscare()
    {
        if (sonidoJumpscare != null) fuenteEfectos.PlayOneShot(sonidoJumpscare);
    }

    public void ReproducirLatidos()
    {
        if (sonidoLatidos != null) fuenteEfectos.PlayOneShot(sonidoLatidos);
    }

    public void ReproducirSusurro()
    {
        if (sonidoSusurro != null) fuenteEfectos.PlayOneShot(sonidoSusurro);
    }

    public void ReproducirPuerta()
    {
        if (sonidoPuerta != null) fuenteEfectos.PlayOneShot(sonidoPuerta);
    }

    public void ReproducirParpadeo1()
    {
        if (sonidoParpadeo1 != null) fuenteEfectos.PlayOneShot(sonidoParpadeo1);
    }

    public void ReproducirParpadeo2()
    {
        if (sonidoParpadeo2 != null) fuenteEfectos.PlayOneShot(sonidoParpadeo2);
    }

    public void ReproducirSustoFinal()
    {
        if (sonidoSustoFinal != null) fuenteEfectos.PlayOneShot(sonidoSustoFinal);
    }

    // Limpieza de sonidos
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }
}