using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMap : Map
{
    public class Cell
    {
        public Vector3 Position;
        public bool Hold;
        public List<int> MapObjectIds;
    }

    private Dictionary<Vector3, Cell> GardenCellDict;

    public override void Enter()
    {
        InitGardenEnviorment();
    }

    public override void Exist()
    {
    }

    private void InitGardenEnviorment()
    {
        GardenCellDict = new Dictionary<Vector3, Cell>();

        for (int h = 0; h < GameDefine.Garden.GardenWidth; h++)
        {
            for (int w = 0; w < GameDefine.Garden.GardenWidth; w++)
            {
                Cell cell = new Cell();
                Vector3 position = new Vector3(w, h, 0);
                cell.Position = position;
                cell.Hold = false;
                cell.MapObjectIds = null;

                GardenCellDict.Add(position, cell);
            }
        }
    }

    public Cell GetGardenCell(Vector3 position)
    {
        Cell cell;
        if (GardenCellDict.TryGetValue(position, out cell))
        {
            return cell;
        }
        return null;
    }

    public bool IsCanCreateMapObjectToMap(Vector3 position)
    {
        Cell cell = GetGardenCell(position);
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
        Cell cell = GetGardenCell(position);
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
