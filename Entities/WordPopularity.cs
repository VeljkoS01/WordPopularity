namespace WordPopularity.Entities;

public class WorldPopularity
{
    public int Id { get; set; }
    public string Word { get; set; }
    public int PositiveScore { get; set; }
    public int NegativeScore { get; set; }
    public double Score { get; set; }
}
