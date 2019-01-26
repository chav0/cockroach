namespace Project.Scripts.Models
{
    public interface IApplicationModel
    {
        int BestScore { get; }
        float Hunger { get; }
        float Thirst { get; }
        void Update();
        void ReportGameOverWithScore(int score);
    }
}