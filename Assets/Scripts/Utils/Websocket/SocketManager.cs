using UnityEngine;
using System;
using System.Threading;
using System.Net.WebSockets;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class SocketManager : MonoBehaviour
{
    public ClientWebSocket clientSocket;
    public CancellationTokenSource ct;
    public delegate void creationPartyMessageDelegate(CreationPartyMessage message);
    public event creationPartyMessageDelegate creationPartyMessageEvent;
    public delegate void updatePartyMessageDelegate(UpdatePartyMessage message);
    public event updatePartyMessageDelegate updatePartyMessageEvent;
    public delegate void deletionPartyMessageDelegate(DeletionPartyMessage message);
    public event deletionPartyMessageDelegate deletionPartyMessageEvent;

    public async void ConnectWebsocket()
    {
        clientSocket = new ClientWebSocket();
        ct = new CancellationTokenSource();
        Debug.Log(StaticVariable.accessToken);
        clientSocket.Options.SetRequestHeader("Authorization", StaticVariable.accessToken);
        Debug.Log("Connecting");
        await clientSocket.ConnectAsync(new Uri("ws://pokechess-card-game.herokuapp.com/api/v1/pokechess"), ct.Token);
        var messageConnect = new ArraySegment<byte>(Encoding.Default.GetBytes("CONNECT\r\nversion:1.2"));
        await clientSocket.SendAsync(messageConnect, WebSocketMessageType.Text, true, ct.Token);
        Debug.Log("Connected");

        HandleMessages();
    }

    public async Task SubscribeRequest(int id, string destination)
    {
        var messageSend = new ArraySegment<byte>(Encoding.Default.GetBytes("SUBSCRIBE\r\nid:" + id + "\r\ndestination:" + destination + "\r\nack:auto"));
        Debug.Log("Subscribing...");
        await clientSocket.SendAsync(messageSend, WebSocketMessageType.Text, true, ct.Token);
        Debug.Log("Subscribed");
    }

    public async void SendRequest(string destination, string jsonBody)
    {
        var messageSend = new ArraySegment<byte>(Encoding.Default.GetBytes("SEND\r\ndestination:" + destination + "\r\n\n" + jsonBody));
        Debug.Log("Sending message...");
        await clientSocket.SendAsync(messageSend, WebSocketMessageType.Text, true, ct.Token);
        Debug.Log("Sended");
    }

    public async void DisconnectWebsocket()
    {
        await clientSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", ct.Token);
        Debug.Log("Closed");
    }

    private async void HandleMessages()
    {
        try
        {
            Debug.Log("Start listening");
            using (var ms = new MemoryStream()) {
                while (clientSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result;
                    do
                    {
                        var messageBuffer = WebSocket.CreateClientBuffer(1024, 16);
                        result = await clientSocket.ReceiveAsync(messageBuffer, CancellationToken.None);
                        ms.Write(messageBuffer.Array, messageBuffer.Offset, result.Count);
                    }
                    while (!result.EndOfMessage);
                    
                    Debug.Log("Message received");

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        MapMessage(Encoding.UTF8.GetString(ms.ToArray()));
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Position = 0;
                }
                Debug.LogWarning("Connection lose");
            }
        } catch (InvalidOperationException)
        {
            Debug.LogError("[WS] Tried to receive message while already reading one.");
        }
    }

    private void MapMessage(string messageStr)
    {
        var destination = new Regex(@"(destination:)(.)*").Match(messageStr);
        var bodyMessage = new Regex(@"({){1}(.)*(}){1}").Match(messageStr);
        if (destination.Success && bodyMessage.Success)
        {
            switch (destination.Value.Substring(12))
            {
                case CreationPartyMessage.destination:
                    if (creationPartyMessageEvent != null) creationPartyMessageEvent(JsonConvert.DeserializeObject<CreationPartyMessage>(bodyMessage.Value));
                    break;
                case UpdatePartyMessage.destination:
                    if (updatePartyMessageEvent != null) updatePartyMessageEvent(JsonConvert.DeserializeObject<UpdatePartyMessage>(bodyMessage.Value));
                    break;
                case DeletionPartyMessage.destination:
                    if (deletionPartyMessageEvent != null) deletionPartyMessageEvent(JsonConvert.DeserializeObject<DeletionPartyMessage>(bodyMessage.Value));
                    break;
            }
        }
    }
}
