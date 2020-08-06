using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPiece { Pawn, Knight, Bishop, Rook, Queen, King };

public class Chessboard : MonoBehaviour
{
    public bool Networked;
    
    Board board;

    bool isPieceClicked = false;
    Piece pieceClicked;
    public event Action<Coordinate, Coordinate> OnPieceMove;

    // Start is called before the first frame update
    void Start()
    {
        board = gameObject.GetComponent<BoardSpawner>().SpawnBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
            else if (board[square] != null)
            { 
                isPieceClicked = true;
                pieceClicked = board[square];
            }
        }
    }

    // Returns true if move was valid
    public bool MoveTo(Coordinate src, Coordinate dest)
    {
        Piece piece = board[src];

        if (piece != null)
        {
            // Kills piece at dest
            if (board[dest] != null)
            {
                Destroy(board[dest].gameObject);
            }

            board[src] = null;
            piece.MoveTo(dest);
            board[dest] = piece;

            return true;
        }

        return false;
    }
}
