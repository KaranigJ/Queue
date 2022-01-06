using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] _numberSprites;

    [Header("Images")]
    [SerializeField] private Image _tensImage;
    [SerializeField] private Image _unitsImage;

    public void SetText(int value)
    {
        var tens = value / 10;
        var units = value % 10;

        _tensImage.sprite = _numberSprites[tens];
        _unitsImage.sprite = _numberSprites[units];
    }
}
