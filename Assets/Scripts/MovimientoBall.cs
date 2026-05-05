using UnityEngine;
using UnityEngine.UIElements;

//La pelota sigue al jugador por delante de forma irregular aplicando fuerzas físicas
public class MovimientoBall : MonoBehaviour
{
    public Transform player;
    public float followDistance = 50f;
    public float moveSpeed = 0.5f;
    public float torqueForce = 0.2f; // Fuerza de torsión para hacerla girar
    public float noiseScale = 0.02f; //Escala del ruido para movimiento irregular


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Calcula una posición detrás del jugador más un offset aleatorio
        Vector3 targetOffset = player.forward * followDistance;
        float noiseX = (Mathf.PerlinNoise(Time.time, 0) - 0.5f) * noiseScale;
        float noiseZ = (Mathf.PerlinNoise(0, Time.time) - 0.5f) * noiseScale;
        Vector3 irregularOffset = new Vector3(noiseX, 0, noiseZ);
        Vector3 targetPosition = player.position + targetOffset + irregularOffset;
        Vector3 moveDir = targetPosition - transform.position;
        moveDir.y = 0;

        // Aplica fuerza para mover la bola
        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.VelocityChange);

        // Aplica torque para que ruede de manera natural
        Vector3 torqueDir = Vector3.Cross(Vector3.up, moveDir.normalized);
        rb.AddTorque(torqueDir * torqueForce);
    }
}