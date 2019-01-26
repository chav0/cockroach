namespace Project.Scripts.Views
{
    public interface IUserInterfaceView
    {
        bool IsLeftPressed { get; }
        bool IsRightPressed { get; }
        bool IsPausePressed { get; }

        void Update();

        void ShowGameOver();
        void ShowNewGame();
        void ShowMainMenu();
        void ShowCharSelect();
    }
}