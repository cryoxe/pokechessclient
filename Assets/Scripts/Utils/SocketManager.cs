using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Newtonsoft.Json;
using System.Text;
using System.IO;

public class SocketManager : MonoBehaviour
{
    public ClientWebSocket clientSocket;
    public CancellationTokenSource ct;

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

        var messageSubscribe = new ArraySegment<byte>(Encoding.Default.GetBytes("SUBSCRIBE\r\nid:0\r\ndestination:/topic/greetings\r\nack:auto"));
        await clientSocket.SendAsync(messageSubscribe, WebSocketMessageType.Text, true, ct.Token);
        Debug.Log("Subscribed");
    }

    public async void SendRequest() {
        var user = new Classes.JwtRequestDTO();
        user.username = "crizang";
        string json = JsonConvert.SerializeObject(user);
        var messageSend = new ArraySegment<byte>(Encoding.Default.GetBytes("SEND\r\ndestination:/app/hello\r\n\n" + json));
        Debug.Log("Sending");
        await clientSocket.SendAsync(messageSend, WebSocketMessageType.Text, true, ct.Token);
        Debug.Log("Sended");
        await HandleMessages(clientSocket);
    }

    public async void DisconnectWebsocket() {
        await clientSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", ct.Token);
        Debug.Log("Closed");
    }

    private async Task HandleMessages(ClientWebSocket ws)
    {
        try {
            Debug.Log("Start listen");
            using (var ms = new MemoryStream()) {
                while (ws.State == WebSocketState.Open) {
                    WebSocketReceiveResult result;
                    do {
                        var messageBuffer = WebSocket.CreateClientBuffer(1024, 16);
                        result = await ws.ReceiveAsync(messageBuffer, CancellationToken.None);
                        ms.Write(messageBuffer.Array, messageBuffer.Offset, result.Count);
                    }
                    while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Text) {
                        Debug.Log("received");
                        var msgString = Encoding.UTF8.GetString(ms.ToArray());
                        Debug.Log(msgString);
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Position = 0;
                }
            }
        } catch (InvalidOperationException) {
            Debug.Log("[WS] Tried to receive message while already reading one.");
        }
    }
}
