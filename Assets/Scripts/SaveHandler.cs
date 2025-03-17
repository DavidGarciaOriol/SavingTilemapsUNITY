using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class SaveHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    [SerializeField]
    BoundsInt limites;
    [SerializeField]
    string filename = "tilemapData.json";

    private void Start()
    {
        iniciarTilemaps();
    }

    /**
     * Obtiene todos los tilemaps de la escena
     * y los escribe en el diccionario
     */
    void iniciarTilemaps()
    {
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap map in maps)
        {
            tilemaps.Add(map.name, map);
        }
    }

    public void onSave()
    {
        List<TilemapData> datos = new List<TilemapData>();

        // Por cada tilemap existente
        foreach (var objetoTilemap in tilemaps)
        {
            TilemapData datosTilemap = new TilemapData();
            datosTilemap.key = objetoTilemap.Key;

            for (int x = limites.xMin; x < limites.xMax; x++)
            {
                for (int y = limites.yMin; y < limites.yMax; y++)
                {
                    Vector3Int posicion = new Vector3Int(x, y, 0);
                    TileBase tile = objetoTilemap.Value.GetTile(posicion);

                    if (tile != null)
                    {
                        TileInfo tileInfo = new TileInfo(tile, posicion);
                    }
                }
            }
        }
    }

    public void onLoad()
    {

    }
}

[Serializable]
public class TilemapData
{
    // key del diccionario
    public string key;
    public List<TileInfo> tiles = new List<TileInfo>();
}

[Serializable]
public class TileInfo
{
    public TileBase tile;
    public Vector3Int posicion;

    public TileInfo(TileBase tile, Vector3Int pos)
    {
        this.tile = tile;
        posicion = pos;
    }
}
