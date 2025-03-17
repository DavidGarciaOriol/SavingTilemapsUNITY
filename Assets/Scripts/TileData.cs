
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

[Serializable]
public class Tile
{
    public string name;
    public Position position;
}

public class TileData : MonoBehaviour
{
    public List<Tile> tiles;
}
