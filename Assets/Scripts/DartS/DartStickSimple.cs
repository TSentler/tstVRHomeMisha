using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class DartStickSimple : MonoBehaviour
{
    public float minSpeed = 1.5f;   // медленнее Ч просто отскочит

    private Rigidbody rb;
    private XRGrabInteractable grab;
    private FixedJoint joint;
    private bool isStuck;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnEnable() => grab.selectEntered.AddListener(OnGrabbed);
    void OnDisable() => grab.selectEntered.RemoveListener(OnGrabbed);

    void OnCollisionEnter(Collision collision)
    {
        if (isStuck) return;
        if (grab.isSelected) return;

        // в момент удара сво€ скорость уже погашена Ч берЄм из контакта
        if (collision.relativeVelocity.magnitude < minSpeed) return;

        isStuck = true;

        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = collision.rigidbody;   // null = прибить к миру
        joint.enableCollision = false;
        joint.breakForce = Mathf.Infinity;
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!isStuck) return;

        isStuck = false;

        Destroy(joint);
        joint = null;
    }
}