using System;
using UnityEngine;


public class Door : MonoBehaviour
{
    public static Door instance;
    public GameObject gameOver;
    private Animator _animator;
    public bool isOpen;

    private void Start()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }

    public void Open(bool force = false)
    {
        if (!isOpen || force)
        {
            isOpen = true;
            if (GameManager.instance.life <= 0)
            {
                GameManager.instance.Restart();
            }
            else
            {
                _animator.SetBool("open", true);

                GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("开门"));
            }
        }
    }

    public void Close()
    {
        isOpen = false;
        _animator.SetBool("open", false);
    }

    private void Update()
    {
        gameOver.SetActive(GameManager.instance.gameTimes > 0);
    }

    public void OnOpen()
    {
        GameManager.instance.DoorOpen();
    }
}