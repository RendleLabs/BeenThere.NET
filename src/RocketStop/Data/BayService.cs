using System.Collections.Generic;
using System.Threading.Tasks;
using RocketStop.Models;

namespace RocketStop.Data
{
    public interface IBayService
    {
        Task<IList<Bay>> List();
    }

    public class BayService : IBayService
    {
        private readonly List<Bay> _data = new List<Bay>{
            new Bay { Id = "A", Size = 1},
            new Bay { Id = "B", Size = 1},
            new Bay { Id = "C", Size = 2},
            new Bay { Id = "D", Size = 2},
            new Bay { Id = "E", Size = 4},
            new Bay { Id = "F", Size = 8},
        };

        public async Task<IList<Bay>> List()
        {
            await Task.Delay(1);
            return _data;
        }
    }
}