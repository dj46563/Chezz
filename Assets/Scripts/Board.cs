using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private Piece[] _board { get; set; }

    public Board()
    {
        _board = new Piece[64];
    }

    public Board(Piece[] chessboard)
    {
        _board = chessboard;
    }

    // Indexers
    public Piece this[int i]
    {
        get { return _board[i]; }
        set { _board[i] = value; }
    }

    public Piece this[Coordinate coordinate]
    {
        get { return _board[CoordinateHelper.CoordinateToArrayIndex(coordinate)]; }
        set { _board[CoordinateHelper.CoordinateToArrayIndex(coordinate)] = value; }
    }

    public Piece this[int column, int row]
    {
        get { return _board[(row - 1) * 8 + (column - 'a')]; }
        set { _board[(row - 1) * 8 + (column - 'a')] = value; }
    }

}
