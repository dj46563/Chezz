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
            _chessNetworker = new ChessNetworker(ChessNetworker.NetworkerType.Server, gameObject);
        else
            _chessNetworker = new ChessNetworker(ChessNetworker.NetworkerType.Client, gameObject);
    }
}
