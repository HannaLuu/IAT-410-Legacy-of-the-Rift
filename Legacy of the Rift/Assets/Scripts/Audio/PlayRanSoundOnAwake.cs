using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRanSoundOnAwake : MonoBehaviour
{
    public RandomSound randomScript;
    private AudioSource source;

    private void Awake()
    {
        randomScript = GetComponent<RandomSound>();
        source = GetComponent<AudioSource>();

        source.clip = randomScript.GetRandomAudioClip();
        source.Play();
    }
}
