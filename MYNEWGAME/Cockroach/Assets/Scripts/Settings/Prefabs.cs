using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Settings/Prefabs")]
public class Prefabs : ScriptableObject
{
    public Cockroach Cockroach;
    public List<Food> Foods;
    public List<Blob> Blob; 
}
