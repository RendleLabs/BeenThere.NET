using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RocketStop.Models;
using RocketStop.Options;

namespace RocketStop.Data
{
    public interface IDockingService
    {
        Task<IEnumerable<Docking>> List();
        Task<Docking> Add(Docking docking);
        Task Delete(int id);
    }
    public class DockingService : IDockingService
    {
        private readonly HttpClient _client;
        public DockingService(IOptions<Services> services)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(services.Value.Docking);
        }

        public async Task<IEnumerable<Docking>> List()
        {
            var json = await _client.GetStringAsync("/api");
            return JsonConvert.DeserializeObject<Docking[]>(json);
        }

        public async Task<Docking> Add(Docking docking)
        {
            var json = JsonConvert.SerializeObject(docking);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/api", content);
            return JsonConvert.DeserializeObject<Docking>(
                await response.Content.ReadAsStringAsync()
                );
        }

        public async Task Delete(int id)
        {
            await _client.DeleteAsync($"/api/{id}");
        }
    }
}