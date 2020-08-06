using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ChessNetworker _chessNetworker;
    private UIController _uiController;
    public bool IsServer => _chessNetworker.IsServer; //Expose IsServer from networker
    private Chessboard _board;

    private void Awake()
    {
        if (Application.isBatchMode)
        {
            StartServer();
        }
        
        MainMenuController.PlayPressed += StartClient;
        MainMenuController.HostPressed += StartServer;
        
        _uiController = new UIController();
        _uiController.LoadMainMenu();

        _board = GameObject.Find("Chessboard").GetComponent<Chessboard>();
        _board.enabled = false;

        _uiController.MainMenuUnloaded += () =>
        {
            _board.enabled = true;
        };
    }

    private void StartServer()
    {
        _chessNetworker = new ChessNetworker(ChessNetworker.NetworkerType.Server, gameObject);
        _chessNetworker.ClientMoveReceived += OnClientMoveReceived;
    }

    private void StartClient()
    {
        _chessNetworker = new ChessNetworker(ChessNetworker.NetworkerType.Client, gameObject);
        _chessNetworker.ServerMoveReceived += OnServerMoveReceived;
        
        _board.OnPieceMove += BoardOnOnPieceMove;
    }

    private void BoardOnOnPieceMove(Coordinate source, Coordinate destination)
    {
        // TODO: Hook in the clock too
        _chessNetworker.SendClientMove(source, destination);
    }

    private void OnServerMoveReceived(Coordinate source, Coordinate dest, TimeSpan arg3)
    {
        // Apply move to board
        _board.MoveTo(source, dest);
    }

    private void OnClientMoveReceived(Coordinate source, Coordinate dest, uint peerId)
    {
        // TODO: Check if move is valid
        // TODO: send move to all clients
        
        bool valid = _board.MoveTo(source, dest);
        
        if (valid)
            _chessNetworker.SendServerMove(source, dest, TimeSpan.Zero);
    }
}
