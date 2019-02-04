using UnityEngine;


public class myGridSc : MonoBehaviour {

    public static int with = 10 ;
    public static int heigth =20;
    public static Transform[,] myGrid = new Transform[with, heigth];

    public static Vector2 roundVector2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideTheborder(Vector2 v)
    {
        return ((int)v.x >= 0 && (int)v.x < with && (int)v.y >= 0);
    }

    public static void deleteRow(int y)
    {
        for(int x = 0; x < with; ++x)
        {
            Destroy(myGrid[x,y].gameObject);
            myGrid[x, y] = null;
        }
    }

    public static void decreseaRow(int y)
    {
        for (int x = 0; x < with; ++x)
        {
            if (myGrid[x, y] != null)
            {
                myGrid[x, y - 1] = myGrid[x, y]; 
                myGrid[x, y].position +=new Vector3(0, -1, 0);
                myGrid[x, y] = null;
            }
        }
    }

    public static void decreseRowAbove(int y)
    {
        for (int i = y; i < heigth; ++i)
            decreseaRow(i);
    }
    
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < with; ++x)
            if (myGrid[x, y] == null)
                return false;
        return true;
    }

    public static void deleteFullRows()
    {
        for(int y = 0; y < heigth; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreseRowAbove(y + 1);
                y--;
            }
        }
    }
}
