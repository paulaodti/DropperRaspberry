using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DropperMqttBrokerRaspberry
{
    public class Mqtt
    {
        private MqttClient _mqttClient;

        public void Setup()
        {
            _mqttClient = new MqttClient(Config.MqttHost);
            for (int tentativa = 1; tentativa <= 5; tentativa++)
            {
                try
                {
                    _mqttClient.Connect(Config.MqttClientId, Config.MqttUsername, Config.MqttPassword);

                    if (_mqttClient.IsConnected)
                    {
                        Console.WriteLine("Connected: " + _mqttClient.ClientId);
                        _mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                        _mqttClient.MqttMsgSubscribed += client_MqttMsgSubscribed;

                        foreach (string item in Config.MqttTopics)
                        {
                            string[] topico = new string[] { item };
                            _mqttClient.Subscribe(topico, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Could not connect to the MQTT server. Tentativa {tentativa}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu uma exceção ao conectar ao servidor MQTT. Detalhes: {ex.Message}");
                }
            }
        }

        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (byte qosLevel in e.GrantedQoSLevels)
            {
                sb.Append(qosLevel);
                sb.Append(" ");
            }

            Console.WriteLine("Tópico assinado - MessageId: " + e.MessageId + ", QOS Levels: " + sb.ToString());
        }

        void client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            string message = new string(Encoding.UTF8.GetChars(e.Message));
            Console.WriteLine("Mensagem Recebida - Tópico: " + e.Topic + ", Mensagem: " + message + ", QosLevel: " + e.QosLevel);
        }
    }
}
