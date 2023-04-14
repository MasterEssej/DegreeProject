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

    private void Start()
    {
        tileGrid = new();
        tileGrid.grid = TopDownGrid;

        tileGrid.tilemaps = tileGrid.grid.GetComponentsInChildren<Tilemap>();
    }

    [ContextMenu("Run FetchMap")]
    public void FetchSCMap(int rotationX, int rotationZ, float angleCheck)
    {
        sideScrollerMap.ClearAllTiles();
        //Fetch player pos
        int fetchedZposition = Mathf.FloorToInt(playerRef.transform.position.z);
        int fetchedXposition = Mathf.FloorToInt(playerRef.transform.position.x);

        //Line up SideScroller tilemap with player
        var scTransform = sideScrollerMap.transform;
        scTransform.position = new Vector3(scTransform.position.x, scTransform.position.y, fetchedZposition + 0.5f);

        int searchWidth = 10;

        //search through and fill Fetched tiles with tiles form topdowngrid;
        for(int i = 0; i < tileGrid.tilemaps.Length; i++)
        {
            //if(angleCheck == 1)
            //{
            //    for (int x = fetchedXposition - searchWidth; x < fetchedXposition + searchWidth; x++)
            //    {
            //        sideScrollerMap.SetTile(new Vector3Int(x, Mathf.FloorToInt(tileGrid.tilemaps[i].transform.position.y - 1), 0), tileGrid.tilemaps[i].GetTile(new Vector3Int(x*rotationX, fetchedZposition, 0)));
            //    }
            //}
            //else if(angleCheck == -1)
            //{
            //    for (int z = fetchedZposition - searchWidth; z < fetchedZposition + searchWidth; z++)
            //    {
            //        sideScrollerMap.SetTile(new Vector3Int(z, Mathf.FloorToInt(tileGrid.tilemaps[i].transform.position.y - 1), 0), tileGrid.tilemaps[i].GetTile(new Vector3Int(fetchedXposition, z*rotationZ, 0)));
            //    }
            //}

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

        //add tiles to respective position in sideScrollerGrid


        //For Rotation:
        //Rotate SideScroller view
        //Swap fetchedZposition and fetchedXposition
        //Swap x and fetchedZposition in GetTile
        //Done


    }

    
    
    // sort int arguments a,b,z in ascending order. after sorting the ints, create a dictionary with the sorted ints as values and their number in the sorted order as keys starting with 1. return the sorted ints as a string.
    public string SortInts(int a, int b, int c)
    {
        int[] ints = new int[3] { a, b, c };
        System.Array.Sort(ints);
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 0; i < ints.Length; i++)
        {
            dict.Add(i + 1, ints[i]);
        }
        return dict[1].ToString() + dict[2].ToString() + dict[3].ToString();
    }



}

public struct TileGridStruct
{

    public Grid grid;

    public Tilemap[] tilemaps;

}