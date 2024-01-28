using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public List<Sprite> emojis;
    public List<Image> emoji;
    private int _lastLife;
    private Animator _animator;

    private void Start()
    {
        _lastLife = GameManager.instance.life;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var life = GameManager.instance.life;
        if (life < _lastLife)
        {
            _animator.SetTrigger("hurt");
            _lastLife = life;
        }

        for (int i = 0; i < emoji.Count; i++)
        {
            if (i >= life)
            {
                emoji[i].gameObject.SetActive(false);
            }
            else
            {
                emoji[i].gameObject.SetActive(true);
                emoji[i].sprite = emojis[life - 1];
            }
        }
    }
}