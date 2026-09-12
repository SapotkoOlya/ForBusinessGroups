using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForPetStore2
{
    public class AllPetsResponseDTO
    {
        [JsonPropertyName("data")]
        public List<PetDTO> Data { get; set; }
    }
}
