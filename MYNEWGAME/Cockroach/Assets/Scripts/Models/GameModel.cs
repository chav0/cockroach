using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Models;
using UnityEngine;

public class GameModel : IApplicationModel
{
    public int BestScore { get; }
    public float Hunger { get; }
    public float Thirst { get; }

    public void ReportGameOverWithScore(int score)
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        
    }
}
