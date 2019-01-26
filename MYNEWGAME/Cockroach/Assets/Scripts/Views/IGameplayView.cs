using UnityEngine;

namespace Project.Scripts.Views
{
    public interface IGameplayView
    {
        bool IsGameOver { get; }

        void Update(Vector2 angle);
        
        Vector2 AveragePos { get; }

        void SetDirectionOfPress(int code);
        void ResetWorld();
        void SetPause(bool toTrue);
        void CreateCockroach(Age age);
    }
}