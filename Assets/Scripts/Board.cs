using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPiece { Pawn, Knight, Bishop, Rook, Queen, King };

public class Board : MonoBehaviour
{
    public bool Networked;
    
    Piece[] board;

    bool isPieceClicked = false;
    Piece pieceClicked;
    private bool gameStarted = false;
    public event Action<Coordinate, Coordinate> OnPieceMove;

    // Start is called before the first frame update
    void Start()
    {
        board = gameObject.GetComponent<BoardSpawner>().SpawnBoard();
    }

    private void Awake()
    {
        UIController.MainMenuUnloaded += () => { gameStarted = true; };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameStarted)
        {
            Coordinate square = CoordinateHelper.Vector3ToCoordinate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (isPieceClicked)
            {
                // Update when clicking new tile on board
                if (square != pieceClicked.Position)
                {
                    OnPieceMove?.Invoke(pieceClicked.Position, square);
                    if (!Networked)
                    {
                        MoveTo(pieceClicked.Position, square);
                    }
                }
                isPieceClicked = false;
            }
            else if (board[CoordinateHelper.CoordinateToArrayIndex(square)] != null)
            { 
                isPieceClicked = true;
                pieceClicked = board[CoordinateHelper.CoordinateToArrayIndex(square)];
            }
        }
    }

    // Returns true if move was valid
    public bool MoveTo(Coordinate src, Coordinate dest)
    {
        Piece piece = board[CoordinateHelper.CoordinateToArrayIndex(src)];
        if (piece != null)
        {
            board[CoordinateHelper.CoordinateToArrayIndex(src)] = null;
            piece.MoveTo(dest);
            board[CoordinateHelper.CoordinateToArrayIndex(dest)] = piece;

            return true;
        }

        return false;
    }
}
