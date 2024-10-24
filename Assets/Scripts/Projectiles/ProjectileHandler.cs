using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour, IDamageDealer
{
    [Header("Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private float lifeSpan;

    private Vector3 lastDirection;

    private void Start()
    {
        Destroy(gameObject,lifeSpan);
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        Vector3 movementDirection;

        if (target == null)
        {
            if(lastDirection == Vector3.zero)
            {
                Destroy(gameObject);
                return;
            }

            movementDirection = lastDirection;
        }
        else
        {
            movementDirection = target.position - transform.position;
            movementDirection.Normalize();
            lastDirection = movementDirection;
        }

        Vector3 bulletMovementVector = speed * Time.deltaTime * movementDirection;
        transform.Translate(bulletMovementVector);
    }

    public void SetTarget(Transform target) => this.target = target;
    public void SetDamage(int damage) => this.damage = damage;
    public void SetSpeed(float speed) => this.speed = speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != target) return;

        if (other.TryGetComponent(out IHasHealth iHasHealth))
        {
            DealDamage(iHasHealth);
        }

        Destroy(gameObject);
    }

    public int GetDamage() => damage;

    public void DealDamage(IHasHealth iHasHealth) => iHasHealth.TakeDamage(damage);
}
