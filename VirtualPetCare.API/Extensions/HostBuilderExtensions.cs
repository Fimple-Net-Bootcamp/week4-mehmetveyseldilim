using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace VirtualPetCare.API.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder, IConfiguration configuration)
        {
            builder.UseSerilog((context, configuration) => 
                                configuration.ReadFrom.Configuration(context.Configuration));

            return builder;
        }

    }
}