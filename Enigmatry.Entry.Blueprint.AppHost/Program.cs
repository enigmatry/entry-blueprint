var builder = DistributedApplication.CreateBuilder(args);

var entryApi =builder.AddProject<Projects.Enigmatry_Entry_Blueprint_Api>("enigmatry-entry-blueprint-api");

builder.AddProject<Projects.Enigmatry_Entry_Blueprint_Scheduler>("enigmatry-entry-blueprint-scheduler");

builder.AddJavaScriptApp("enigmatry-entry-blueprint-app", "../enigmatry-entry-blueprint-app", "start:aspire")
    .WithReference(entryApi)
    .WaitFor(entryApi)
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
