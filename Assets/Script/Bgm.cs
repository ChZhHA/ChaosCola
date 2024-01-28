using System;
using UnityEngine;


public class Bgm : MonoBehaviour
{
    public static Bgm instance;
    private AudioSource _audioSource;

    private void Start()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void Update()
    {
        _audioSource.volume = Mathf.Lerp(_audioSource.volume,
            GameManager.instance.gameStarted && GameManager.instance.life > 0 ? 0.5f : 0, Time.deltaTime);
    }
}