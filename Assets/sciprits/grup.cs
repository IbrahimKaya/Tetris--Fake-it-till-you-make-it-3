using UnityEngine;

public class grup : MonoBehaviour {
    float lastFall = 0;
    static float gameSpeed =1.0f;

	void Start () {
        if (!İsValidGridPos())
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
	}

     void speedUp()
    {
        for (int y = 0; y < myGridSc.heigth; ++y)
        {
            if (myGridSc.isRowFull(y))
            {
                if (gameSpeed > 0.1f)
                {
                    gameSpeed = gameSpeed - 0.05f;
                }
                break;
            }
        }
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (İsValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (İsValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position +=new Vector3 (-1, 0, 0);
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.gameObject.name!="Square(Clone)")
            transform.Rotate(0, 0, 90);

            if (İsValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.Rotate (0, 0, -90);
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow)||Time.time-lastFall>=gameSpeed)
        {
            transform.position += new Vector3(0, -1, 0);

            if (İsValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                speedUp();

                myGridSc.deleteFullRows();

                FindObjectOfType<spawner>().spawnNext();
                enabled = false;       
            }

            lastFall = Time.time;
        }
	}

    bool İsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = myGridSc.roundVector2(child.position);

            if (!myGridSc.insideTheborder(v))
                return false;

            if (myGridSc.myGrid[(int)v.x, (int)v.y] != null &&
                myGridSc.myGrid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

  public void updateGrid()
    {
        for (int y = 0; y < myGridSc.heigth; ++y)
            for (int x = 0; x < myGridSc.with; ++x)
                if (myGridSc.myGrid[x, y] != null)
                    if (myGridSc.myGrid[x, y].parent == transform)
                        myGridSc.myGrid[x, y] = null;

        foreach(Transform child in transform)
        {
            Vector2 v = myGridSc.roundVector2(child.position);
            myGridSc.myGrid[(int)v.x, (int)v.y] = child;
        }
    }
}
