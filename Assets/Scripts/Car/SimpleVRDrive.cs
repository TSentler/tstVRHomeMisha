using UnityEngine;
using UnityEngine.InputSystem; // если используешь новый Input System

public class SimpleCarController : MonoBehaviour
{
    [Header("Настройки")]
    public float motorForce = 1500f;      // сила двигателя
    public float maxSteerAngle = 30f;    // максимальный угол поворота
    public float brakeForce = 3000f;     // сила торможения

    [Header("Колёса (можно не указывать)")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private bool isBraking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Центр масс чуть ниже — машина стабильнее
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        // === Самый простой ввод (замени на VR контроллеры) ===
        // Горизонталь — поворот, вертикаль — газ/тормоз
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);

        // --- Пример для VR (OpenXR / XR Interaction Toolkit) ---
        // horizontalInput = rightController.thumbstick.x;
        // verticalInput   = rightController.trigger.ReadValue(); // газ
        // isBraking       = leftController.trigger.ReadValue() > 0.5f;
    }

    void FixedUpdate()
    {
        // Движение вперёд / назад
        rb.AddForce(transform.forward * verticalInput * motorForce);

        // Поворот
        currentSteerAngle = maxSteerAngle * horizontalInput;
        transform.Rotate(0, currentSteerAngle * Time.fixedDeltaTime * 2f, 0);

        // Торможение
        if (isBraking)
        {
            rb.linearVelocity *= 0.95f; // простое затухание
        }

        // Вращение колёс (чисто визуально)
        UpdateWheels();
    }

    void UpdateWheels()
    {
        float rotationSpeed = rb.linearVelocity.magnitude * 50f;
        if (frontLeftWheel) frontLeftWheel.Rotate(rotationSpeed, 0, 0);
        if (frontRightWheel) frontRightWheel.Rotate(rotationSpeed, 0, 0);
        if (rearLeftWheel) rearLeftWheel.Rotate(rotationSpeed, 0, 0);
        if (rearRightWheel) rearRightWheel.Rotate(rotationSpeed, 0, 0);

        // Поворот передних колёс
        if (frontLeftWheel)
            frontLeftWheel.localEulerAngles = new Vector3(frontLeftWheel.localEulerAngles.x, currentSteerAngle, 0);
        if (frontRightWheel)
            frontRightWheel.localEulerAngles = new Vector3(frontRightWheel.localEulerAngles.x, currentSteerAngle, 0);
    }
}