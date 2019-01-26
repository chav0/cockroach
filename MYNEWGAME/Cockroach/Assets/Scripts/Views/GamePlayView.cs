using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Project.Scripts.Views;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GamePlayView : IGameplayView
{
    private List<Cockroach> _cockroaches;
    private GameObject _cockroachPrefab;
    private GameSettings _gameSettings;
    private Camera _mainCamera; 

    public GamePlayView(List<Cockroach> cockroaches, GameObject prefab, GameSettings gameSettings, Camera MainCamera)
    {
        _cockroaches = cockroaches;
        _cockroachPrefab = prefab;
        _gameSettings = gameSettings;
        _mainCamera = MainCamera; 
    }

    public bool IsGameOver => _cockroaches.Count == 0;

    public void Update(Vector2 angle)
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
        throw new System.NotImplementedException();
    }

    public void SetPause(bool toTrue)
    {
        throw new System.NotImplementedException();
    }

    public void CreateCockroach(Age age)
    {
        var newCockroach = Object.Instantiate(_cockroachPrefab).GetComponent<Cockroach>();
        newCockroach.CockroachCreate(Time.time, Age.Young);
        _cockroaches.Add(newCockroach);
    }
}
