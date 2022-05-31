using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(3).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").transform.position += new Vector3(10, 10, 10);

            GameManager.Instance.isPuzzleSolved = true;
            SceneManager.LoadScene(1);
            

            
            //open portal
        }
    }
}
