using ChatMessages;
using Akka;
using Akka.Actor;
using System;
using System.Collections.Generic;
using Akka.Configuration;
using Akka.Event;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("MyServer"))
            {
                Console.WriteLine("Serever Init");
                system.ActorOf<ChatServerActor>("ChatServer");
                Console.WriteLine("Quit");
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}
