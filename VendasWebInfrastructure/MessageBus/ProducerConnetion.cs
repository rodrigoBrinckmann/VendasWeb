using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebInfrastructure.MessageBus
{
    public class ProducerConnetion
    {
        public IConnection Connection { get; private set; }

        public ProducerConnetion(IConnection connection)
        {
            Connection = connection;
        }
    }
} 
