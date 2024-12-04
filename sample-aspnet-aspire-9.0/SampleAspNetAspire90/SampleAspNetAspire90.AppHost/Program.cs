var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var cache = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRedisInsight((redisInsight) => {
        redisInsight.WithLifetime(ContainerLifetime.Persistent);
    })
    .WithRedisCommander((rediscommander) => {
        rediscommander.WithLifetime(ContainerLifetime.Persistent);
    });

//var cache = builder.AddRedis("cache")
//    .WithLifetime(ContainerLifetime.Persistent)
//    .WithRedisInsight()
//    .WithRedisCommander();

var sqlserverdb = builder.AddSqlServer("sqlserverdb", port: 1500)
    .WithLifetime(ContainerLifetime.Persistent);

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
