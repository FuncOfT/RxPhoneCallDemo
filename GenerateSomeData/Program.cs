using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSomeData
{
    class Program
    {
        static void Main(string[] args)
        {
            var conversations = new Generator().WithNumberOfBrokers(10)
                .WithNumberOfTotalConversations(100)
                .Generate();

            var writer = new Writer(Directory.GetCurrentDirectory());
            foreach (var conversation in conversations)
            {
                writer.Write(conversation);
            }
        }
    }
}
