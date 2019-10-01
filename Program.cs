using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DropperMqttBrokerRaspberry
{
    class Program
    {
        static void Main(string[] args)
        {
            Mqtt mqtt = new Mqtt();
            mqtt.Setup();
        }
    }
}
