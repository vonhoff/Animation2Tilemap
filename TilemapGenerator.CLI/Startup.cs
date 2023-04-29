﻿using Microsoft.Extensions.DependencyInjection;
using TilemapGenerator.CLI.Logging;
using TilemapGenerator.Services;

namespace TilemapGenerator.CLI;

public class Startup
{
    private readonly ApplicationOptions _applicationOptions;

    public Startup(ApplicationOptions applicationOptions)
    {
        _applicationOptions = applicationOptions;
    }

    public Application BuildApplication()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices(_applicationOptions);
        services.AddSerilogLogging(_applicationOptions.Verbose);

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetService<Application>()!;
    }
}