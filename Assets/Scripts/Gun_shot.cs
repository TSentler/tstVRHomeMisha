using UnityEngine;
using UnityEngine.InputSystem;

public class Gun_shot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePoint;
    public float Speed = 20f;
    public float LifeTime = 6f;
    public float BulletSize = 2f;

    public AudioClip clip;
    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {   
        if (Keyboard.current[Key.U].wasPressedThisFrame)

        {
            FireBullet();
        }
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        bullet.transform.localScale = Vector3.one * BulletSize;
        Rigidbody rigidbody = bullet.GetComponentInChildren<Rigidbody>();

        rigidbody.linearVelocity = FirePoint.forward * Speed;

        source.PlayOneShot(clip);

        Destroy(bullet, LifeTime);
    }

}
