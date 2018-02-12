using System;
using System.Linq;
using Akka;
using Akka.Actor;
using ChatMessages;
using Akka.Configuration;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("MyClient"))
            {
                var chatClient = system.ActorOf(Props.Create<ChatClientActor>());
                system.ActorSelection("akka.tcp://MyServer@localhost:8081/user/ChatServer");
                chatClient.Tell(new ConnectRequest()
                {
                    Username = "Roggan",
                });

                while (true)
                {
                    var input = Console.ReadLine();
                    if (input.StartsWith("/"))
                    {
                        var parts = input.Split(' ');
                        var cmd = parts[0].ToLowerInvariant();
                        var rest = string.Join(" ", parts.Skip(1));

                        if (cmd == "/nick")
                        {
                            chatClient.Tell(new NickRequest
                            {
                                NewUsername = rest
                            });
                        }
                    }
                    else
                    {
                        chatClient.Tell(new SayRequest()
                        {
                            Text = input,
                        });
                    }
                }
            }
        }
    }
}