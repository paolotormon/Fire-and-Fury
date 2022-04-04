using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement params")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x) //keep moving
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x) //keep moving
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("moving", true);
        //Make enemy face direction
        float facePosX = Mathf.Abs(initScale.x) * _direction;
        enemy.localScale = new Vector3(facePosX, initScale.y, initScale.z);

        //Move in the direction
        float movePosX = enemy.position.x + Time.deltaTime * _direction * speed;
        enemy.position = new Vector3(movePosX, enemy.position.y, enemy.position.z);
    }
    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        movingLeft = !movingLeft;
    }

}
