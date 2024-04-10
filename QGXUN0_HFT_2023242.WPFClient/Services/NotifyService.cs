using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace QGXUN0_HFT_2023242.WPFClient.Services
{
    public class NotifyService
    {
        private HubConnection connection;

        public NotifyService(string url, string hub)
        {
            connection = new HubConnectionBuilder().WithUrl(url + hub).Build();

            connection.Closed += async _ =>
            {
                await Task.Delay(1000);
                await connection.StartAsync();
            };
        }

        public async void Init()
        {
            await connection.StartAsync();
        }

        public void AddHandler<T>(string methodName, Action<T> value)
        {
            connection.On<T>(methodName, value);
        }
    }
}
