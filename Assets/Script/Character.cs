using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public bool isWatch;
    public bool needCoca;

    private bool keepState;

    private const float WatchGapMax = 3f;
    private const float WatchSafeTime = 0.5f;

    public GameObject watch;
    public GameObject ignore;
    public ColaBubble bubble;

    public SpriteRenderer cloth;
    public SpriteRenderer hairWatch;
    public SpriteRenderer hairIgnore;

    public List<Sprite> clothList;
    public List<Sprite> hairWatchList;
    public List<Sprite> hairIgnoreList;

    public Think think;

    public float lastChangeStatusTime;
    public float waitTime;

    private static int lastClothIndex = -1;
    private static int lastHairIndex = -1;

    private Animator _animator;

    private int GetRandomClothIndex(List<Sprite> list)
    {
        var index = Random.Range(0, list.Count);
        if (index == lastClothIndex)
        {
            index = (index + 1) % list.Count;
        }

        lastClothIndex = index;
        return index;
    }

    private int GetRandomHairIndex(List<Sprite> list)
    {
        var index = Random.Range(0, list.Count);
        if (index == lastHairIndex)
        {
            index = (index + 1) % list.Count;
        }

        lastHairIndex = index;
        return index;
    }

    private void Awake()
    {
        needCoca = Random.Range(0, 100) > 50;
        isWatch = true;
        lastChangeStatusTime = Time.time;
        waitTime = Random.Range(0.5f, 1f) * WatchGapMax;
        bubble.gameObject.SetActive(false);
        _animator = GetComponent<Animator>();
        cloth.sprite = clothList[GetRandomClothIndex(clothList)];
        var hairIndex = GetRandomHairIndex(hairWatchList);
        hairWatch.sprite = hairWatchList[hairIndex];
        hairIgnore.sprite = hairIgnoreList[hairIndex];
    }

    public bool isWatching()
    {
        if (!isWatch)
            return false;
        return Time.time - lastChangeStatusTime > WatchSafeTime;
    }

    private bool _hasShocked = false;

    public void ShowThink(Think.CharacterState state)
    {
        if (think.state != Think.CharacterState.Default)
        {
            return;
        }

        isWatch = true;
        waitTime += 1f;
        if (state == Think.CharacterState.Shock)
        {
            _animator.SetTrigger("jump");
        }

        think.ShowThink(state);
        if (state == Think.CharacterState.Shock)
        {
            _hasShocked = true;
        }
    }

    public void OnReached()
    {
        bubble.isCoca = needCoca;
        bubble.gameObject.SetActive(true);
    }

    public void OnReceive()
    {
        isWatch = true;
        keepState = true;
        if (!ColaBin.instance.closeLid || PlayerController.instance.colaProgress < 200)
        {
            think.ShowThink(Think.CharacterState.Doubt);
        }
        else if (_hasShocked)
        {
            think.ShowThink(Think.CharacterState.Chaos);
        }
        else
        {
            think.ShowThink(Think.CharacterState.Happy);
        }

        StartCoroutine(DoLeave());
    }

    IEnumerator DoLeave()
    {
        yield return new WaitForSeconds(1.5f);
        _animator.SetTrigger("leave");
    }

    public void OnLeave()
    {
        GameManager.instance.NextGuest();
    }

    private bool canFake = true;

    private void Update()
    {
        if (waitTime > 0f && !keepState)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                isWatch = !isWatch;
                lastChangeStatusTime = Time.time;
                waitTime = Random.Range(0.5f, 1f) * WatchGapMax;
                canFake = Random.Range(0, 1f) > 0.7f;
            }
            else if (waitTime < 1.5f && !isWatch && canFake && Random.Range(0, 1f) < 0.9f)
            {
                canFake = false;
                _animator.SetTrigger("fake");
            }
        }

        _animator.SetBool("watch", isWatch);
    }
}