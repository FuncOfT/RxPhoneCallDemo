using System;

namespace GenerateSomeData
{
    public class Conversation
    {
        public Conversation(DateTime timeStamp, string payload, string brokerId)
        {
            TimeStamp = timeStamp;
            Payload = payload;
            BrokerId = brokerId;
        }

        public DateTime TimeStamp { private set; get; }

        public string Payload { private set; get; }

        public string BrokerId { private set; get; }
    }
}