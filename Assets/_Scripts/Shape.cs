using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public float x, y, z;
    public bool isVisible;

    private void Awake()
    {
        this.SetClickAble(false);
    }

    public void SetName(string name)
    {
        gameObject.name = name;
    }

    public string GetName()
    {
        return gameObject.name;
    }

    public void SetPosition(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        gameObject.transform.position = new Vector3(x, y, z);
    }

    public void MoveToXY(float x, float y)
    {
        this.x = x;
        this.y = y;
        gameObject.transform.position = new Vector3(x, y, z);
    }

    public void SetColor(string hexColor)
    {
        Color c;
        if (ColorUtility.TryParseHtmlString(hexColor, out c))
        {
            GetComponent<Renderer>().material.color = c;
        }
    }

    public void SetSize(int w, int h, int d)//d:depth
    {
        transform.localScale = new Vector3(w, h, d);
    }

    public void ScaleSize(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetVisible(bool visible)
    {
        GetComponent<Renderer>().enabled = visible;
    }

    public void SetClickAble(bool able)
    {
        GetComponent<Collider>().enabled = able;
        this.isVisible = able;
    }
}
