using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private Transform previousArea;
    [SerializeField] private Transform nextArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                previousArea.GetComponent<Area>().ActivateArea(false);
                nextArea.GetComponent<Area>().ActivateArea(true);
            }
            else
            {
                previousArea.GetComponent<Area>().ActivateArea(true);
                nextArea.GetComponent<Area>().ActivateArea(false);
            }
        }
    }

}
