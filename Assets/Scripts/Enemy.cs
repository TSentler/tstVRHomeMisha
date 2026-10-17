using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Имя триггера должно совпадать с параметром в Animator Controller
    private static readonly int HitTrigger = Animator.StringToHash("Hit");

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void OnHit()
    {
        animator.SetTrigger(HitTrigger);
    }
}
