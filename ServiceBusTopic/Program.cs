using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusTopic
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "CONNECTIONSTRING";
            string topicName = "TOPICNAME";

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);
            bool topicExists = namespaceManager.TopicExists(topicName);
            if (!topicExists)
            {
                namespaceManager.CreateTopic(topicName);
            }

            TopicClient Client = TopicClient.CreateFromConnectionString(connectionString, topicName);



            for (int i = 0; i < 5; i++)
            {
                // Create message, passing a string message for the body.
                BrokeredMessage message = new BrokeredMessage("Test message " + i);

                // Set some addtional custom app-specific properties.
                message.Properties["TestProperty"] = "TestValue";
                message.Properties["MessageNumber"] = i;

                // Send message to the queue.
                Client.Send(message);
            }

        }
    }
}
