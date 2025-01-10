var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ChatService_APIs_REST>("chatservice-apis-rest");

builder.Build().Run();