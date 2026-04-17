using UnityEngine;

public class MovimientoBall : MonoBehaviour
{
    public Transform player;
    public float followDistance = 50f;
    public float moveSpeed = 0.5f;      // Velocidad máxima
    public float torqueForce = 0.2f;   // Fuerza de rotación
    public float noiseScale = 0.02f;   // Escala para movimiento irregular

    private Rigidbody rb;
    private Vector3 lastOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastOffset = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Dirección base hacia delante del jugador
        Vector3 targetOffset = player.forward * followDistance;

        // Movimiento irregular usando Perlin Noise
        float noiseX = (Mathf.PerlinNoise(Time.time, 0) - 0.5f) * noiseScale;
        float noiseZ = (Mathf.PerlinNoise(0, Time.time) - 0.5f) * noiseScale;
        Vector3 irregularOffset = new Vector3(noiseX, 0, noiseZ);

        Vector3 targetPosition = player.position + targetOffset + irregularOffset;

        // Vector hacia la posición deseada
        Vector3 moveDir = targetPosition - transform.position;
        moveDir.y = 0; // Mantener en el suelo

        // Mover usando AddForce para física real
        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.VelocityChange);

        // Hacer que la bola ruede naturalmente
        Vector3 torqueDir = Vector3.Cross(Vector3.up, moveDir.normalized);
        rb.AddTorque(torqueDir * torqueForce);
    }
}