using azureapplicationdemo.api.ViewModel;

namespace azureapplicationdemo.api.Services
{
    public interface IAzureinterfacebusservice
    {
        Task SendMessageAsync(string message);
        Task SendMessageAsync<T>(T message);

        Task<List<MessageResponse>> ReceivedMessageAsync(int msgcount);

        Task<List<MessageResponse>> ReceivedMessageDeleteAsync(int msgcount);
    }
}
