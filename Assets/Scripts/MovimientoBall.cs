using UnityEngine;

public class MovimientoBall : MonoBehaviour
{
    public Transform player;
    public float followDistance = 50f;
    public float moveSpeed = 0.5f;
    public float torqueForce = 0.2f;
    public float noiseScale = 0.02f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 targetOffset = player.forward * followDistance;
        float noiseX = (Mathf.PerlinNoise(Time.time, 0) - 0.5f) * noiseScale;
        float noiseZ = (Mathf.PerlinNoise(0, Time.time) - 0.5f) * noiseScale;
        Vector3 irregularOffset = new Vector3(noiseX, 0, noiseZ);
        Vector3 targetPosition = player.position + targetOffset + irregularOffset;
        Vector3 moveDir = targetPosition - transform.position;
        moveDir.y = 0;

        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.VelocityChange);

        Vector3 torqueDir = Vector3.Cross(Vector3.up, moveDir.normalized);
        rb.AddTorque(torqueDir * torqueForce);
    }
}