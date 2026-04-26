using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.Postgres;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace ChatBotSmartAI.Services;

public class PostgresVectorService : IVectorDbService
{
    private readonly ISemanticTextMemory _memory;

    public PostgresVectorService(string connectionString, string openAiApiKey)
    {
        var dbStore = new PostgresVectorStore(connectionString, vectorSize: 1536);

        _memory = new MemoryBuilder()
            .WithOpenAITextEmbeddingGeneration("text-embedding-3-small", openAiApiKey)
            .WithMemoryStore(dbStore)
            .Build();
    }

    public async Task SaveInformationAsync(string collection, string text, string id)
    {
        await _memory.SaveInformationAsync(collection, text, id);
    }

    public async Task<string> SearchContextAsync(string collection, string query)
    {
        var results = _memory.SearchAsync(collection, query, limit: 3, minRelevanceScore: 0.7);
        var context = "";

        await foreach (var res in results)
        {
            context += $"\n[DOC {res.Metadata.Id}]: {res.Metadata.Text}";
        }

        return context;
    }
}