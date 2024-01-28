using UnityEngine;


public class ColaBinLid : MonoBehaviour
{
    public void OnClose()
    {
        GetComponentInParent<ColaBin>().OnLidClose();
    }

    public void PlayAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("盖子.wav"));
    }
}