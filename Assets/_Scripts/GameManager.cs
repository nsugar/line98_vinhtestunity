using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<Cell> cellArr;
    private List<Ball> ballArr;
    private List<int> mapArr;
    /*
     * 0:   emtpy
     * 1:   small ball
     * 2:   big ball
     * 3:   user ball-xy choice
     */
    private const int NUM_TILES_PER_COL = 9;

    public enum GameState { Initial, Standby, NewGame, Playing, GameOver };
    public GameState gameState;

    void Start()
    {
        cellArr = new List<Cell>();
        ballArr = new List<Ball>();
        mapArr = new List<int>();
        gameState = GameState.Initial;
    }

    // Update is called once per frame
    void Update()
    {
        GameLoop();
        InputHandler();
    }

    void GameLoop()
    {
        switch (gameState)
        {
            case GameState.Initial:
                initGUI();
                gameState = GameState.NewGame;
                break;
            case GameState.Standby:
                //waiting for user start
                break;
            case GameState.NewGame:
                NewGame();
                gameState = GameState.Playing;
                break;
            case GameState.Playing:
                //process logic
                break;
            case GameState.GameOver:
                gameState = GameState.Standby;
                break;
        }
    }

    void OnGameObjectClicked(GameObject go)
    {
        Debug.Log("name is: " + go.name);
    }

    void NewGame()
    {
        //first start => genenate: 7 big balls, 3 small balls
        if (ballArr.Count > 0)
        {
            for (int i = 0; i < 10; i++)
            {
                Ball ball = ballArr[i];
                ball.SetVisible(true);
                ball.RandomColor();

                int mpos = RandomFreePositionBallMap();
                mapArr[mpos] = 2; //2: big ball

                if (i > 6)
                {
                    mapArr[mpos] = 1; //1: small ball
                    ball.ScaleSize(0.5f);
                }

                Debug.Log("mpos: " + (mpos + 1) + " -- aar: " + mapArr[mpos]);

                Cell cell = cellArr[mpos];
                ball.MoveToXY(cell.x, cell.y);
            }
        }

        DebugMap();
    }

    void EndTurn()
    {
        //random
    }

    void AddNewScore()
    {

    }

    void GameOver()
    {

    }

    int RandomFreePositionBallMap()
    {
        if (mapArr.Count > 0)
        {
            List<int> freeSpaceIndexArr = new List<int>();

            //check free space, avoid duplicating
            for (int i = 0; i < mapArr.Count; i++)
            {
                if (mapArr[i] == 0)
                {
                    freeSpaceIndexArr.Add(i);
                }
            }

            DebugMap();

            if (freeSpaceIndexArr.Count > 0)
            {
                return Random.Range(0, freeSpaceIndexArr.Count);
            }
        }
        return 0;
    }

    void DebugMap()
    {
        string map = "";
        for (int i = 0; i < mapArr.Count; i++)
        {
            map += mapArr[i] + ", ";
            if ((i + 1) % 9 == 0)
            {
                map += "\n";
            }
        }

        Debug.Log(map);
    }

    void initGUI()
    {
        //init gui programmatically
        for (int i = 0; i < NUM_TILES_PER_COL; i++)
        {
            for (int j = 0; j < NUM_TILES_PER_COL; j++)
            {
                //tile cells
                GameObject cgo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Cell cell = cgo.AddComponent<Cell>();
                cell.SetName("Cell" + ((j + 1) + (i * NUM_TILES_PER_COL)));
                float w = cgo.GetComponent<Renderer>().bounds.size.x;
                float h = cgo.GetComponent<Renderer>().bounds.size.y;
                float x = (w * (j * 1.1f)) + 1;
                float y = (h * (i * 1.1f)) + 1;
                cell.SetPosition(x, y, 0);
                cell.SetClickAble(true);
                cellArr.Add(cell);

                //balls
                GameObject bgo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Ball ball = bgo.AddComponent<Ball>();
                ball.SetName("Ball");
                ball.SetPosition(-5, -5, -5);
                ball.SetVisible(false);
                ballArr.Add(ball);

                mapArr.Add(0);//first time empty
            }
        }

        GameObject bggo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cell bg = bggo.AddComponent<Cell>();
        bg.SetName("BoardBg");
        bg.SetColor("#595959");
        bg.SetPosition(5, 5, 10);
        bg.SetSize(20, 20, 1);
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
                    OnGameObjectClicked(go);
                }
            }
        }
#endif
    }
}