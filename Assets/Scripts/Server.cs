using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENet;
using Event = ENet.Event;
using EventType = ENet.EventType;

public class Server : MonoBehaviour
{
    public event Action<byte[], uint> PacketReceived;
    
    private Host _server;
    private Address _address;

    public void Listen(ushort port, string hostName = null)
    {
        _address = new Address();
        _server = new Host();
        
        if (hostName != null)
            _address.SetHost(hostName);
        _address.Port = port;
        
        _server.Create(_address, Constants.MaxClients);
    }

    public float BroadcastData(byte[] data, bool reliable = true)
    {
        Packet packet = default(Packet);
        PacketFlags packetFlags = PacketFlags.None;
        if (reliable)
            packetFlags |= PacketFlags.Reliable;
        packet.Create(data, packetFlags);
        
        _server.Broadcast(0, ref packet);
        return data.Length;
    }

    private void LateUpdate()
    {
        bool polled = false;
        Event netEvent;

        while (!polled)
        {
            if (_server.CheckEvents(out netEvent) <= 0)
            {
                if (_server.Service(0, out netEvent) <= 0)
                    return;

                polled = true;
            }

            switch (netEvent.Type)
            {
                case EventType.None:
                    break;
                case EventType.Connect:
                    Debug.Log("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;
                case EventType.Disconnect:
                    Debug.Log("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;
                case EventType.Timeout:
                    Debug.Log("Client timed out - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;
                case EventType.Receive:
                    byte[] buffer = new byte[netEvent.Packet.Length];
                    netEvent.Packet.CopyTo(buffer);
                    PacketReceived?.Invoke(buffer, netEvent.Peer.ID);
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        _server.Flush();
        _server.Dispose();
    }
}
