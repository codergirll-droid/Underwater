using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleManager : MonoBehaviour
{
    public GameObject[] boxes;
    int boxCount = 0;
    public int currentBoxCount = 0;

    public static puzzleManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        currentBoxCount = 0;
        boxes = GameObject.FindGameObjectsWithTag("box");
        boxCount = boxes.Length;
    }

    public void checkCompleteness()
    {
        if(boxCount == currentBoxCount)
        {
            Debug.Log("Puzzle solved");
        }
    }
}
