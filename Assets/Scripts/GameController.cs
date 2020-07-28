using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ChessNetworker _chessNetworker;
    public bool IsServer => _chessNetworker.IsServer; //Expose IsServer from networker

    private void Awake()
    {
        if (Application.isBatchMode)
        {
            _chessNetworker = new ChessNetworker(ChessNetworker.NetworkerType.Server, gameObject);
            _chessNetworker.ClientMoveReceived += OnClientMoveReceived;
        }
        else
        {
            _chessNetworker = new ChessNetworker(ChessNetworker.NetworkerType.Client, gameObject);
            _chessNetworker.ServerMoveReceived += OnServerMoveReceived;
        }
    }

    private void OnServerMoveReceived(Coordinate arg1, Coordinate arg2, TimeSpan arg3)
    {
        // Apply move to board
        throw new NotImplementedException();
    }

    private void OnClientMoveReceived(Coordinate arg1, Coordinate arg2, uint arg3)
    {
        // Check if move is valid
        // send move to all clients
        throw new NotImplementedException();
    }
}
