using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AddCollidersToTiles : MonoBehaviour
{
    [SerializeField]    private Grid TopDownGrid;
    private Tilemap[] tilemaps;
    
    private void Awake()
    {
        tilemaps = TopDownGrid.GetComponentsInChildren<Tilemap>();
        foreach(var tilemap in tilemaps)
        {
            AddCollidersToAllTiles(tilemap);
        }
    }

    private void AddCollidersToAllTiles(Tilemap tilemap)
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                Debug.Log(position);
                var tile = tilemap.GetTile(position);
                Debug.Log("Tile: " + tile);
                var tileGameObject = tilemap.GetInstantiatedObject(position);
                Debug.Log("Tile GameObject: " + tileGameObject);
                

                if (tileGameObject != null && tileGameObject.GetComponent<BoxCollider>() == null)
                {
                    tileGameObject.transform.position = tilemap.CellToWorld(position) + new Vector3(0.5f, -0.5f, 0.5f);
                    Debug.Log("Adding BoxCollider to tile");
                    var collider = tileGameObject.AddComponent<BoxCollider>();
                    collider.size = new Vector3(tilemap.cellSize.x, tilemap.cellSize.y, 1f);
                }
            }
        }
    }
}
