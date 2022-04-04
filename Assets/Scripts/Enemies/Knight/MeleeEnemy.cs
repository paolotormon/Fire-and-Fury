using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    //Refs
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

    }
    private bool PlayerInSight()
    {
        //args
        //1st = origin (flip depending on localscalex)
        //2nd = change size of box
        Vector3 origin = boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 size = new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 size = new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z);

        Gizmos.DrawWireCube(origin, size);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
