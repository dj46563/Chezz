using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPiece : MonoBehaviour
{
    public bool IsWhite { get; set; }
    public int PointValue { get; set; }
    public Coordinate Position { get; set; }

    public abstract void Initialize(bool isWhite, int pointValue, Coordinate position);
}
