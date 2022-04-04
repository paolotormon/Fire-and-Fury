using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPos;

    private void Awake()
    {
        //Save init positions of enemies
        initialPos = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initialPos[i] = enemies[i].transform.position;
        }
    }
    public void ActivateArea(bool _status)
    {
        //Activate/Deactivate status
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPos[i];
            }

        }
    }
}
