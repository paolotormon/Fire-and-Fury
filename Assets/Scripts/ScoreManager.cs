using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    //METHOD 2: Better approach for Singleton Design Pattern
    private static ScoreManager instance = null;
    public static ScoreManager Instance
    {
        get
        {
            //there is still no instance
            if (instance == null)
            {
                //Check if there is an existing game obj in scene that has the component
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)//did not find any gameobj in the hierarchy
                {
                    //generate our own instance
                    GameObject go = new GameObject();
                    //change default name
                    go.name = "ScoreManager";
                    //add component and set it as instance
                    instance = go.AddComponent<ScoreManager>();
                    //make sure it persists
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        //set the instance if no copy in the hierarchy
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //if there is copy then destroy
            Destroy(gameObject);
        }
    }


    //METHOD 1: Simplest implementation of Singleton Design Pattern
    /*
    public static ScoreManager instance;
    public void Awake()
    {
        //Should only be 1 instance of class in game
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;//set ourself as the instance
            //Make sure singleton persists
            DontDestroyOnLoad(this.gameObject);
        }
    }*/

    public void AddScore(int value)
    {
        this.score += value;
    }
    public void ResetScore()
    {
        this.score = 0;
    }
    public int GetScore() { return score; }
}
