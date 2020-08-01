using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPiece { Pawn, Knight, Bishop, Rook, Queen, King };

public class Board : MonoBehaviour
{
    Piece[] board;

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
                    board[CoordinateHelper.CoordinateToArrayIndex(pieceClicked.Position)] = null;
                    pieceClicked.MoveTo(square);
                    board[CoordinateHelper.CoordinateToArrayIndex(square)] = pieceClicked;
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

    public void MoveTo(Coordinate src, Coordinate dest)
    {
        Piece piece = board[CoordinateHelper.CoordinateToArrayIndex(src)];
        if (piece != null)
        {
            board[CoordinateHelper.CoordinateToArrayIndex(src)] = null;
            piece.MoveTo(dest);
            board[CoordinateHelper.CoordinateToArrayIndex(dest)] = piece;
        }
    }
}
