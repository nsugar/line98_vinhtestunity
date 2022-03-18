using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum CellState{ Blank, Fill };
    public CellState cellState = CellState.Blank;
    public enum CellEventState { Select, Deselect };
    public CellEventState cellEventState = CellEventState.Deselect;

    void Start()
    {

    }

    void Update()
    {

    }
}
