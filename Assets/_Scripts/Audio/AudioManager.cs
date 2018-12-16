using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] clips;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameStateHandler.NormalObstacleSpawned += PlayAsteroidSound;
        GameStateHandler.UfoSpawned += PlayUfoSound;
    }

    private void OnDestroy()
    {
        GameStateHandler.NormalObstacleSpawned -= PlayAsteroidSound;
        GameStateHandler.UfoSpawned -= PlayUfoSound;
    }

    void PlayAsteroidSound()
    {
        source.PlayOneShot(clips[0]);
    }

    void PlayUfoSound()
    {
        source.PlayOneShot(clips[1]);

    }
}
