using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WepApiScrapingData.DTOs.Play;

namespace WebApiScrapingData.Test.Game;

[TestFixture]
public class DuelDtoTimeTests
{
    [Test]
    public void QuestionTimesFromDatabaseAreSerializedAsUtc()
    {
        // SQL Server datetime2 returns DateTime values without a Kind, although they were stored in UTC.
        var start = new DateTime(2026, 7, 1, 10, 0, 0, DateTimeKind.Unspecified);
        var dto = new DuelDto
        {
            QuestionStartsAt = start,
            QuestionEndsAt = start.AddSeconds(15),
            ServerTime = new DateTime(2026, 7, 1, 10, 0, 0, DateTimeKind.Utc)
        };

        using var reader = new JsonTextReader(new StringReader(JsonConvert.SerializeObject(dto)))
        {
            DateParseHandling = DateParseHandling.None
        };
        var json = JObject.Load(reader);
        Assert.That((string?)json[nameof(DuelDto.QuestionStartsAt)], Is.EqualTo("2026-07-01T10:00:00Z"));
        Assert.That((string?)json[nameof(DuelDto.QuestionEndsAt)], Is.EqualTo("2026-07-01T10:00:15Z"));
        Assert.That((string?)json[nameof(DuelDto.ServerTime)], Is.EqualTo("2026-07-01T10:00:00Z"));
    }
}
