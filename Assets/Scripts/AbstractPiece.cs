using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPiece : MonoBehaviour
{
    public bool isWhite { get; set; }
    public int pointValue { get; set; }
    public Coordinate position { get; set; }

    public abstract Coordinate[] GetMoves();
}
