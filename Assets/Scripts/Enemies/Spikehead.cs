using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float checkDelay;
    private float checkTimer;

    private Vector3 destination;

    private bool attacking;

    private Vector3[] directions = new Vector3[4];
    private void Update()
    {
        if (attacking)//Move towards player
        {
            this.transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void OnEnable()
    {
        Stop();//make sure it starts at idle
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Check if spikehead sees player in any of 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
    private void Stop()
    {
        destination = transform.position;//set destination as current position
        attacking = false;

    }
}
