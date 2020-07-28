using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPiece : MonoBehaviour
{
    public bool IsWhite { get; set; }
    public int PointValue { get; set; }
    public Coordinate Position { get; set; }

    public void MoveTo(Coordinate destination)
    {
        transform.position = CoordinateToVector3(destination);
        Position = destination;
    }

    private Vector3 CoordinateToVector3(Coordinate coordinate)
    {
        return new Vector3
        {
            x = (coordinate.column - 'a') * 8 - 28,
            y = (coordinate.row - 1) * 8 - 28,
            z = 0
        };
    }

    public abstract void Initialize(bool isWhite, int pointValue, Coordinate position);
}
