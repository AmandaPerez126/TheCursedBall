using UnityEngine;

//Controla el movimiento y la cámara en primera persona.
public class MovimientoPlayer : MonoBehaviour
{
    [SerializeField] private bool UsaGetAxisRaw = true; // Movimiento más brusco o suave
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float sensibilidadMouse = 2f;

    private Transform camara;
    private CharacterController controller;
    private float rotX = 0f;

    void Start()
    {
        camara = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        // El cursor es visible y libre
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

        // Rotación horizontal del cuerpo
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical de la cámara, con límites
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);
        camara.localEulerAngles = new Vector3(rotX, 0, 0);
    }
}