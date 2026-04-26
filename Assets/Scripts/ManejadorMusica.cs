using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManejadorMusica : MonoBehaviour
{
    public static ManejadorMusica Instancia;

    [Header("Audios de fondo por escena")]
    public AudioClip musicaMenu;
    public AudioClip musicaCinematica1;
    public AudioClip musicaCinematica2;
    public AudioClip musicaCinematica3;
    public AudioClip musicaAmbiente;

    [Header("Sonidos MainScene")]
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

    private AudioSource fuenteMenuYOAmbiente;
    private AudioSource fuenteCinematica1;
    private AudioSource fuenteCinematica2;
    private AudioSource fuenteCinematica3;
    private AudioSource fuenteEfectos;
    private Slider sliderVolumen;

    private string escenaActual = "";

    void Awake()
    {
        if (Instancia != null)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);

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
        float vol = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        AplicarVolumen(vol);

        escenaActual = SceneManager.GetActiveScene().name;
        CambiarMusica(escenaActual);
    }

    void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        escenaActual = escena.name;
        CambiarMusica(escena.name);
    }

    void Update()
    {
        if (escenaActual == "OpcionesScene" && sliderVolumen == null)
        {
            BuscarSliderEnEscena();
        }
    }

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

    void CambiarMusica(string nombreEscena)
    {
        fuenteMenuYOAmbiente.Stop();
        fuenteCinematica1.Stop();
        fuenteCinematica2.Stop();
        fuenteCinematica3.Stop();

        if (nombreEscena == "MenuScene")
        {
            if (musicaMenu != null)
            {
                fuenteMenuYOAmbiente.clip = musicaMenu;
                fuenteMenuYOAmbiente.Play();
            }
        }
        else if (nombreEscena == "OpcionesScene")
        {
            if (musicaMenu != null)
            {
                fuenteMenuYOAmbiente.clip = musicaMenu;
                fuenteMenuYOAmbiente.Play();
            }
        }
        else if (nombreEscena == "CinematicaScene")
        {
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
            if (musicaAmbiente != null)
            {
                fuenteMenuYOAmbiente.clip = musicaAmbiente;
                fuenteMenuYOAmbiente.Play();
            }
        }
    }

    void AplicarVolumen(float v)
    {
        if (fuenteMenuYOAmbiente != null)
            fuenteMenuYOAmbiente.volume = v;

        if (fuenteCinematica1 != null)
            fuenteCinematica1.volume = v;

        if (fuenteCinematica2 != null)
            fuenteCinematica2.volume = v;

        if (fuenteCinematica3 != null)
            fuenteCinematica3.volume = v;

        if (fuenteEfectos != null)
            fuenteEfectos.volume = v;
    }

    void CambiarVolumen(float v)
    {
        AplicarVolumen(v);
        PlayerPrefs.SetFloat("MusicVolume", v);
        PlayerPrefs.Save();
    }

    public void TriggerLucesEncienden()
    {
        if (lucesEncienden != null)
            fuenteEfectos.PlayOneShot(lucesEncienden);
    }

    public void TriggerObjetoCaer()
    {
        if (objetoCaeSuelo != null)
            fuenteEfectos.PlayOneShot(objetoCaeSuelo);
    }

    public void TriggerLucesApagan()
    {
        if (lucesApagan != null)
            fuenteEfectos.PlayOneShot(lucesApagan);
    }

    public void ReproducirJumpscare()
    {
        if (sonidoJumpscare != null)
            fuenteEfectos.PlayOneShot(sonidoJumpscare);
    }

    public void ReproducirLatidos()
    {
        if (sonidoLatidos != null)
            fuenteEfectos.PlayOneShot(sonidoLatidos);
    }

    public void ReproducirSusurro()
    {
        if (sonidoSusurro != null)
            fuenteEfectos.PlayOneShot(sonidoSusurro);
    }

    public void ReproducirPuerta()
    {
        if (sonidoPuerta != null)
            fuenteEfectos.PlayOneShot(sonidoPuerta);
    }

    public void ReproducirParpadeo()
    {
        if (sonidoParpadeo1 != null)
            fuenteEfectos.PlayOneShot(sonidoParpadeo1);
    }

    public void ReproducirParpadeo1()
    {
        if (sonidoParpadeo1 != null)
            fuenteEfectos.PlayOneShot(sonidoParpadeo1);
    }

    public void ReproducirParpadeo2()
    {
        if (sonidoParpadeo2 != null)
            fuenteEfectos.PlayOneShot(sonidoParpadeo2);
    }

    public void ReproducirSustoFinal()
    {
        if (sonidoSustoFinal != null)
            fuenteEfectos.PlayOneShot(sonidoSustoFinal);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }
}