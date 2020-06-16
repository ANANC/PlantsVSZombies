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

    public const int GardenWidth = 2;
    public const int GardenHeight = 4;

    private Transform GardenParent;

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
        GardenParent = new GameObject("Garden").transform;

           GardenCellDict = new Dictionary<Vector3, Cell>();
        for (int h = 0; h < GardenHeight; h++)
        {
            for (int w = 0; w < GardenWidth; w++)
            {
                Cell cell = new Cell();
                Vector3 position = new Vector3(w, h, 0);
                cell.Position = position;
                cell.Hold = false;
                cell.MapObjectIds = null;

                GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(GameDefine.Path.Lawn);
                gameObject.transform.position = new Vector3(w * GameDefine.Art.GardenCellSize.x, h * GameDefine.Art.GardenCellSize.y, 0);
                gameObject.transform.SetParent(GardenParent);

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
