using ChatBotSmartAI.Services;
using ChatBotSmartAI.Engines;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Configurações (Idealmente viriam do appsettings.json)
string pgConn = "Host=localhost;Port=5432;Database=smart_ai_db;Username=postgres;Password=suasenha";
string openAiKey = "sua-chave-aqui";

// Injeção de Dependência
builder.Services.AddSingleton<IVectorDbService>(sp =>
    new PostgresVectorService(pgConn, openAiKey));

builder.Services.AddScoped<ChatOrchestrator>();

using IHost host = builder.Build();

// Exemplo de uso rápido no console
var orchestrator = host.Services.GetRequiredService<ChatOrchestrator>();
var resposta = await orchestrator.GenerateResponseAsync("Como funciona a política de garantia?");

Console.WriteLine(resposta);

await host.RunAsync();