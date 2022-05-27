using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vocals : MonoBehaviour
{
    public static Vocals instance;
    private  AudioSource source;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    public void Say(AudioObject clip)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.PlayOneShot(clip.clip);

        UIsubtitle.instance.SetSubtitle(clip.subtitle,clip.clip.length);
    }

}
