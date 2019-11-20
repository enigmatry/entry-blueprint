﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Enigmatry.Blueprint.Api.Init
{
    public static class AutoMapperStartupExtensions
    {
        public static void AppAddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AssemblyFinder.ApiAssembly, AssemblyFinder.ApplicationServicesAssembly);
        }
    }
}
