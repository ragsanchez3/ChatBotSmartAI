using ChatBotSmartAI.Services;

namespace ChatBotSmartAI.Engines;

public class ChatOrchestrator
{
    private readonly IVectorDbService _vectorService;
    private const string DefaultCollection = "smart_ai_knowledge";

    public ChatOrchestrator(IVectorDbService vectorService)
    {
        _vectorService = vectorService;
    }

    public async Task<string> GenerateResponseAsync(string userQuestion)
    {
        // Busca o contexto relevante no Postgres
        var context = await _vectorService.SearchContextAsync(DefaultCollection, userQuestion);

        if (string.IsNullOrEmpty(context))
            return "Não encontrei informações específicas sobre isso na minha base.";

        // Montagem do prompt para a LLM
        var prompt = $"""
            Você é um assistente inteligente. Responda com base no contexto técnico fornecido.
            
            CONTEXTO:
            {context}
            
            PERGUNTA:
            {userQuestion}
            """;

        return prompt; // Por enquanto retornamos o prompt; na evolução chamaremos o ChatCompletion
    }
}