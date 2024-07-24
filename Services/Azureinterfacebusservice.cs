using Azure.Storage.Queues;
using azureapplicationdemo.api.ViewModel;
using System.Text.Json;

namespace azureapplicationdemo.api.Services
{
    public class Azureinterfacebusservice : IAzureinterfacebusservice
    {
        private readonly QueueClient _queueClient;

        public Azureinterfacebusservice(QueueServiceClient queueServiceClient)
        {
            _queueClient = queueServiceClient.GetQueueClient("testqueue");
        }


        public async Task SendMessageAsync(string message)
        {
          await  _queueClient.CreateIfNotExistsAsync();
          await  _queueClient.SendMessageAsync(message);
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var serializeconfig = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            var msg = JsonSerializer.Serialize(message,serializeconfig);
            await _queueClient.CreateIfNotExistsAsync();
            await _queueClient.SendMessageAsync(msg);
        }
        public async Task<List<MessageResponse>> ReceivedMessageAsync(int msgcount)
        {
            var response = new List<MessageResponse>();
            var messages = await _queueClient.PeekMessagesAsync(msgcount);

            messages.Value.ToList().ForEach(x =>
            {
                var mes = new MessageResponse();
                mes.MessageId = x.MessageId;
                mes.Body = x.MessageText;
                response.Add(mes);
            });

            return response;

        }

        public async Task<List<MessageResponse>> ReceivedMessageDeleteAsync(int msgcount)
        {
            var response = new List<MessageResponse>();
            var messages = await _queueClient.ReceiveMessagesAsync(msgcount);

            messages.Value.ToList().ForEach(async x =>
            {
                var mes = new MessageResponse();
                mes.MessageId = x.MessageId;
                mes.Body = x.MessageText;
                response.Add(mes);
                await   _queueClient.DeleteMessageAsync(x.MessageId, x.PopReceipt);
            });

            return response;
        }
    }
}
