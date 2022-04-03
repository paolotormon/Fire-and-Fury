using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    [SerializeField] private float speedVal = 2.0f;
    [SerializeField] private float jumpVal = 5.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.Instance.PlaySound("powerup");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            PlayerMovement pc = collision.gameObject.GetComponent<PlayerMovement>();
            pc.IncreaseStats(speedVal, jumpVal);
        }
    }
}
