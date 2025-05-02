using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithExternalHttpEndpoints()
    .WithLifetime(ContainerLifetime.Persistent);

var productsDb = postgres.AddDatabase("productsdb");

var productApi = builder.AddProject<ProductCatalog>("product-api")
    .WithExternalHttpEndpoints()
    .WithReference(productsDb)
    .WaitFor(productsDb)
    .WithReference(keycloak)
    .WaitFor(keycloak);

var blazorClient = builder.AddProject<Blazor>("blazor-client")
    .WithReference(productApi)
    .WaitFor(productApi);

var reactClient = builder.AddViteApp("react-client", 
        "../Clients/React", "yarn")
    .WithExternalHttpEndpoints()
    .WithReference(productApi)
    .WaitFor(productApi);

var gateway = builder.AddProject<Gateway>("gateway")
    .WithReference(productApi)
    .WaitFor(productApi)
    .WithReference(blazorClient)
    .WaitFor(blazorClient)
    .WithReference(reactClient)
    .WaitFor(reactClient)
    .WithExternalHttpEndpoints();

builder.Build().Run();
