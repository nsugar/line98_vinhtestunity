using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<GameObject> cellArr;
    private List<GameObject> ballArr;
    private const int NUM_TILES_PER_COL = 9;
    private string[] BallHexColorArr = {
        "#ff0000",  //red
        "#0000ff",  //blue
        "#008000",  //green
        "#00ffff",  //cyan
        "#ffff00",  //yellow
        "#8b4513"   //brown
    };

    public enum GameState { Initial, Standby, NewGame, Playing, GameOver };
    public GameState gameState = GameState.Initial;

    void Start()
    {
        cellArr = new List<GameObject>();
        ballArr = new List<GameObject>();
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
                break;
            case GameState.Standby:
                //waiting for user start
                break;
            case GameState.NewGame:
                NewGame();
                break;
            case GameState.Playing:
                //process logic
                break;
            case GameState.GameOver:
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
        for (int i = 0; i < 10; i++)
        {
            //balls
            GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ball.GetComponent<Collider>().enabled = false;
            ball.AddComponent<Ball>().SetBallColor(ball, RandomBallColor());

            //get Cell position
            GameObject cell = cellArr[RandomBallPosition()];
            float x = cell.transform.position.x;
            float y = cell.transform.position.y;

            ball.transform.position = new Vector3(x, y, -5);
            ball.name = "ball" + (i + 1);
            ballArr.Add(ball);

            if (i > 6)
            {
                ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        //SetBallColor(ball, Random.Range(0, 6));
        gameState = GameState.Playing;
    }

    void EndTurn()
    {

    }

    void AddNewScore()
    {

    }

    void GameOver()
    {

    }

    string RandomBallColor()
    {
        return BallHexColorArr[Random.Range(0, 6)];
    }

    int RandomBallPosition()
    {
        if (cellArr.Count > 0)
        {
            return Random.Range(0, cellArr.Count);
        }
        return 0;
    }

    void initGUI()
    {
        //init gui programmatically
        for (int i = 0; i < NUM_TILES_PER_COL; i++)
        {
            for (int j = 0; j < NUM_TILES_PER_COL; j++)
            {
                //tile cells
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.name = "cell" + ((j + 1) + (i * NUM_TILES_PER_COL));
                float w = cell.GetComponent<Renderer>().bounds.size.x;
                float h = cell.GetComponent<Renderer>().bounds.size.y;
                float x = (w * (j * 1.1f)) + 1;
                float y = (h * (i * 1.1f)) + 1;
                cell.transform.position = new Vector3(x, y, 0);
                cellArr.Add(cell);
            }
        }

        GameObject boardBg = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boardBg.name = "BoardBg";
        boardBg.GetComponent<Renderer>().material.color = Color.grey;
        boardBg.transform.position = new Vector3(5, 5, 10);
        boardBg.transform.localScale += new Vector3(20, 20, 1);
        boardBg.GetComponent<Collider>().enabled = false;

        //gameState = GameState.Standby;
        gameState = GameState.NewGame;
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

