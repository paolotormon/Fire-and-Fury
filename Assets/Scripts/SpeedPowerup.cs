using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement pc = collision.gameObject.GetComponent<PlayerMovement>();
            PlayerAttack pa = collision.gameObject.GetComponent<PlayerAttack>();
            pc.IncreaseSpeed();
            Destroy(this.gameObject);
        }
    }
}
