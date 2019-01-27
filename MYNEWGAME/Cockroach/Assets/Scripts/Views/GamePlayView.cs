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
    private int _detailLevel;
    private float _timerDetail; 
    
    public GamePlayView(List<Cockroach> cockroaches, Prefabs prefabs, GameSettings gameSettings, Camera MainCamera)
    {
        _cockroaches = cockroaches;
        _prefabs = prefabs;
        _gameSettings = gameSettings;
        _mainCamera = MainCamera;
        _foods = new List<Food>();
    }

    private void FieldGenerate()
    {
        _detailLevel++; 
        for (int i = -20 * _detailLevel; i < 20 * _detailLevel; i += 10)
        {
            if (i <= -20 * (_detailLevel - 1) || i >= 20 * (_detailLevel - 1))
            {
                for (int j = -20 * _detailLevel; j < 20 * _detailLevel; j += 10)
                {
                    if (j <= -20 * (_detailLevel - 1) || j >= 20 * (_detailLevel - 1))
                    {
                        var prefab = _prefabs.Foods[Mathf.RoundToInt(Random.Range(-0.5f, _prefabs.Foods.Count - 1))];
                        var food = Object.Instantiate(prefab);
                        food.transform.position = new Vector3(i + Random.Range(0f, 5f), j + Random.Range(0f, 5f));
                        _foods.Add(food);
                    }
                }
            }
        }
    }

    public bool IsGameOver => _cockroaches.Count == 0;
    public float AddFood { get; private set; }
    public float AddWater { get; private set; }
    public int CockNum => _cockroaches.Count; 

    public void Update(Vector2 angle)
    {
        if (!IsGameOver)
        {
            UpdateCockRoach(angle);
            UpdateFood();
            UpdatePregnant();

            if (Time.time - _timerDetail > 5f && _detailLevel < 20)
            {
                _timerDetail = Time.time; 
                FieldGenerate();
            }
        }
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

    private void UpdatePregnant()
    {
        for (int i = 0; i < _cockroaches.Count; i++)
        {
            var cockroach = _cockroaches[i];
            if (cockroach.IsPregnant && Time.time - cockroach.TimePregnant > _gameSettings.PregnantPorog)
            {
                CreateCockroach(Age.Adult);
                cockroach.SetPregnant(false, 0f);
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
                DeathMarker = cockroach.Name + " died of old age"; 
            }
            else
            {
                cockroach.SetPosition(angle, _mainCamera.transform.position, AveragePos, _gameSettings.Speed);
            }           
        }

        AveragePos = Vector2.zero;
        foreach (var cockroach in _cockroaches)
        {
            AveragePos += new Vector2(cockroach.transform.position.x, cockroach.transform.position.y);; 
        }

        AveragePos /= _cockroaches.Count;
        
        var transform = _mainCamera.transform; 
        var delta = angle * _gameSettings.Speed;
        transform.position += new Vector3(delta.x, delta.y);
        transform.position = Vector3.Lerp(transform.position, new Vector3(AveragePos.x, AveragePos.y, transform.position.z), 0.01f); 
    }

    public Vector2 AveragePos { get; private set; }

    public void SetDirectionOfPress(int code)
    {
        throw new System.NotImplementedException();
    }

    public void ResetWorld()
    {
        SceneManager.LoadScene(0); 
        _foods.Clear();
        FieldGenerate(); 
    }

    public void SetPause(bool toTrue)
    {
        Time.timeScale = toTrue ? 0f : 1f; 
    }

    public void CreateCockroach(Age age)
    {
        var newCockroach = Object.Instantiate(_prefabs.Cockroach).GetComponent<Cockroach>();
        newCockroach.CockroachCreate(Time.time, age);
        newCockroach.transform.position = new Vector3(AveragePos.x, AveragePos.y) + new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f));
        newCockroach.speedScaler = Random.Range(0.98f, 1.02f);
        newCockroach.transform.localScale = Vector3.one * Random.Range(0.9f, 1.1f);
        newCockroach.lerp = Random.Range(0.85f, 0.95f);
        _cockroaches.Add(newCockroach);
    }

    public void DeathCockroach()
    {
        int randomId = (int)Random.value * (CockNum - 1); 
        _cockroaches[randomId].SetDeath();
        DeathMarker = _cockroaches[randomId].Name + " starve to death";
        _cockroaches.RemoveAt(randomId);
    }

    public void SetPregnant()
    {
        int randomId = (int)Random.value * (CockNum - 1);
        Debug.Log(randomId);
        _cockroaches[randomId].SetPregnant(true, Time.time); 
    }

    public string DeathMarker { get; private set; }
}
