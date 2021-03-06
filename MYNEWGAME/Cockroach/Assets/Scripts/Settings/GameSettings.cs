﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Settings/Settngs")]
public class GameSettings : ScriptableObject
{
    public float YoungAge = 10f;
    public float AdultAge = 7000000f;
    public float OldAge = 70000f;
    public float Speed = 10f;
    public int StartNum = 1;
    public float FoodPerSecond = 0.01f;
    public float WaterPerSecond = 0.01f;
    public int StartFood = 25;
    public int StartWater = 25;
    public int FoodPerCock = 25;
    public int WaterPerCock = 25;
    public float PregnantPorog = 0.95f;
    public float DeathPorog = 0.2f;
    public float DeathSeconds = 3f;
    public float PregnantIntervals = 5f;
    public float PregnantPeriod = 3f; 
}
