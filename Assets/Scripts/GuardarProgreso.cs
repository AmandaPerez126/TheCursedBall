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
        if (tienePosicionGuardada && escena.name == ultimaEscena)
        {
            CargarEstadoTriggers();
            CargarPosicionJugador();
        }
    }

    public void RegistrarTrigger(string nombreTrigger)
    {
        if (!triggersActivados.Contains(nombreTrigger))
        {
            triggersActivados.Add(nombreTrigger);
        }
    }

    public bool TriggerFueActivado(string nombreTrigger)
    {
        return triggersActivados.Contains(nombreTrigger);
    }

    public void GuardarProgresoActual()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            posicionJugador = jugador.transform.position;
            tienePosicionGuardada = true;
        }
        ultimaEscena = SceneManager.GetActiveScene().name;
    }

    void CargarEstadoTriggers()
    {
        ActivarLuces[] lucesTriggers = FindObjectsByType<ActivarLuces>(FindObjectsSortMode.None);
        foreach (var trigger in lucesTriggers)
        {
            if (TriggerFueActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
        }

        CaidaCarro[] carroTriggers = FindObjectsByType<CaidaCarro>(FindObjectsSortMode.None);
        foreach (var trigger in carroTriggers)
        {
            if (TriggerFueActivado(trigger.gameObject.name))
                trigger.ReactivarEstado(true);
        }

        DesactivarLuces[] lucesApagarTriggers = FindObjectsByType<DesactivarLuces>(FindObjectsSortMode.None);
        foreach (var trigger in lucesApagarTriggers)
        {
            if (TriggerFueActivado(trigger.gameObject.name))
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