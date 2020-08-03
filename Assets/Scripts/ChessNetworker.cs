using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class ChessNetworker
{
    public event Action<Coordinate, Coordinate, uint> ClientMoveReceived;
    public event Action<Coordinate, Coordinate, TimeSpan> ServerMoveReceived;
    
    public bool IsServer { get; private set; }
    private Server _server;
    private Client _client;
    private IFormatter formatter;
    private enum MessageType : byte
    {
        ClientMove,
        ServerMove,
    }

    public enum NetworkerType
    {
        Client, Server
    }

    public ChessNetworker(NetworkerType networkerType, GameObject parent)
    {
        formatter = new BinaryFormatter();
        
        switch (networkerType)
        {
            case NetworkerType.Server:
                IsServer = true;
                _server = parent.AddComponent<Server>();
                _server.Listen(Constants.DefaultPort);
                _server.PacketReceived += ServerOnPacketReceived;
                break;
            case NetworkerType.Client:
                IsServer = false;
                _client = parent.AddComponent<Client>();
                ConnectToServer(Constants.DefaultHost, Constants.DefaultPort);
                _client.PacketReceived += ClientOnPacketReceived;
                break;
        }
    }

    public void ConnectToServer(string host, ushort port)
    {
        if (!IsServer)
            _client.Connect(host, port);
    }

    private MessageType GetMessageType(MemoryStream stream)
    {
        return (MessageType)formatter.Deserialize(stream);
    }

    private void ClientOnPacketReceived(byte[] data)
    {
        using (MemoryStream stream = new MemoryStream(data))
        {
            MessageType messageMessageType = GetMessageType(stream);
            switch (messageMessageType)
            {
                case MessageType.ServerMove:
                    Coordinate source = (Coordinate) formatter.Deserialize(stream);
                    Coordinate destination = (Coordinate) formatter.Deserialize(stream);
                    TimeSpan timeLeft = (TimeSpan) formatter.Deserialize(stream);
                    ServerMoveReceived?.Invoke(source, destination, timeLeft);
                    break;
                default:
                    throw new Exception("Client received another client packet");
            }
        }
    }

    private void ServerOnPacketReceived(byte[] data, uint peerId)
    {
        MessageType messageType = (MessageType) data[0];
        switch (messageType)
        {
            case MessageType.ClientMove:
                byte[] srcData = new byte[2];
                Buffer.BlockCopy(data, 1, srcData, 0, 2);
                byte[] dstData = new byte[2];
                Buffer.BlockCopy(data, 3, dstData, 0, 2);
                ClientMoveReceived?.Invoke(Coordinate.Deserialize(srcData), Coordinate.Deserialize(dstData), peerId);
                break;
            default:
                throw new Exception("Server received another server packet");
        }
    }

    public void SendClientMove(Coordinate source, Coordinate destination)
    {
        if (IsServer)
            throw new Exception("Server networker cannot send this message");

        byte[] data = GenerateClientMove(source, destination);
        _client.SendData(data);
    }

    public void SendServerMove(Coordinate source, Coordinate destination, TimeSpan timeLeft)
    {
        if (!IsServer)
            throw new Exception("Client networker cannot send this message");

        byte[] data = GenerateServerMove(source, destination, timeLeft);
        _server.BroadcastData(data);
    }
    
    private byte[] GenerateClientMove(Coordinate source, Coordinate destination)
    {
        byte[] bytes = new byte[5];
        bytes[0] = (byte)MessageType.ClientMove;
        Buffer.BlockCopy(Coordinate.Serialize(source), 0, bytes, 1, 2);
        Buffer.BlockCopy(Coordinate.Serialize(destination), 0, bytes, 3, 2);

        return bytes;
    }

    private byte[] GenerateServerMove(Coordinate source, Coordinate destination, TimeSpan timeLeft)
    {
        MessageType messageMessageType = MessageType.ServerMove;
        byte[] bytes;
        
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, messageMessageType);
            formatter.Serialize(stream, source);
            formatter.Serialize(stream, destination);
            formatter.Serialize(stream, timeLeft);

            bytes = stream.ToArray();
        }

        return bytes;
    }
}
