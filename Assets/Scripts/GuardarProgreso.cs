using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GuardarProgreso : MonoBehaviour
{
    public static GuardarProgreso Instancia;

    private List<string> triggersActivados = new List<string>();
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
            Invoke("CargarEstadoTriggers", 0.1f);
            Invoke("CargarPosicionJugador", 0.1f);
        }
    }

    public void RegistrarTrigger(string nombreTrigger)
    {
        if (!triggersActivados.Contains(nombreTrigger))
        {
            triggersActivados.Add(nombreTrigger);
        }
    }

    public bool TriggerActivado(string nombreTrigger)
    {
        return triggersActivados.Contains(nombreTrigger);
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
        triggersActivados.Clear();
        tienePosicionGuardada = false;
        ultimaEscena = "";
    }

    void CargarEstadoTriggers()
    {
        ActivarLuces[] lucesTriggers = FindObjectsByType<ActivarLuces>(FindObjectsSortMode.None);
        foreach (var trigger in lucesTriggers)
        {
            if (TriggerActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
        }

        DesactivarLuces[] lucesApagarTriggers = FindObjectsByType<DesactivarLuces>(FindObjectsSortMode.None);
        foreach (var trigger in lucesApagarTriggers)
        {
            if (TriggerActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
        }

        CaidaCarro[] carroTriggers = FindObjectsByType<CaidaCarro>(FindObjectsSortMode.None);
        foreach (var trigger in carroTriggers)
        {
            if (TriggerActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
        }

        ParpadeoTrigger1[] parpadeoTriggers1 = FindObjectsByType<ParpadeoTrigger1>(FindObjectsSortMode.None);
        foreach (var trigger in parpadeoTriggers1)
        {
            if (TriggerActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
        }

        ParpadeoTrigger2[] parpadeoTriggers2 = FindObjectsByType<ParpadeoTrigger2>(FindObjectsSortMode.None);
        foreach (var trigger in parpadeoTriggers2)
        {
            if (TriggerActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
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