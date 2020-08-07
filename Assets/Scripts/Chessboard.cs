using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPiece { Pawn, Knight, Bishop, Rook, Queen, King };

public class Chessboard : MonoBehaviour
{
    public bool Networked;
    
    private Board _board;
    private bool _isPieceClicked = false;
    private Piece _pieceClicked;
    private List<Coordinate> _validMoves = new List<Coordinate>();

    public event Action<Coordinate, Coordinate> OnPieceMove;

    // Start is called before the first frame update
    void Start()
    {
        _board = gameObject.GetComponent<BoardSpawner>().SpawnBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Coordinate square = CoordinateHelper.Vector3ToCoordinate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (_isPieceClicked)
            {
                // Update when clicking new tile on board
                if (_validMoves.Contains(square))
                {
                    OnPieceMove?.Invoke(_pieceClicked.Position, square);
                    if (!Networked)
                    {
                        MoveTo(_pieceClicked.Position, square);
                    }
                }
                _validMoves.Clear();
                _isPieceClicked = false;
            }
            else if (_board[square] != null)
            { 
                _isPieceClicked = true;
                _pieceClicked = _board[square];
                FindValidMoves(_pieceClicked);

                // Print clicked piece's moves for debug purposes
                //Debug.LogFormat("{0} move: {1}", _pieceClicked.Type.ToString(), string.Join(", ", _validMoves));
            }
        }
    }

    // Returns true if move was valid
    public bool MoveTo(Coordinate src, Coordinate dest)
    {
        Piece piece = _board[src];

        if (piece != null)
        {
            // TODO: Validate that move was valid

            // Kills piece at dest
            if (_board[dest] != null)
            {
                Destroy(_board[dest].gameObject);
            }

            _board[src] = null;
            piece.MoveTo(dest);
            _board[dest] = piece;

            // TODO: Prompt Pawn promotion if needed

            return true;
        }

        return false;
    }

    public void FindValidMoves(Piece piece)
    {
        switch (piece.Type)
        {
            case ChessPiece.Pawn:
                FindPawnMoves(piece);
                break;
            case ChessPiece.Knight:
                FindKnightMoves(piece);
                break;
            case ChessPiece.Bishop:
                FindBishopMoves(piece);
                break;
            case ChessPiece.Rook:
                FindRookMoves(piece);
                break;
            case ChessPiece.Queen:
                FindQueenMoves(piece);
                break;
            case ChessPiece.King:
                FindKingMoves(piece);
                break;
        }
    }

    private void FindKingMoves(Piece piece)
    {
        throw new NotImplementedException();
    }

    private void FindQueenMoves(Piece piece)
    {
        throw new NotImplementedException();
    }

    private void FindRookMoves(Piece piece)
    {
        throw new NotImplementedException();
    }

    private void FindBishopMoves(Piece piece)
    {
        throw new NotImplementedException();
    }

    private void FindKnightMoves(Piece piece)
    {
        throw new NotImplementedException();
    }

    private void FindPawnMoves(Piece piece)
    {
        Coordinate current = piece.Position;
        // TODO: Implement 'En Passant'
        if (piece.IsWhite)
        {
            // 1 step in front
            if (_board[current.column, current.row + 1] == null)
            {
                _validMoves.Add(new Coordinate(current.column, (byte)(current.row + 1)));

                // 2 steps forward if starting
                if (current.row == 2 && _board[current.column, 4] == null)
                    _validMoves.Add(new Coordinate(current.column, 4));
            }

            // Check captures
            if (current.column > 'a' && _board[current.column - 1, current.row + 1] != null)
                _validMoves.Add(new Coordinate((char)(current.column - 1), (byte)(current.row + 1)));
            if (current.column < 'h' && _board[current.column + 1, current.row + 1] != null)
                _validMoves.Add(new Coordinate((char)(current.column + 1), (byte)(current.row + 1)));
        }
        else
        {
            // 1 step in front
            if (_board[current.column, current.row - 1] == null)
            {
                _validMoves.Add(new Coordinate(current.column, (byte)(current.row - 1)));

                // 2 steps forward if starting
                if (current.row == 7 && _board[current.column, 5] == null)
                    _validMoves.Add(new Coordinate(current.column, 5));
            }

            // Check captures
            if (current.column > 'a' && _board[current.column - 1, current.row - 1] != null)
                _validMoves.Add(new Coordinate((char)(current.column - 1), (byte)(current.row - 1)));
            if (current.column < 'h' && _board[current.column + 1, current.row - 1] != null)
                _validMoves.Add(new Coordinate((char)(current.column + 1), (byte)(current.row - 1)));
        }
    }
}
