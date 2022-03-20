using System.Collections.Generic;
using UnityEngine;
using GameAI.PathFinding;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    private const int CELL_PER_ROWS = 9;
    private const int TOTAL_CELLS = 81;
    private List<Cell> cellArr;
    private List<Ball> ballArr;
    private int[] selectRoute = new int[] { -1, -1 }; //{start id, end id}
    private Cell clickcell;
    private int[,] map = new int[9, 9];

    private enum GameState { Initial, Standby, NewGame, Playing, CheckMove, GameOver };
    private GameState gameState;

    private enum PlayerTurnState { Thinking, BallSelected, DestSelected, CheckMove };
    private PlayerTurnState playerTurnState;

    void Start()
    {
        cellArr = new List<Cell>();
        ballArr = new List<Ball>();
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
                break;
            case GameState.NewGame:
                NewGame();
                break;
            case GameState.Playing:
                Playing();
                break;
            case GameState.CheckMove:
                Debug.Log("Check Move!");
                //CheckMove();
                gameState = GameState.Playing;
                break;
            case GameState.GameOver:
                gameState = GameState.Standby;
                break;
        }
    }

    void NewGame()
    {
        selectRoute = new int[] { -1, -1 };
        clickcell = null;

        //first start => genenate: 7 big balls, 3 small balls
        if (ballArr.Count > 0)
        {
            for (int i = 0; i < 10; i++)
            {
                int mpos = RandomFreePositionBallMap();

                if (mpos != -1)
                {
                    Ball ball = ballArr[i];
                    ball.SetVisible(true);
                    ball.RandomColor();

                    if (i > 6)
                    {
                        ball.SetSmall(0.5f);
                    }

                    //Debug.Log("mpos: " + (mpos + 1));

                    Cell cell = cellArr[mpos];
                    cell.AttachBall(ball);
                    ball.MoveToXY(cell.x, cell.y);
                }
            }
        }

        //generate new map
        //for (int i = 0; i < CELL_PER_ROWS; i++)
        //{
        //    for (int j = 0; j < TOTAL_CELLS; j++)
        //    {
        //        map[i, j] = 0;
        //    }
        //}

        gameState = GameState.Playing;
        playerTurnState = PlayerTurnState.Thinking;
    }

    void Playing()
    {
        //3 small balls -> 3 big balls
        //random 3 new small balls
        //new turn -> end turn
        //add new score
        if (clickcell != null)
        {
            switch (playerTurnState)
            {
                case PlayerTurnState.Thinking:
                    if (clickcell.HasBall() && (!clickcell.ball.isSmall))    //user clicks a ball
                    {
                        selectRoute[0] = clickcell.id;
                        clickcell.SetColor(Color.grey);
                        clickcell = null;
                        playerTurnState = PlayerTurnState.BallSelected;
                        Debug.Log("ball-id: " + selectRoute[0]);
                    }
                    break;

                case PlayerTurnState.BallSelected:
                    {
                        if (!clickcell.HasBall()) //user clicks new dest
                        {
                            selectRoute[1] = clickcell.id;
                            Debug.Log("destination!!!");
                            clickcell.SetColor(Color.grey);
                            playerTurnState = PlayerTurnState.DestSelected;
                            break;
                        }
                        if (clickcell.HasBall() && (!clickcell.ball.isSmall)) //user clicks new ball
                        {
                            if (clickcell.ball.id != selectRoute[0])
                            {
                                cellArr[selectRoute[0]].SetColor(Color.white);
                                selectRoute[0] = clickcell.id;
                                clickcell.SetColor(Color.grey);
                                clickcell = null;
                                Debug.Log("new ball-id: " + selectRoute[0]);
                                playerTurnState = PlayerTurnState.BallSelected;
                            }
                            break;
                        }
                    }

                    break;
                case PlayerTurnState.DestSelected:
                    clickcell = null;
                    gameState = GameState.CheckMove;
                    playerTurnState = PlayerTurnState.Thinking;
                    break;
            }
        }

    }

    int[] CheckMove()
    {
        int[,] Data = new int[2, 2];
        int[] SmallData;
        int[] bfs;
        int[] ans = new int[9];
        for (int i = 0; i < 9; i++)
        {
            int[] ans2 = new int[2] { i, 0 };

            for (int j = 1; j < 10; j++)
            {
                if (Data[i, j] == Data[i, j - 1] && Data[i, j - 1] != 0)
                {
                    ans2[0] = i;
                    ans2[1] = j;
                }
                else
                {
                    if (ans2.GetLength(0) >= 5)
                    {
                        //ans = ans.concat(ans2);
                    }
                    ans2[0] = i;
                    ans2[1] = j;
                }
            }
        }

        for (int j = 0; j < 9; j++)
        {
            int[] ans2 = new int[2] { 0, j };
            for (int i = 1; i < 10; i++)
            {
                if (Data[i, j] == Data[i, j - 1] && Data[i, j - 1] != 0)
                {
                    ans2[0] = i;
                    ans2[1] = j;
                }
                else
                {
                    if (ans2.GetLength(0) >= 5)
                    {
                        //ans = ans.concat(ans2);
                    }
                    ans2[0] = i;
                    ans2[1] = j;
                }
            }
        }

        for (int j = 0; j < 9; j++)
        {
            int[] ans2 = new int[2] { 0, j };
            int J = j;
            int I = 0;
            do
            {
                I++;
                J++;
                if (Data[I, J] == Data[I - 1, J - 1] && Data[I - 1, J - 1] != 0)
                {
                    ans2[0] = I;
                    ans2[1] = J;
                }
                else
                {
                    if (ans2.GetLength(0) >= 5)
                    {
                        //ans = ans.concat(ans2);
                    }
                    ans2[0] = I;
                    ans2[1] = J;
                }
            } while (I > 0);//while (isPoint(I, J));
            ans2[0] = j;
            ans2[1] = 0;
            J = 0;
            I = j;
            do
            {
                I++;
                J++;
                if (Data[I, J] == Data[I - 1, J - 1] && Data[I - 1, J - 1] != 0)
                {
                    ans2[0] = I;
                    ans2[1] = J;
                }
                else

                {
                    if (ans2.GetLength(0) >= 5)
                    {
                        Debug.Log(ans2);
                        //ans = ans.concat(ans2);
                    }
                    ans2[0] = I;
                    ans2[1] = J;
                }
            } while (I > 0);// (this.isPoint(I, J));
        }

        for (int j = 0; j < 9; j++)
        {
            int[] ans2 = new int[2] { 0, j };
            int J = j;
            int I = 0;
            do
            {
                I++;
                J--;
                if (Data[I, J] == Data[I - 1, J - 1] && Data[I - 1, J - 1] != 0)
                {
                    ans2[0] = I;
                    ans2[1] = J;
                }
                else
                {
                    if (ans2.GetLength(0) >= 5)
                    {
                        //ans = ans.concat(ans2);
                    }
                    ans2[0] = I;
                    ans2[1] = J;
                }
            } while (I > 0);// (this.isPoint(I, J)) ;

            ans2 = new int[2] { j, 8 };
            J = 8;
            I = j;
            do
            {
                I++;
                J--;
                if (Data[I, J] == Data[I - 1, J - 1] && Data[I - 1, J - 1] != 0)
                {
                    ans2[0] = I;
                    ans2[1] = J;
                }
                else
                {
                    if (ans2.GetLength(0) >= 5)
                    {
                        Debug.Log(ans2);
                        //ans = ans.concat(ans2);
                    }
                    ans2[0] = I;
                    ans2[1] = J;
                }
            } while (this.isPoint(I, J));
        }

        //return ans;
        return null;
    }

    public bool isPoint(int x, int y)
    {
        if (x < 0 || x > 8 || y < 0 || y > 8)
        {
            return false;
        }
        return true;
    }

    void GameOver()
    {

    }

    int RandomFreePositionBallMap()
    {
        List<int> freeSpaceIndexArr = new List<int>();

        for (int i = 0; i < TOTAL_CELLS; i++)
        {
            Cell cell = cellArr[i];

            if (cell.ball == null)
            {
                freeSpaceIndexArr.Add(i);
            }
        }

        if (freeSpaceIndexArr.Count > 0)
        {
            return Random.Range(0, freeSpaceIndexArr.Count);
        }
        return -1;
    }

    void initGUI()
    {
        //init gui programmatically
        for (int i = 0; i < CELL_PER_ROWS; i++)
        {
            for (int j = 0; j < CELL_PER_ROWS; j++)
            {
                //tile cells
                GameObject cgo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Cell cell = cgo.AddComponent<Cell>();
                int id = ((j + 1) + (i * CELL_PER_ROWS));
                cell.SetName("Cell" + id);
                cell.SetId(id - 1);
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
                ball.SetId(id - 1);
                ball.SetName("Ball");
                ball.SetPosition(-5, -5, -5);
                ball.SetVisible(false);
                ballArr.Add(ball);
            }
        }

        GameObject bggo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cell bg = bggo.AddComponent<Cell>();
        bg.SetName("BoardBg");
        bg.SetColor("#595959");
        bg.SetPosition(5, 5, 10);
        bg.SetSize(20, 20, 1);

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

                    //is Cell clicked
                    Cell cell = go.GetComponent<Cell>();
                    if (cell != null)
                    {
                        clickcell = cell;
                        Debug.Log("-----> " + cell.id);
                    }
                }
            }
        }
#endif
    }

    void DebugMap()
    {
        string map = "";
        for (int i = 0; i < TOTAL_CELLS; i++)
        {
            map += cellArr[i].ball.id + ", ";
            if ((i + 1) % 9 == 0)
            {
                map += "\n";
            }
        }

        Debug.Log(map);
    }
}
