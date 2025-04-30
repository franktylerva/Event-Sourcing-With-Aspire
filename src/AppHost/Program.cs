using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var productsDb = postgres.AddDatabase("productsdb");

// Keycloak container
var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak:24.0")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
    .WithArgs("start-dev")
    .WithHttpEndpoint(port: 8080, targetPort: 8080, name: "http")
    .WithLifetime(ContainerLifetime.Persistent);

var productApi = builder.AddProject<ProductCatalog>("product-api")
    .WithReference(productsDb)
    .WaitFor(postgres)
    .WithEnvironment("Keycloak__Authority", keycloak.GetEndpoint("http"))
    .WithEnvironment("Keycloak__ClientId", "myapp-api")
    .WithEnvironment("Keycloak__Secret", "myapp-secret");

var blazorClient = builder.AddProject<Blazor>("blazor-client")
    .WithReference(productApi)
    .WaitFor(productApi);

var gateway = builder.AddProject<Gateway>("gateway")
    .WithReference(productApi)
    .WaitFor(productApi)
    .WithReference(blazorClient)
    .WaitFor(blazorClient)
    .WithEnvironment("Keycloak__Authority", keycloak.GetEndpoint("http"))
    .WithExternalHttpEndpoints();

builder.Build().Run();
