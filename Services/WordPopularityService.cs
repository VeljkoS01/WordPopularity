using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordPopularity.Abstractions;
using WordPopularity.Controllers;

namespace WordPopularity.Services;

public class WordPopularityService : IWordPopularityService
{
    private HttpClient httpClient;
    private Context context;

    public WordPopularityService(Context context)
    {
        httpClient = new HttpClient();
        this.context = context;
    }
    public async Task<Entities.WorldPopularity> SearchWordPopularity(string term)
    {
        Entities.WorldPopularity result = await context.WordPopularity.Where(wp => wp.Word == term).FirstOrDefaultAsync();
        if (result != null) { return result; }


        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "VAS KLJUC");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        httpClient.DefaultRequestHeaders.Add("User-Agent", "request");
        
        HttpResponseMessage rocksResponse = await httpClient.GetAsync($"https://api.github.com/search/issues?q={term} rocks");
        HttpResponseMessage sucksResponse = await httpClient.GetAsync($"https://api.github.com/search/issues?q={term} sucks");

        CountResponse rocksResult = JsonSerializer.Deserialize<CountResponse>(await rocksResponse.Content.ReadAsStringAsync())!;
        CountResponse sucksResult = JsonSerializer.Deserialize<CountResponse>(await sucksResponse.Content.ReadAsStringAsync())!;

        double score = 0;
        if (rocksResult.TotalCount + sucksResult.TotalCount != 0)
            score = ((double)(rocksResult.TotalCount) / (rocksResult.TotalCount + sucksResult.TotalCount)) * 10;
        
        result = new Entities.WorldPopularity
        {
            Word = term,
            PositiveScore = rocksResult!.TotalCount,
            NegativeScore = sucksResult!.TotalCount,
            Score = score
        };

        await context.AddAsync(result);
        await context.SaveChangesAsync();

        return result;
        
    }
}

public class CountResponse
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
};
