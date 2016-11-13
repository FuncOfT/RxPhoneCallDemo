using GenerateSomeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.IO;
using System.Reactive.Subjects;
using Newtonsoft.Json;

namespace RxPhoneCallDemo
{
    public class ConversationFileWatcher
    {
        private readonly FileSystemWatcher _fileWatcher;
       
        public ConversationFileWatcher()
        {
            _fileWatcher = new FileSystemWatcher();

            _fileWatcher.Path = Directory.GetCurrentDirectory();
            _fileWatcher.IncludeSubdirectories = true;
            _fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime | NotifyFilters.Attributes;
            _fileWatcher.Filter = "*.txt";

            _fileWatcher.EnableRaisingEvents = true;
        }

        public IObservable<Conversation> GetConversationObservable()
        {
            return Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(handler => _fileWatcher.Created += handler,
                handler => _fileWatcher.Created -= handler)
                .Where(x => x.EventArgs.ChangeType == WatcherChangeTypes.Created)
                .Select(x => Deserialize(x.EventArgs.FullPath, 1))
                .Where(x => x != null);
        }

        private Conversation Deserialize(string path, int attempt)
        {
            if (attempt > 5)
            {
                return null;
            }

            try
            {
                using (var stream = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    return JsonConvert.DeserializeObject<Conversation>(stream.ReadToEnd());
                }
            }
            catch (Exception)
            {
                return Deserialize(path, ++attempt);
            }
        }
    }
}
