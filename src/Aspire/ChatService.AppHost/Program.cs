var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ChatService_APIs_REST>("chatservice-rest-api");

builder.Build().Run();