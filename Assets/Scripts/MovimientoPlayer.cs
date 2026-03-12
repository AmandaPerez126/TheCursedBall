using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPlayer : MonoBehaviour
{
    [SerializeField] private bool UsaGetAxisRaw = true;
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float sensibilidadMouse = 2f;

    private Transform camara;
    private CharacterController controller;

    private float rotX = 0f;

    void Start()
    {
        camara = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        // Cursor siempre visible y libre
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        Movimiento();
        RotacionCamara();
    }

    void Movimiento()
    {
        float ValorHorizontal = UsaGetAxisRaw ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float ValorVertical = UsaGetAxisRaw ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");

        Vector3 direccion = transform.forward * ValorVertical + transform.right * ValorHorizontal;

        controller.Move(direccion.normalized * velocidad * Time.deltaTime);
    }

    void RotacionCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        transform.Rotate(Vector3.up * mouseX);

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        camara.localEulerAngles = new Vector3(rotX, 0, 0);
    }
}