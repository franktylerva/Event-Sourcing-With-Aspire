using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var productsDb = postgres.AddDatabase("productsdb");

var productApi = builder.AddProject<ProductCatalog>("product-api")
    .WithReference(productsDb)
    .WaitFor(postgres);

var blazorClient = builder.AddProject<Blazor>("blazor-client")
    .WithReference(productApi)
    .WaitFor(productApi);

var gateway = builder.AddProject<Gateway>("gateway")
    .WithReference(productApi)
    .WaitFor(productApi)
    .WithReference(blazorClient)
    .WaitFor(blazorClient)
    .WithExternalHttpEndpoints();

builder.Build().Run();
