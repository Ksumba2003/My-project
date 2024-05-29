using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/ToolAction/Plow")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
        Debug.Log(tileToPlow);
        if (canPlow.Contains(tileToPlow) == false)
        {
            Debug.Log("Not plowable");
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);


        return true;
    }
}
