using System;
using System.Collections.Generic;
using System.Text;

namespace Kafka.Consumer.Console
{
    internal class ParametersModel
    {
        public ParametersModel()
        {
            BoostrapServer = "localhost:9092";
            TopicName = "topic1";
            GroupId = "Group 1";
        }

        public string BoostrapServer {  get; set; }
        public string TopicName {  get; set; }
        public string GroupId {  get; set; }

    }
}
