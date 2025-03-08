import json
from socket import *

serverName = "localhost"
serverPort = 12000

clientSocket = socket(AF_INET,SOCK_STREAM)
clientSocket.connect((serverName,serverPort))


pyobj  = {
  "Method": "Random",
  "Tal1": 11,
  "Tal2": 21
}
#  Convert it into a JSON string and adding and enpoint for all the bytes so that i can use readLine in C#
ConvertedPyObj = json.dumps(pyobj) + "\n"
# send it to server
clientSocket.sendall(ConvertedPyObj.encode())
# What i retrieve from server
ReceivedSentence = clientSocket.recv(1024).decode()

response = json.loads(ReceivedSentence)

# Print formatted JSON response
print("From Server:", json.dumps(response))



clientSocket.close()
