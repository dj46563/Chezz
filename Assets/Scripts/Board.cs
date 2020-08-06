using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Piece[] Chessboard { get; set; }

    public Board()
    {
        Chessboard = new Piece[64];
    }

    public Board(Piece[] chessboard)
    {
        Chessboard = chessboard;
    }

    // Indexers
    public Piece this[int i]
    {
        get { return Chessboard[i]; }
        set { Chessboard[i] = value; }
    }

    public Piece this[Coordinate coordinate]
    {
        get { return Chessboard[CoordinateHelper.CoordinateToArrayIndex(coordinate)]; }
        set { Chessboard[CoordinateHelper.CoordinateToArrayIndex(coordinate)] = value; }
    }
}
