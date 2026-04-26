namespace ChatBotSmartAI.Services;

public interface IVectorDbService
{
    Task SaveInformationAsync(string collection, string text, string id);
    Task<string> SearchContextAsync(string collection, string query);
}