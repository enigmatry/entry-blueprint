var builder = DistributedApplication.CreateBuilder(args);

var entryAPi =builder.AddProject<Projects.Enigmatry_Entry_Blueprint_Api>("enigmatry-entry-blueprint-api");

builder.AddProject<Projects.Enigmatry_Entry_Blueprint_Scheduler>("enigmatry-entry-blueprint-scheduler");

builder.AddNpmApp("enigmatry-entry-blueprint-app", "../enigmatry-entry-blueprint-app", "start:aspire")
    .WithReference(entryAPi)
    .WaitFor(entryAPi)
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
