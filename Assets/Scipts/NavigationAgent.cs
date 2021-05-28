using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationAgent : MonoBehaviour
{

    private BoxCollider2D cl2D;
    private Rigidbody2D rb2D;

    //Матрица точек, которые "влезут" в коллайдер
    Vector2[,] points;

    [SerializeField] private Vector2 SizeObj;

    private void Start()
    {
        cl2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        //Определяем размеры объекта
        SizeObj = new Vector2(Mathf.Clamp(Mathf.Round(cl2D.size.x  / NavigationMap.SizeCell+2), 1, (int)(cl2D.size.x / NavigationMap.SizeCell + 2)), Mathf.Clamp(Mathf.Round(cl2D.size.y  / NavigationMap.SizeCell+2), 1, (int)(cl2D.size.y / NavigationMap.SizeCell + 2)));
        BlockCell();
    }

    private void BlockCell()
    {
        if (points == null) points= new Vector2[(int)SizeObj.x, (int)SizeObj.y];

        //Расстояние до границы коллайдера
        float XBorder = (cl2D.size.x % NavigationMap.SizeCell) / 2;
        float YBorder = (cl2D.size.y% NavigationMap.SizeCell) / 2;

        for (int i = 0; i < SizeObj.x; i++)
        {
            for (int j = 0; j < SizeObj.y; j++)
            {
                float H = (YBorder - cl2D.size.y / 2 + j * NavigationMap.SizeCell);
                float W = (XBorder - cl2D.size.x / 2 + i * NavigationMap.SizeCell);
                float a = transform.rotation.eulerAngles.z/180*Mathf.PI;

                points[i, j] = new Vector2(cl2D.bounds.center.x - H * Mathf.Sin(a) + W * Mathf.Cos(a), cl2D.bounds.center.y + H * Mathf.Cos(a) + W * Mathf.Sin(a));

                Vector2 indexes = NavigationMap.GetIndexes(points[i, j]);

                NavigationMap.MapData[(int)indexes.x, (int)indexes.y] = 1;
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (points != null)
        {
            foreach(Vector2 p in points)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(p, new Vector2(NavigationMap.SizeCell / 5, NavigationMap.SizeCell / 5));
            }   
        }
    }
}
