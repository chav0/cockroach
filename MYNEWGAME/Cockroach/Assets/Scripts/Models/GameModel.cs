using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Project.Scripts.Models;
using UnityEngine;

public class GameModel : IApplicationModel
{
    public int BestScore { get; private set; }
    public float Hunger { get; private set; }
    public float FullHunger { get; private set; }
    public float Thirst { get; private set; }
    public float FullThirst { get; private set; }

    private float _timer;
    private GameSettings _settings; 

    public GameModel(GameSettings settings)
    {
        _settings = settings;
        Hunger = settings.StartFood;
        Thirst = settings.StartWater; 
    }

    public void Update(float addFood, float addWater, int CockNum)
    {
        if (Time.time - _timer > 1f)
        {
            Hunger -= _settings.FoodPerSecond * CockNum;
            Thirst -= _settings.WaterPerSecond * CockNum;
        }

        Hunger += addFood;
        Thirst += addWater;

        /*Hunger = Mathf.Clamp(Hunger, 0f, FullHunger); 
        Thirst = Mathf.Clamp(Thirst, 0f, FullThirst); */

        FullHunger = CockNum * _settings.FoodPerCock;
        FullThirst = CockNum * _settings.WaterPerCock; 
    }

    public void ReportGameOverWithScore(int score)
    {
        BestScore = BestScore < score ? score : BestScore; 
    }

}
