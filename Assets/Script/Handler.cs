using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 range = new Vector2(-60f, -120f);
    private float startRotateY;
    public float progress = 0;
    private void Start()
    {
        startRotateY = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(Mathf.Lerp(range.x, range.y, progress), startRotateY, 0);
    }
}
