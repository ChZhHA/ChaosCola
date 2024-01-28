using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaBin : MonoBehaviour
{
    public static ColaBin instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        instance = this;
        lid.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    public List<AudioClip> voiceList;
    public bool isCoca;
    public GameObject cocaCola;
    public GameObject pepsiCola;
    public Animator lid;
    public List<GameObject> waterList;
    public bool closeLid;

    private void Update()
    {
        if (isCoca)
        {
            pepsiCola.SetActive(false);
            cocaCola.SetActive(true);
        }
        else
        {
            pepsiCola.SetActive(true);
            cocaCola.SetActive(false);
        }
    }

    public void Close()
    {
        lid.gameObject.SetActive(true);
        lid.SetTrigger("close");
    }

    public void Send()
    {
        GetComponent<Animator>().SetTrigger("send");
    }

    // public void OnSendEnd()
    // {

    // }

    public void OnReady()
    {
        PlayerController.instance.colaProgress = 0;
    }

    IEnumerator OnSendEnd()
    {
        var position = transform.position;
        var scale = transform.lossyScale;
        var parentScale = GameManager.instance.currentGuest.transform.localScale;
        Destroy(GetComponent<Animator>());
        yield return new WaitForEndOfFrame();
        transform.SetParent(GameManager.instance.currentGuest.transform);
        transform.position = position;
        transform.localScale = new Vector3(scale.x / parentScale.x, scale.y / parentScale.y, scale.z / parentScale.z);

        GameManager.instance.currentGuest.OnReceive();
    }


    public void OnLidClose()
    {
        foreach (var o in waterList.ConvertAll((item) => item))
        {
            if (!ReferenceEquals(null, o) && o != null)
            {
                Destroy(o);
            }
        }

        closeLid = true;
        waterList.Clear();
    }

    public void OnDestroy()
    {
        OnLidClose();
    }

    private float _lastVoiceTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        waterList.Add(other.gameObject);
        _audioSource.PlayOneShot(voiceList[UnityEngine.Random.Range(0, voiceList.Count)]);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        waterList.Remove(other.gameObject);
    }
}