using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : AbstractPiece
{
    public override void Initialize(bool isWhite, int pointValue, Coordinate position)
    {
        IsWhite = isWhite;
        PointValue = pointValue;
        Position = position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveTo(new Coordinate('f', 4));
        }
    }
}
