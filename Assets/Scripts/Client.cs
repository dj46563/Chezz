using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENet;
using Event = ENet.Event;
using EventType = ENet.EventType;

public class Client : MonoBehaviour
{
    public event Action<byte[]> PacketReceived;
    
    private Address _address;
    private Host _client;
    private Peer _peer;
    
    public void Connect(string host, ushort port)
    {
        _address = new Address();
        _client = new Host();
        
        _address.SetHost(host);
        _address.Port = port;
        _client.Create();

        _peer = _client.Connect(_address);
    }

    public void SendData(byte[] data, bool reliable = true)
    {
        Packet packet = default(Packet);
        PacketFlags packetFlags = PacketFlags.None;
        if (reliable)
            packetFlags |= PacketFlags.Reliable;
        packet.Create(data, packetFlags);

        _peer.Send(0, ref packet);
    }

    private void LateUpdate()
    {
        bool polled = false;
        Event netEvent;

        while (!polled)
        {
            if (_client.CheckEvents(out netEvent) <= 0)
            {
                if (_client.Service(0, out netEvent) <= 0)
                    return;
                
                polled = true;
            }

            switch (netEvent.Type)
            {
                case EventType.None:
                    break;
                case EventType.Connect:
                    Debug.Log("Client connected to server");
                    break;
                case EventType.Disconnect:
                    Debug.Log("Client disconnected from server");
                    break;
                case EventType.Timeout:
                    Debug.Log("Client timed out");
                    break;
                case EventType.Receive:
                    byte[] buffer = new byte[netEvent.Packet.Length];
                    PacketReceived?.Invoke(buffer);
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        _client.Flush();
        _peer.DisconnectNow(0);
        _client.Dispose();
    }
}
