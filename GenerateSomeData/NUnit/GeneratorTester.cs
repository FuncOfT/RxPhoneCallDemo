using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSomeData.NUnit
{
    [TestFixture]
    public class GeneratorTester
    {
        [Test]
        public void WhenGenerating100ConversationsSpreadOver10Brokers()
        {
            var conversationsPerBroker = new Generator().WithNumberOfBrokers(10)
                .WithNumberOfTotalConversations(100).Generate().GroupBy(c => c.BrokerId);

            Assert.That(conversationsPerBroker.Count, Is.EqualTo(10));

            int totalConversations = conversationsPerBroker.Select(group => group.Count()).Sum();
            Assert.That(totalConversations, Is.EqualTo(100));                        
        }
    }
}
