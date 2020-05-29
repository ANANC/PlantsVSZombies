using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenMap : Map
{
    public class GardenCell
    {
        public Vector3 Position;
        public bool Hold;
        public List<int> MapObjectIds;
    }

    public const int GardenWidth = 2;
    public const int GardenHeight = 4;


    private Dictionary<Vector3, GardenCell> GardenCellDict;

    public override void Enter()
    {
        InitGardenEnviorment();
    }

    public override void Exist()
    {
    }

    private void InitGardenEnviorment()
    {
        GardenCellDict = new Dictionary<Vector3, GardenCell>();
        for (int h = 0; h < GardenHeight; h++)
        {
            for (int w = 0; w < GardenWidth; w++)
            {
                GardenCell cell = new GardenCell();
                Vector3 position = new Vector3(w, h, 0);
                cell.Position = position;
                cell.Hold = false;
                cell.MapObjectIds = null;
                GardenCellDict.Add(position, cell);
            }
        }
    }

    public GardenCell GetGardenCell(Vector3 position)
    {
        GardenCell cell;
        if (GardenCellDict.TryGetValue(position, out cell))
        {
            return cell;
        }
        return null;
    }

    public bool IsCanCreateMapObjectToMap(Vector3 position)
    {
        GardenCell cell = GetGardenCell(position);
        if (cell == null)
        {
            return false;
        }

        if (!cell.Hold)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void AddMapObjectToMap(int mapObjectId, Vector3 position)
    {
        GardenCell cell = GetGardenCell(position);
        if (cell == null)
        {
            return;
        }

        cell.Hold = true;
        if (cell.MapObjectIds == null)
        {
            cell.MapObjectIds = new List<int>() { mapObjectId };
        }
        else
        {
            cell.MapObjectIds.Add(mapObjectId);
        }
    }
}
