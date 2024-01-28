using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public FontDisplay fontDisplay;
    public Image emoji;
    public List<Sprite> emojis;

    private void Update()
    {
        var score = GameManager.instance.score;
        var targetScore = GameManager.instance.targetScore;
        fontDisplay.number = Mathf.FloorToInt(score);
        emoji.sprite = emojis[Mathf.Min(Mathf.FloorToInt(score / targetScore * emojis.Count), emojis.Count - 1)];
    }
}