using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class Food : MonoBehaviour
{
    public float FoodPoint;
    public float WaterPoint;

    public bool isCollisionEnter;

    private Sequence _sequence;

    public void Remove()
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOScale(0f, .5f).SetEase(Ease.InBack))
            .AppendCallback(() => Destroy(gameObject));

        _sequence.Play(); 
    }
}
