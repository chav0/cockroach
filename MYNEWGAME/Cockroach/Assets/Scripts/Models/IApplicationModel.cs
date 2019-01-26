namespace Project.Scripts.Models
{
    public interface IApplicationModel
    {
        int BestScore { get; }
        int Coins { get; }
        float LastLevelProgress { get; }
        int CurrentLevelReached { get; }
        
        void Update();
        void ReportGameOverWithScore(int score, int coinsCollected);
        void ReportLevelPassWithCScore(int level, int score, int coinsCollected);
    }
}