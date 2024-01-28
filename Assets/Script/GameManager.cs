using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> characterList;
    public GameObject cokeBin;

    public Character currentGuest;
    private ColaBin _cola;
    private AudioSource _audioSource;

    public int life = 3;
    public float score;
    private float _lastScoreTime;
    private bool _inScore;
    public float targetScore = 1000000;
    public int gameTimes = 0;
    private float lastMissTime;

    public bool gameStarted = false;

    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            return;
        }

        if (life <= 0) return;
        var player = PlayerController.instance;
        if (currentGuest == null)
        {
            var prefab = characterList[Random.Range(0, characterList.Count)];
            var obj = Instantiate(prefab);
            currentGuest = obj.GetComponent<Character>();
            StartCoroutine(showBin());
        }
        else if (_cola != null)
        {
            if ((currentGuest.needCoca && player.isPepsi)
                || (!currentGuest.needCoca && player.isCoca))
            {
                if (!_inScore)
                {
                    _inScore = true;
                    _lastScoreTime = Time.time;
                }

                if (player.colaProgress <= 330)
                {
                    score += 10000 * Mathf.Pow((Time.time - _lastScoreTime) / 5, 2) * Time.deltaTime;
                }

                if (_cola != null && currentGuest.isWatching() && lastMissTime + 2 < Time.time)
                {
                    currentGuest.ShowThink(Think.CharacterState.Shock);
                    life--;
                    lastMissTime = Time.time;
                }
            }
            else
            {
                if (player.colaProgress <= 330 && player.isPepsi || player.isCoca)
                {
                    score += Time.deltaTime;
                }

                _inScore = false;
            }

            if (player.isPepsi || player.isCoca)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.time = 8f * PlayerController.instance.colaProgress / 330;
                    _audioSource.Play();
                }
            }
            else
            {
                _audioSource.Stop();
            }
        }

        if (life <= 0)
        {
            EndGame();
            _audioSource.Stop();

        }
    }

    public void EndGame()
    {
        Door.instance.Close();
        StartCoroutine(waitForEndGame());
    }

    IEnumerator waitForEndGame()
    {
        yield return new WaitForSeconds(1f);
        Destroy(currentGuest.gameObject);
        currentGuest = null;
        Destroy(_cola.gameObject);
        _cola = null;
    }

    public void Restart()
    {
        StartCoroutine(waitForRestart());
        gameTimes++;
    }

    IEnumerator waitForRestart()
    {
        life = 3;
        gameStarted = false;
        yield return new WaitForSeconds(1f);
        Door.instance.Open(true);
        score = 0;
    }

    public void NextGuest()
    {
        Destroy(currentGuest.gameObject);
        currentGuest = null;
    }

    public void DoorOpen()
    {
        gameStarted = true;
    }

    IEnumerator showBin()
    {
        yield return new WaitForSeconds(1.5f);
        var obj = Instantiate(cokeBin);
        _cola = obj.GetComponent<ColaBin>();
        _cola.isCoca = currentGuest.needCoca;
        _cola.GetComponent<Animator>().SetTrigger("ready");
    }
}