namespace Project.Scripts.Models
{
    public interface IApplicationModel
    {
        int BestScore { get; }
        float Hunger { get; }
        float FullHunger { get; }
        float Thirst { get; }
        float FullThirst { get; }
        void Update(float addFood, float addWater, int CockNum);
        void ReportGameOverWithScore(int score);
    }
}