using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardarProgreso : MonoBehaviour
{
    public static GuardarProgreso Instancia;

    private Vector3 posicionJugador;
    private bool tienePosicionGuardada = false;
    public string ultimaEscena = "";

    void Awake()
    {
        if (Instancia != null)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == "MenuScene")
        {
            Limpiar();
        }

        if (tienePosicionGuardada && escena.name == ultimaEscena)
        {
            Invoke("CargarPosicionJugador", 0.1f);
        }
    }

    public void Guardar()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            posicionJugador = jugador.transform.position;
            tienePosicionGuardada = true;
        }
        ultimaEscena = SceneManager.GetActiveScene().name;
    }

    public void Limpiar()
    {
        tienePosicionGuardada = false;
        ultimaEscena = "";
        if (ManejadorMusica.Instancia != null)
        {
            ManejadorMusica.Instancia.DetenerTodosLosSonidos();
        }
    }

    void CargarPosicionJugador()
    {
        if (tienePosicionGuardada)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                jugador.transform.position = posicionJugador;
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}