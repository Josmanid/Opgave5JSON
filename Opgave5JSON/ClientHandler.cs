using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Opgave5JSON.Models;

namespace Opgave5JSON
{
    public static class ClientHandler
    {
        public static void HandleClient(TcpClient socket) {
            //The client handling process
            //now we can use connection
            NetworkStream stream = socket.GetStream();

            //we can se what the client sends through this
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };




            try
            {

                //read incoming Json string
                string jMessage = reader.ReadLine();
                // check if we got anything to work with/deserialize
                if (string.IsNullOrWhiteSpace(jMessage))
                {
                    writer.WriteLine("Empty or invalid request");
                    return;
                }

                //convert to object for easier manipulation
                Answers answerobj = JsonSerializer.Deserialize<Answers>(jMessage);
                Console.WriteLine("Client sent: " + jMessage);

                switch (answerobj.Method)
                {
                    //step 1
                    case "Random":

                        // use the incoming converted JSON object
                        Random random = new Random();
                        int randomNumber = random.Next(answerobj.Tal1, answerobj.Tal2 + 1);

                        // Create a response object
                        Answers response = new Answers
                        {
                            Method = "Random",
                            Tal1 = answerobj.Tal1,
                            Tal2 = answerobj.Tal2,
                            Result = randomNumber
                        };

                        //convert back to Json string so that we can send it as it came! 
                        string JsonResultRandom = JsonSerializer.Serialize(response);
                        writer.WriteLine(JsonResultRandom);

                        break;
                    case "Add":

                        int addResult = (answerobj.Tal2 + answerobj.Tal1);
                        Answers response2 = new Answers
                        {
                            Method = "Add",
                            Tal1 = answerobj.Tal1,
                            Tal2 = answerobj.Tal2,
                            Result = addResult
                        };
                        string jsonResultAdd = JsonSerializer.Serialize(response2);
                        writer.WriteLine(jsonResultAdd);
                        break;
                    case "Subtract":
                        int subtractResult = (answerobj.Tal2 - answerobj.Tal1);
                        Answers response3 = new Answers
                        {
                            Method = "Subtract",
                            Tal1 = answerobj.Tal1,
                            Tal2 = answerobj.Tal2,
                            Result = subtractResult
                        };
                        string jsonResultSub = JsonSerializer.Serialize(response3);
                        writer.WriteLine(jsonResultSub);
                        break;
                    default:
                        writer.WriteLine("Not a valid protocol input");
                        break;


                }


            }
            catch (JsonException)
            {
                writer.WriteLine("Ups! Some typo in the JSON!");
            }
            catch (ArgumentNullException)
            {
                writer.WriteLine("You sent nothing to the server");
            }
            finally
            {
                socket.Close();
            }
        }
    }
}
