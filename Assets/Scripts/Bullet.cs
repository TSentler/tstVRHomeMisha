using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Для обычного (не Trigger) коллайдера
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.OnHit();
            Destroy(gameObject);
        }
    }

    // Если на коллайдере пули включён Is Trigger
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.OnHit();
            Destroy(gameObject);
        }
    }
}
