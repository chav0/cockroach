using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Project.Scripts.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GamePlayView : IGameplayView
{
    private List<Cockroach> _cockroaches;
    private List<Food> _foods; 
    private Prefabs _prefabs;
    private GameSettings _gameSettings;
    private Camera _mainCamera; 
    
    public GamePlayView(List<Cockroach> cockroaches, Prefabs prefabs, GameSettings gameSettings, Camera MainCamera)
    {
        _cockroaches = cockroaches;
        _prefabs = prefabs;
        _gameSettings = gameSettings;
        _mainCamera = MainCamera; 
    }

    private void FieldGenerate()
    {
        for (int i = -100; i < 100; i++)
        {
            for (int j = -100; j < 100; j++)
            {
                var prefab = _prefabs.Foods[(int) Random.Range(0, _prefabs.Foods.Count - 1)];
                var food = Object.Instantiate(prefab); 
                food.transform.position = new Vector3(i + Random.value, j + Random.value);
                _foods.Add(food);
            }
        }
    }

    public bool IsGameOver => _cockroaches.Count == 0;
    public float AddFood { get; private set; }
    public float AddWater { get; private set; }

    public void Update(Vector2 angle)
    {
        UpdateCockRoach(angle); 
        
    }

    private void UpdateFood()
    {
        AddFood = 0f;
        AddWater = 0f; 
        for (int i = 0; i < _foods.Count; i++)
        {
            var food = _foods[i]; 
            if (food.isCollisionEnter)
            {
                AddFood += food.FoodPoint;
                AddWater += food.WaterPoint; 
                _foods.RemoveAt(i);
                food.Remove();
            }
        }
    }

    private void UpdateCockRoach(Vector2 angle)
    {
        for (int i = 0; i < _cockroaches.Count; i++)
        {
            var cockroach = _cockroaches[i]; 
            if (Time.time - cockroach.TimeBirth > _gameSettings.YoungAge && cockroach.Age == Age.Young)
            {
                cockroach.ChangeStage(Age.Adult);
            }
            
            if (Time.time - cockroach.TimeBirth > _gameSettings.AdultAge && cockroach.Age == Age.Adult)
            {
                cockroach.ChangeStage(Age.Old);
            }
            
            if (Time.time - cockroach.TimeBirth > _gameSettings.OldAge && cockroach.Age == Age.Old)
            {
                cockroach.Age = Age.Death; 
            }

            if (cockroach.Age == Age.Death)
            {
                cockroach.SetDeath();
                _cockroaches.RemoveAt(i);
            }
            else
            {
                cockroach.SetPosition(angle, _mainCamera.transform.position, AveragePos, _gameSettings.Speed);
            }           
        }

        AveragePos = new Vector2();
        foreach (var cockroach in _cockroaches)
        {
            AveragePos += new Vector2(cockroach.transform.position.x, cockroach.transform.position.y);; 
        }

        AveragePos /= _cockroaches.Count;
        
        var transform = _mainCamera.transform; 
        var delta = angle * _gameSettings.Speed;
        transform.position += new Vector3(delta.x, delta.y);
        transform.position = Vector3.Lerp(transform.position, new Vector3(AveragePos.x, AveragePos.y, transform.position.z), 0.9f); 
    }

    public Vector2 AveragePos { get; private set; }

    public void SetDirectionOfPress(int code)
    {
        throw new System.NotImplementedException();
    }

    public void ResetWorld()
    {
        SceneManager.LoadScene(0); 
        FieldGenerate(); 
    }

    public void SetPause(bool toTrue)
    {
        Time.timeScale = toTrue ? 0f : 1f; 
    }

    public void CreateCockroach(Age age)
    {
        var newCockroach = Object.Instantiate(_prefabs.Cockroach).GetComponent<Cockroach>();
        newCockroach.CockroachCreate(Time.time, Age.Young);
        _cockroaches.Add(newCockroach);
    }
}
