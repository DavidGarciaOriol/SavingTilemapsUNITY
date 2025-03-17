using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FileManager : MonoBehaviour
{
    
    [SerializeField]
    Tilemap tilemap;

    [SerializeField]
    List<TileBase> tiles;

    TileData tileData;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);

        // SaveData();

        LoadData();
    }

    private void SaveData()
    {

        // Recorrer tilemap y guardar en un archivo
        tileData = new TileData();
        tileData.tiles = new List<Tile>();

        for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);

                if (tile != null)
                {
                    Tile t = new Tile();
                    t.name = tile.name;
                    t.position = new Position(x, y);
                    tileData.tiles.Add(t);
                }
            }
        }

        SaveDataFile();
    }

    private void SaveDataFile()
    {
        // Archivo persistente

        FileStream fileStream = new FileStream(Application.persistentDataPath + "/my_tilemap.dat", FileMode.OpenOrCreate);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, tilemap);
        fileStream.Close();
    }

    private void LoadData()
    {
        // Archivo persistente

        FileStream fileStream = new FileStream(Application.persistentDataPath + "/my_tilemap.dat", FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        tileData = (TileData) binaryFormatter.Deserialize(fileStream);
        fileStream.Close();

        foreach (Tile tile in tileData.tiles)
        {
            Vector3Int pos = new Vector3Int(tile.position.x, tile.position.y, 0);
            // tilemap.SetTile(pos, Resources.Load<Tile>())
        }
    }

    void Update()
    {
        
    }
}
