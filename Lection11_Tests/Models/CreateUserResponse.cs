using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lection11_Tests.Models
{
    public class CreateUserResponse
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("job")] public string Job { get; set; }
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("createdAt")] public string CreatedAt { get; set; }
    }
}
