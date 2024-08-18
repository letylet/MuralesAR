using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source; 

    public AudioSource getSource()
    {
        return this.source;
    }

    public void setSource(AudioSource source)
    {
        this.source = source;
    }

    public void playSound()
    {
        source.Play();
    }

    public void stopSound()
    {
        source.Stop();
    }
}
