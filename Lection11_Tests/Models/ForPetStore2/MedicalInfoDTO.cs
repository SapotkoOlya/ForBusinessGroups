using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForPetStore2
{
    public class MedicalInfoDTO
    {
        [JsonPropertyName("vaccinated")]
        public bool Vaccinated { get; set; }

        [JsonPropertyName("spayedNeutered")]
        public bool SpayedNeutered { get; set; }

        [JsonPropertyName("microchipped")]
        public bool Microchipped { get; set; }

        [JsonPropertyName("specialNeeds")]
        public bool SpecialNeeds { get; set; }

        [JsonPropertyName("healthNotes")]
        public string HealthNotes { get; set; }
    }
}
