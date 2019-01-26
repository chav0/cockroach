using UnityEngine;

namespace Project.Scripts.Views
{
    public interface IGameplayView
    {
        bool IsGameOver { get; }
        float AddFood { get; }
        float AddWater { get; }
        void Update(Vector2 angle);
        void SetDirectionOfPress(int code);
        void ResetWorld();
        void SetPause(bool toTrue);
        void CreateCockroach(Age age);
    }
}