using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Coordinate
{
    public char column;
    public byte row;

    public Coordinate(char inCol, byte inRow)
    {
        column = inCol;
        row = inRow;
    }
}
