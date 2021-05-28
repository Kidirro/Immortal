using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NavigationMap
{
    //Размеры 
    //N - ширина
    //M - высота
    const int N = 100; 
    const int M = 100;

    ///<summary>
    ///Размеры 
    ///N - ширина
    ///M - высота
    ///</summary>
    static public Vector2 Size
    {
        get { return new Vector2(N, M); }
    }

    ///<summary>
    ///Размеры клетки
    ///</summary>    
    static public float SizeCell
    {
        get { return _SizeCell; }
    }
    const float _SizeCell =0.1f;

    static public int[,] MapData;


    ///<param name="Coords">число</param>
    static public Vector2 GetIndexes(Vector3 Coords)
    {
        float RoundX= Mathf.Round(Coords.x / _SizeCell) * _SizeCell;
        float RoundY= Mathf.Round(Coords.y / _SizeCell) * _SizeCell;

        float XIndex = ((RoundX) / _SizeCell + Mathf.Round(N / 2));
        float YIndex = ((RoundY) / _SizeCell + Mathf.Round(M / 2));

        return new Vector2(XIndex,YIndex);
    }

    static public Vector2 GetCoords(Vector2 Indexes)
    {
        float XCoords = (Indexes.x - Mathf.Round(N / 2)) * _SizeCell;
        float YCoords = (Indexes.y - Mathf.Round(M / 2)) * _SizeCell;

        return new Vector2(XCoords, YCoords);
    }

}
