using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapPopulator : MonoBehaviour
{
    public GameObject playerRef;


    public Grid SideScrollerGrid;
    public Tilemap sideScrollerMap;
    public Grid TopDownGrid;
    public Tilemap topDownMap;

    [SerializeField]
    List<KeyValuePair<Vector2Int, TileBase>> FetchedTiles;

    [ContextMenu("Run FetchMap")]
    public void FetchSCMap()
    {
        sideScrollerMap.ClearAllTiles();
        //Fetch player pos
        int fetchedZposition = Mathf.FloorToInt(playerRef.transform.position.z);
        int fetchedXPosition = Mathf.FloorToInt(playerRef.transform.position.x);

        int searchWidth = 10;

        //search through and fill Fetched tiles with tiles form topdowngrid;
        for (int x = fetchedXPosition-searchWidth; x < fetchedXPosition + searchWidth; x++)
        {
            sideScrollerMap.SetTile(new Vector3Int(x, Mathf.FloorToInt(TopDownGrid.transform.position.y - 1), 0) ,topDownMap.GetTile(new Vector3Int(x, 0, fetchedZposition)));
            //FetchedTiles.Add(new KeyValuePair<new Vector2Int(x, 0), topDownMap.GetTile(new Vector3Int(x, 0, fetchedZposition)) > );
        }

        //add tiles to respective position in sideScrollerGrid
    }


}
