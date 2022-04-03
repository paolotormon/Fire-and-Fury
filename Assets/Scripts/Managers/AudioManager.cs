using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //METHOD 2: Better approach for Singleton Design Pattern
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            //there is still no instance
            if (instance == null)
            {
                //Check if there is an existing game obj in scene that has the component
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)//did not find any gameobj in the hierarchy
                {
                    //generate our own instance
                    GameObject go = new GameObject();
                    //change default name
                    go.name = "AudioManager";
                    //add component and set it as instance
                    instance = go.AddComponent<AudioManager>();
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
    public void PlaySound(string clipName, float vol = 1.0f)
    {
        //Create gameobj with an AudioController component
        GameObject go = new GameObject();
        go.AddComponent<AudioController>();
        go.GetComponent<AudioController>().PlayClip(clipName, vol);
    }

}
