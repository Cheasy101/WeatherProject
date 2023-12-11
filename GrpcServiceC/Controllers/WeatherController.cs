using GrpcServiceC.Db.Entity;
using GrpcServiceC.Db.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GrpcServiceC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(WeatherRepository weatherRepository) : ControllerBase
{
    [HttpGet("getLastTenData")]
    [SwaggerOperation(
        Summary = "Get last ten data from the database",
        Description = "Returns a list of the last ten records from the database."
    )]
    [SwaggerResponse(200, "OK", typeof(List<WeatherData>))]
    public async Task<IActionResult> GetLastTenData()
    {
        var lastTenRecords = await weatherRepository.GetLastTenRecordsAsync();
        return Ok(lastTenRecords);
    }
}