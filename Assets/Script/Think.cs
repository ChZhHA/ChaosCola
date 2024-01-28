using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Think : MonoBehaviour
{
    private Animator animator;
    public CharacterState state;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public enum CharacterState
    {
        Default,
        Shock,
        Chaos,
        Doubt,
        Happy
    }

    public void OnAnimationEnd()
    {
        state = CharacterState.Default;
    }

    public void ShowThink(CharacterState state)
    {
        if (this.state != CharacterState.Default)
        {
            return;
        }

        switch (state)
        {
            case CharacterState.Shock:
                animator.SetTrigger("shock");
                break;
            case CharacterState.Chaos:
                animator.SetTrigger("chaos");
                break;
            case CharacterState.Doubt:
                animator.SetTrigger("doubt");
                break;
            case CharacterState.Happy:
                animator.SetTrigger("happy");
                break;
            default:
                break;
        }

        this.state = state;
    }
}