using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapPopulator : MonoBehaviour
{
    public GameObject playerRef;

    public TileConverter converter;

    public Grid SideScrollerGrid;
    public Tilemap sideScrollerMap;
    public Grid TopDownGrid;
    public Tilemap topDownMap;

    public TileGridStruct tileGrid;

    private float scMapOffsetX = 0f;
    private float scMapOffsetZ = 0f;

    private float newSCPosX;
    private float newscPosZ;

    private Transform scTransform;
    private Vector3 scInitialPosition;

    private void Start()
    {
        tileGrid = new();
        tileGrid.grid = TopDownGrid;

        tileGrid.tilemaps = tileGrid.grid.GetComponentsInChildren<Tilemap>();

        // store sideScrollerMap.transform in a variable that will not change even if the transform changes.
        

        scInitialPosition = sideScrollerMap.transform.position;
        scTransform = sideScrollerMap.transform;
    }

    [ContextMenu("Run FetchMap")]
    public void FetchSCMap(int rotationX, int rotationZ, float angleCheck)
    {
        sideScrollerMap.ClearAllTiles();
        //Fetch player pos
        int fetchedZposition = Mathf.FloorToInt(playerRef.transform.position.z);
        int fetchedXposition = Mathf.FloorToInt(playerRef.transform.position.x);
        
        if(rotationX == 1)
        {
            scMapOffsetX = 0f;
        }
        else if(rotationX == -1)
        {
            scMapOffsetX = 1f;
        }
        if(rotationZ == 1)
        {
            scMapOffsetZ = 0f;
        }
        else if(rotationZ == -1)
        {
            scMapOffsetZ = 1f;
        }

        //Line up SideScroller tilemap with player
        if(angleCheck == 1)
        {   
            newSCPosX = scInitialPosition.x + scMapOffsetX;
            newscPosZ = fetchedZposition + scMapOffsetX;
        }
        else if(angleCheck == -1)
        {
            newSCPosX = fetchedXposition + scMapOffsetX;
            newscPosZ = scInitialPosition.z + scMapOffsetZ;
        }

        scTransform.position = new Vector3(newSCPosX, scTransform.position.y, newscPosZ);

        int searchWidth = 10;

        //search through and fill Fetched tiles with tiles form topdowngrid;
        for(int i = 0; i < tileGrid.tilemaps.Length; i++)
        {

            if (angleCheck == 1)
            {
                for (int x = fetchedXposition - searchWidth; x < fetchedXposition + searchWidth; x++)
                {
                    var tile = tileGrid.tilemaps[i].GetTile(new Vector3Int(x * rotationX, fetchedZposition, 0));
                    var newTile = converter.ConvertTile(tile);
                    sideScrollerMap.SetTile(new Vector3Int(x, Mathf.FloorToInt(tileGrid.tilemaps[i].transform.position.y - 1), 0), newTile);
                }
            }
            else if (angleCheck == -1)
            {
                for (int z = fetchedZposition - searchWidth; z < fetchedZposition + searchWidth; z++)
                {
                    var tile = tileGrid.tilemaps[i].GetTile(new Vector3Int(fetchedXposition, z * rotationZ, 0));
                    var newTile = converter.ConvertTile(tile);
                    sideScrollerMap.SetTile(new Vector3Int(z, Mathf.FloorToInt(tileGrid.tilemaps[i].transform.position.y - 1), 0), newTile);
                }
            }

        }

    }

}

public struct TileGridStruct
{

    public Grid grid;

    public Tilemap[] tilemaps;

}