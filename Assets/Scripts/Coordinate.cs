using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    public static byte[] Serialize(Coordinate coordinate)
    {
        byte[] data = new byte[2];
        data[0] = (byte)coordinate.column;
        data[1] = coordinate.row;

        return data;
    }

    public static Coordinate Deserialize(byte[] data)
    {
        Coordinate coordinate;
        coordinate.column = (char)data[0];
        coordinate.row = data[1];
        
        return coordinate;
    }
}
