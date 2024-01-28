using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FontDisplay : MonoBehaviour
{
    public List<Sprite> sprites;
    public int number = 0;

    private void Start()
    {
        var baseObject = transform.GetChild(0).gameObject;
        for (int i = 0; i < 10; i++)
        {
            Instantiate(baseObject, transform).SetActive(false);
        }
    }

    private void Update()
    {
        var numberStr = number.ToString();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (i < numberStr.Length)
            {
                var image = child.GetComponent<Image>();
                child.gameObject.SetActive(true);
                var spriteIndex = int.Parse(numberStr[i].ToString());
                image.sprite = sprites[spriteIndex];
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}