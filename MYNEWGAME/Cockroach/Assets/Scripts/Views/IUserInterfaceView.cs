using UnityEngine;

namespace Project.Scripts.Views
{
    public interface IUserInterfaceView
    {
        Vector2 AnglePress { get; }
        bool Pause { get; }
        bool NewGame { get; }
        bool Continue { get; }
        bool Defeat { get; set; }

        void Update(float hunger, float thirst);

        void ShowGameOver();
        void ShowMainMenu();
        void ShowPause();
        void ShowHUD();
        void ShowDeath(string deathMarker); 
    }
}