using UnityEngine;
using UnityEngine.XR;

public class SimpleCarController : MonoBehaviour
{
    public float motorForce = 1800f;
    public float maxSteerAngle = 35f;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;

    private InputDevice rightHand;
    private InputDevice leftHand;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.6f, 0.2f);

        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        // Стик правого контроллера — поворот
        if (rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stick))
            horizontalInput = stick.x;

        // Триггер правого — газ
        if (rightHand.TryGetFeatureValue(CommonUsages.trigger, out float trigger))
            verticalInput = trigger;

        // Триггер левого — тормоз (просто замедляем)
        if (leftHand.TryGetFeatureValue(CommonUsages.trigger, out float brake) && brake > 0.3f)
            verticalInput = -0.6f; // лёгкий задний ход / торможение
    }

    void FixedUpdate()
    {
        // Движение
        rb.AddForce(transform.forward * verticalInput * motorForce);

        // Поворот
        float steer = horizontalInput * maxSteerAngle;
        transform.Rotate(0, steer * Time.fixedDeltaTime * 2.2f, 0);
    }
}