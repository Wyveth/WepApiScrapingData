using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class DebugController : ControllerBase
{
    [HttpGet("scanfiles")]
    public IActionResult ScanFiles()
    {
        var rootPath = "/Users/benjamingarrigue/Documents/GitHub/WepApiScrapingData/WepApiScrapingData/bin/Debug/net8.0/Content";

        if (!Directory.Exists(rootPath))
        {
            return NotFound(new { message = $"Dossier introuvable: {rootPath}" });
        }

        var result = new List<object>();

        foreach (var file in Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories))
        {
            var exists = System.IO.File.Exists(file);
            result.Add(new
            {
                CheminComplet = file,
                Existe = exists
            });
        }

        return Ok(result);
    }
}


[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new { status = "API is running", timestamp = System.DateTime.UtcNow });
    }
}