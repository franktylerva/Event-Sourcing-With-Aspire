using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var productsDb = postgres.AddDatabase("productsdb");

var productApi = builder.AddProject<ProductCatalog>("product-api")
    .WithExternalHttpEndpoints()
    .WithReference(productsDb)
    .WaitFor(postgres);

builder.AddProject<Blazor>("blazor-client")
    .WithExternalHttpEndpoints()
    .WithReference(productApi)
    .WaitFor(productApi);

builder.Build().Run();
