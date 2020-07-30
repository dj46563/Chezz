using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordinateHelper
{
    public static Vector3 CoordinateToVector3(Coordinate coordinate)
    {
        return new Vector3
        {
            x = (float)(coordinate.column - 'a' - 3.5),
            y = (float)(coordinate.row - 1 - 3.5),
            z = 0
        };
    }

    public static Coordinate Vector3ToCoordinate(Vector3 vector)
    {
        return new Coordinate
        {
            column = (char)((int)(vector.x + 4) + 'a'),
            row = (byte)((int)(vector.y + 4) + 1)
        };
    }

    public static int CoordinateToArrayIndex(Coordinate coordinate)
    {
        return (coordinate.row - 1) * 8 + (coordinate.column - 'a');
    }
}
