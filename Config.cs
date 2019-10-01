using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropperMqttBrokerRaspberry
{
    public static class Config
    {
        public static string MqttHost = ConfigurationManager.AppSettings["MqttHost"];
        public static int MqttPort
        {
            get
            {

                int port = 0;
                if (!int.TryParse(ConfigurationManager.AppSettings["MqttPort"], out port))
                    return 1883;
                return port;
            }
        }

        public static string[] MqttTopics
        {
            get
            {
                try
                {
                    string[] listOfTopics = ConfigurationManager.AppSettings["MqttTopics"].Split(';').ToArray();
                    return listOfTopics;
                }
                catch(Exception ex)
                {
                    return new string[] { };
                }
            }
        }

        public static string MqttClientName = ConfigurationManager.AppSettings["MqttClientName"];
        public static string MqttClientId = MqttClientName + "-" + Guid.NewGuid().ToString();  // This just needs to be unique per participant in the MQTT pipeline
        public static string MqttUsername = ConfigurationManager.AppSettings["MqttUsername"];
        public static string MqttPassword = ConfigurationManager.AppSettings["MqttPassword"];
    }
}
