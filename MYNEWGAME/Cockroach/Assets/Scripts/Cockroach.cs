﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class Cockroach : MonoBehaviour
{
    public string Name;
    public Age Age; 
    public Animator Animator;
    public List<GameObject> Stages; 
    public bool IsPregnant;
    public float TimePregnant;
    public float TimeBirth; 

    private Sequence _sequence;

    public Cockroach(float timeBirth, Age newAge = Age.Young)
    {
        TimeBirth = timeBirth;
        Age = newAge;

        for (var i = 0; i < Stages.Count; i++)
        {
            Stages[i].SetActive(i == (int)newAge);
        }
    }

    public void SetPregnant(bool isPregnant, float timePregnant)
    {
        IsPregnant = isPregnant;
        TimePregnant = timePregnant; 
    }

    public void ChangeStage(Age newAge)
    {
        if (newAge != Age)
        {
            
            if (_sequence != null)
                _sequence.Kill();
            _sequence = DOTween.Sequence(); 
            
            transform.localScale = Vector3.zero;

            Stages[(int)Age].gameObject.SetActive(false);
            Stages[(int)newAge].gameObject.SetActive(true);

            _sequence.Append(transform.DOScale(1f, .25f).SetEase(Ease.OutBack));

            _sequence.Play();

            Age = newAge; 
        }
            
    }
    
    
}

public enum Age
{
    Young = 0,
    Adult = 1,
    Old = 2,
}
