using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Project.Scripts.Models;
using UnityEngine;

public class GameModel : IApplicationModel
{
    public int BestScore { get; private set; }
    public float Hunger { get; private set; }
    public float Thirst { get; private set; }

    private float _timer;
    private GameSettings _settings; 

    public GameModel(GameSettings settings)
    {
        _settings = settings; 
    }

    public void Update(float addFood, float addWater)
    {
        if (Time.time - _timer > 1f)
        {
            Hunger -= _settings.FoodPerSecond;
            Thirst -= _settings.WaterPerSecond;
        }

        Hunger += addFood;
        Thirst += addWater; 
    }

    public void ReportGameOverWithScore(int score)
    {
        BestScore = BestScore < score ? score : BestScore; 
    }

}
