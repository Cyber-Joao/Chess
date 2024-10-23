using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScript : MonoBehaviour
{

    public GameObject tileprefab;

    public int height;
    public int width;
    public Color newwhite; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu ("create board")]  
    public void CreateBoard()
    {
        bool isOdd = true;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 tilePosition = new Vector3(x, y, 0);
                GameObject tile = Instantiate(tileprefab, transform.position + tilePosition, transform.rotation);
                if (isOdd == true)
                {
                    isOdd = false;
                }
                else
                {
                    tile.GetComponent<SpriteRenderer>().color = newwhite;
                    isOdd = true;
                }
            }
            if (isOdd == true)
            {
               isOdd = false;
            }
            else
            {
                isOdd = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
