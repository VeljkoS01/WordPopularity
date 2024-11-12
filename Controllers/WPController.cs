using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordPopularity.Abstractions;

namespace WordPopularity.Controllers;

[ApiController]
[Route("[controller]")]
public class WPController: ControllerBase
{
    private IWordPopularityService wpService;

    public WPController(IWordPopularityService wpService)
    {
        this.wpService = wpService;

    }

    [HttpGet("search")]
    public async Task<ActionResult> SearchForWordPopularity([FromQuery] string term)
    {
        return Ok(await wpService.SearchWordPopularity(term));
    }

    
};



