using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Entities
{
    public class ErrorDetails
    {
        	public int StatusCode { get; set; }
            public IEnumerable<string>? Message { get; set; }

            public override string ToString() => JsonSerializer.Serialize(this);
    }
}