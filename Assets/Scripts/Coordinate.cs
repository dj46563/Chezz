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

    public static bool operator==(Coordinate c1, Coordinate c2)
    {
        return (c1.column == c2.column && c1.row == c2.row);
    }

    public static bool operator!=(Coordinate c1, Coordinate c2)
    {
        return !(c1.column == c2.column && c1.row == c2.row);
    }
}
