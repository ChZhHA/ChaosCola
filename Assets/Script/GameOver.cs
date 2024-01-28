using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    private Animator _animator;

    private void Start()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("gameOver", GameManager.instance.life <= 0);
    }
}