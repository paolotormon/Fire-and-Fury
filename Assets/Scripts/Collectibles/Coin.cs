using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //private AudioSource audSrc;
    //private void Awake()
    //{
        //audSrc = GetComponent<AudioSource>();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Without singleton you need instance of class to access functions
            //PlayerScore ps = collision.gameObject.GetComponent<PlayerScore>();
            //ps.AddScore(1);

            //With singleton direcho sa clas
            ScoreManager.Instance.AddScore(1);
            //audSrc.Play(); commented out for AudioManager for singleton
            AudioManager.Instance.PlaySound("coin");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
