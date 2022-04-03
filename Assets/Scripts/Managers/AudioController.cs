using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource audSrc;
    private void Awake()
    {
        audSrc = GetComponent<AudioSource>();
        audSrc.playOnAwake = false;
    }
    private void Update()
    {
        if (!audSrc.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayClip(string audioName, float vol)
    {
        audSrc.clip = Resources.Load<AudioClip>("Audio/" + audioName);
        audSrc.volume = vol;
        this.gameObject.name = "audioClip_" + audioName;
        audSrc.Play();
    }
}
