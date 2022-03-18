using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<GameObject> spArr;
    private const int NUM_TILES_PER_COL = 9;

    // Start is called before the first frame update
    void Start()
    {
        spArr = new List<GameObject>();

        //init gui programmatically
        for (int i = 0; i < NUM_TILES_PER_COL; i++)
        {
            for (int j = 0; j < NUM_TILES_PER_COL; j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = "cube" + ((j + 1) + (i * NUM_TILES_PER_COL));
                //cube.transform.position = new Vector3(0, 0.5f, 0);

                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                //GameObject sp = Instantiate(spritePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                //string name = "spriteTile" + ((j + 1) + (i * NUM_TILES_PER_COL));
                //sp.name = name;
                //float w = sp.GetComponent<SpriteRenderer>().bounds.size.x;
                //float h = sp.GetComponent<SpriteRenderer>().bounds.size.y;
                //float x = 1;
                //float y = 1;
                ////if (j > 0) x = j + w;
                ////if (i > 0) y = i + h;
                //if (j > 0) x = j + w;
                //if (i > 0) y = i + h;


                //sp.transform.position = new Vector3(x, y, 0);
                //spArr.Add(sp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    void InputHandler()
    {
        //Mobile
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //logic
                }
            }
        }

        //PC
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    GameObject go = hit.collider.gameObject;
                    Debug.Log(go.name);
                }
            }
        }
#endif
    }
}

