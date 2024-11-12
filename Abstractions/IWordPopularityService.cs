namespace WordPopularity.Abstractions;

public interface IWordPopularityService
{
    public Task<Entities.WorldPopularity> SearchWordPopularity(string term);
}
