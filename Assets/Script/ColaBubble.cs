using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaBubble : MonoBehaviour
{
    public bool isCoca;
    // Start is called before the first frame update
    void Start()
    {
        if (isCoca)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }        
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
