using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSomeData
{
    public interface IWriter
    {
        void Write(Conversation conversation);
    }

    public class Writer : IWriter
    {
        private readonly string _workingDirectory;

        public Writer(string workingDirectory)
        {
            _workingDirectory = workingDirectory;
        }

        public void Write(Conversation conversation)
        {
            var folderName = Path.Combine(_workingDirectory, conversation.BrokerId);

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            var fileName = $"{conversation.BrokerId}_{conversation.TimeStamp.ToString("MM_dd_yy_H_mm_ss")}";

            var path = Path.ChangeExtension(Path.Combine(folderName, fileName), "txt");

            using (var writer = new StreamWriter(path))
            {
                var json = JsonConvert.SerializeObject(conversation, Formatting.Indented);
                writer.Write(json);
            }
        }        
    }
}
