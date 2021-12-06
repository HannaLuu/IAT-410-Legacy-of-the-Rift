using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenDonePlayingAudio : MonoBehaviour
{
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
