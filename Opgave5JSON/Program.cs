using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Opgave5JSON;
using Opgave5JSON.Models;
using System.Text.Json;



public class Program
{
    private static void Main(string[] args) {
        
     
        //først skal der lyttes
        Console.WriteLine("TCP Server: With JSON");
        //The IP Address used here, is not the client IP
        TcpListener listener = new TcpListener(IPAddress.Any, 12000);
        listener.Start();

        // Now we can handle more clients
        while (true)
        {
            //our communication socket/object we can communicate through
            TcpClient socket = listener.AcceptTcpClient();
            //Handling clients at the same time
            Task.Run(() => ClientHandler.HandleClient(socket));

        }
        listener.Stop();
    }
}