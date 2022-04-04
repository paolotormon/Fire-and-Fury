using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float lifetime;
    public void ActivateProjectile()
    {
        lifetime = 0;
        this.gameObject.SetActive(true);
    }
    private void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            this.gameObject.SetActive(false);
    }
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
