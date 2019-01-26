namespace Project.Scripts.Views
{
    public interface IGameplayView
    {
        bool IsGameOver { get; }
        int CoinsCollectedInLastGame { get; }
        int ScoreCollectedInLastGame { get; }

        void Update();

        void SetDirectionOfPresss(int code);
        void ResetWorld();
        void SetPause(bool toTrue);
    }
}