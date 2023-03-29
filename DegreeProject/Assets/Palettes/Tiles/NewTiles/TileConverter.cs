using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileConverter : MonoBehaviour
{


    
    public List<KeyValuePair<TileBase, TileBase>> converter = new ();


    [SerializeField]
    private List<TileBase> TopDownList;

    [SerializeField]
    private List<TileBase> SideScrollerList;


    private void Awake()
    {
        for (int i = 0; i < TopDownList.Count; i++)
        {
            var pair = new KeyValuePair<TileBase, TileBase>(TopDownList[i], SideScrollerList[i]);
            converter.Add(pair);
        }
    }


    public TileBase ConvertTile(TileBase tile)
    {
        foreach (var pair in converter)
        {
            if (pair.Key == tile)
                return pair.Value;
        }
        return tile;
    }

}
