using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualPetCare.Data.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string errorMessage) : base(errorMessage) { }
    }
}