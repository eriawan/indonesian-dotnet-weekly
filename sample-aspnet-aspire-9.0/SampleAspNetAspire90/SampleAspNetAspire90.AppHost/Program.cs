var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var cache = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRedisInsight()
    .WithRedisCommander();

var apiService = builder.AddProject<Projects.SampleAspNetAspire90_ApiService>("apiservice")
    .WithReference(cache)
    .WaitFor(cache);

builder.AddProject<Projects.SampleAspNetAspire90_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
