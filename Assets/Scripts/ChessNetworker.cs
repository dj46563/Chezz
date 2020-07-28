using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ChessNetworker : MonoBehaviour
{
    private bool _isServer;
    private Server _server;
    private Client _client;
    private enum Type : byte
    {
        ClientMove,
        ServerMove,
    }

    public enum NetworkerType
    {
        Client, Server
    }
    
    public ChessNetworker(NetworkerType networkerType)
    {
        switch (networkerType)
        {
            case NetworkerType.Server:
                _isServer = true;
                _server = gameObject.AddComponent<Server>();
                _server.Listen(Constants.DefaultPort);
                break;
            case NetworkerType.Client:
                _isServer = false;
                _client = gameObject.AddComponent<Client>();
                _client.Connect(Constants.DefaultHost, Constants.DefaultPort);
                break;
        }
    }

    public void SendClientMove(Coordinate source, Coordinate destination)
    {
        if (_isServer)
            throw new Exception("Server networker cannot send this message");

        byte[] data = GenerateClientMove(source, destination);
        _client.SendData(data);
    }

    public void SendServerMove(Coordinate source, Coordinate destination, TimeSpan timeLeft)
    {
        if (!_isServer)
            throw new Exception("Client networker cannot send this message");

        byte[] data = GenerateServerMove(source, destination, timeLeft);
        _server.BroadcastData(data);
    }
    
    private byte[] GenerateClientMove(Coordinate source, Coordinate destination)
    {
        Type messageType = Type.ClientMove;
        byte[] bytes;
        
        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, messageType);
            formatter.Serialize(stream, source);
            formatter.Serialize(stream, destination);

            bytes = stream.ToArray();
        }

        return bytes;
    }

    private byte[] GenerateServerMove(Coordinate source, Coordinate destination, TimeSpan timeLeft)
    {
        Type messageType = Type.ServerMove;
        byte[] bytes;
        
        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, messageType);
            formatter.Serialize(stream, source);
            formatter.Serialize(stream, destination);
            formatter.Serialize(stream, timeLeft);

            bytes = stream.ToArray();
        }

        return bytes;
    }
}
