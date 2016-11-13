using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSomeData
{
    public class Generator
    {
        private int _numberOfBrokers;
        private int _numberOfConversations;
        private static Random s_Random = new Random();

        public Generator WithNumberOfBrokers(int numberOfBrokers)
        {
            _numberOfBrokers = numberOfBrokers;

            return this;
        }

        public Generator WithNumberOfTotalConversations(int numberOfConversations)
        {
            _numberOfConversations = numberOfConversations;

            return this;
        }

        public IEnumerable<Conversation> Generate()
        {
            var startDate = DateTime.UtcNow;

            return Enumerable.Range(1, _numberOfConversations)
                .Select(x => new Conversation(startDate.AddSeconds(x), GenerateRandomPayload(), $"Broker_{x%_numberOfBrokers}"));
        }

        public static string GenerateRandomPayload()
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var payload = new char[32];

            for (int i = 0; i < payload.Length; i++)
            {
                payload[i] = alphabet[s_Random.Next(alphabet.Length)];
            }
            
            return new string(payload);
        }
    }
}
