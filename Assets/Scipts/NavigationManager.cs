using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{

    [Tooltip("Включение/Отключение прорисовки сетки навигации.")]
    public bool DrawGrid = true;

    private void Awake()
    {
        if (NavigationMap.MapData==null) InitializationMapData();
    }


    private void OnDrawGizmos()
    {
        if (DrawGrid)
        {
            for (int i = 0; i < NavigationMap.Size.x; i++)
            {
                for (int j = i%2; j < NavigationMap.Size.y; j++)
                {
                    if (i != 0) Gizmos.DrawLine(NavigationMap.GetCoords(new Vector2(i, j)), NavigationMap.GetCoords(new Vector2(i - 1, j)));
                    if (i != NavigationMap.Size.x - 1) Gizmos.DrawLine(NavigationMap.GetCoords(new Vector2(i, j)), NavigationMap.GetCoords(new Vector2(i + 1, j)));

                    if (j != 0) Gizmos.DrawLine(NavigationMap.GetCoords(new Vector2(i, j)), NavigationMap.GetCoords(new Vector2(i, j - 1)));
                    if (j != NavigationMap.Size.y - 1) Gizmos.DrawLine(NavigationMap.GetCoords(new Vector2(i, j)), NavigationMap.GetCoords(new Vector2(i, j + 1)));
                    j++;
                }
            }

            if (NavigationMap.MapData != null)
            {
                for (int i = 0; i < NavigationMap.Size.x; i++)
                {
                    for (int j = 0; j < NavigationMap.Size.y; j++)
                    {
                        Gizmos.color = (NavigationMap.MapData[i, j] == 0) ? Color.green : Color.red;
                        Gizmos.DrawCube(NavigationMap.GetCoords(new Vector2(i, j)), new Vector2(NavigationMap.SizeCell / 5, NavigationMap.SizeCell / 5));
                    }
                }
            }
        }
    }

    private void InitializationMapData() 
    {
        NavigationMap.MapData = new int[(int)NavigationMap.Size.x, (int)NavigationMap.Size.y];
        for (int i = 0; i < NavigationMap.Size.x; i++)
        {
            for (int j = 0; j < NavigationMap.Size.y; j++)
            {
                NavigationMap.MapData[i, j] = 0;
            }
        }
    }
}
