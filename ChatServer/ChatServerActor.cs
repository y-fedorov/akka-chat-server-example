using Akka.Actor;
using ChatMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class ChatServerActor : TypedActor,
            IHandle<SayRequest>,
            IHandle<ConnectRequest>,
            IHandle<NickRequest>,
            IHandle<Disconnect>,
            IHandle<ChannelsRequest>,
            ILogReceive
    {

        private readonly HashSet<IActorRef> _clients = new HashSet<IActorRef>();

        public void Handle(SayRequest message)
        {
            Console.WriteLine("User {0} said {1}", message.Username, message.Text);
            var response = new SayResponse
            {
                Username = message.Username,
                Text = message.Text,
            };
            foreach (var client in _clients) client.Tell(response, Self);
        }

        public void Handle(ConnectRequest message)
        {
            Console.WriteLine("User {0} has connected", message.Username);
            _clients.Add(this.Sender);
            Sender.Tell(new ConnectResponse
            {
                Message = "Hello and welcome to Akka .NET chat example",
            }, Self);
        }

        public void Handle(NickRequest message)
        {
            var response = new NickResponse
            {
                OldUsername = message.OldUsername,
                NewUsername = message.NewUsername,
            };

            foreach (var client in _clients) client.Tell(response, Self);
        }

        public void Handle(Disconnect message)
        {

        }

        public void Handle(ChannelsRequest message)
        {

        }
    }
}
