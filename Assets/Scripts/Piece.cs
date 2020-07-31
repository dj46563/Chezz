using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public ChessPiece Type { get; set; }
    public bool IsWhite { get; set; }
    public int PointValue { get; set; }
    public Coordinate Position { get; set; }

    public void MoveTo(Coordinate destination)
    {
        transform.position = CoordinateHelper.CoordinateToVector3(destination);
        Position = destination;
    }

    public void Initialize(ChessPiece type, bool isWhite, int pointValue, Coordinate position)
    {
        Type = type;
        IsWhite = isWhite;
        PointValue = pointValue;
        Position = position;
    }
}
