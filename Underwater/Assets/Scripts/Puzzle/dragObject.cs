using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragObject : MonoBehaviour
{
    Camera cam;
    Vector2 startPos;

    public GameObject[] boxes;

    private void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        //Debug.Log(Input.mousePosition);

        transform.position = pos;
    }

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        startPos = transform.position;

        boxes = GameObject.FindGameObjectsWithTag("box");
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach(GameObject box in boxes)
            {
                if(box.name == gameObject.name)
                {
                    float distance = Vector3.Distance(box.transform.position, transform.position);

                    if (distance <= 1)
                    {
                        transform.position = box.transform.position;
                        puzzleManager.Instance.currentBoxCount++;
                        this.enabled = false;
                        puzzleManager.Instance.checkCompleteness();
                        Destroy(this);
                    }
                    else
                    {
                        transform.position = startPos;
                    }
                }

                
            }
        }
    }
    
}
