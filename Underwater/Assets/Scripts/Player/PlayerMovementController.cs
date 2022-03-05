using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementController : MonoBehaviour
{

    [Header("Move Variables")]
    public float moveAmount = 1;
    public float moveTimeAmount = 1;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        if(Input.GetKey(KeyCode.E))
        {
            this.transform.DOMoveY(transform.position.y + moveAmount, moveTimeAmount);

        }
        else if(Input.GetKey(KeyCode.Q))
        {
            this.transform.DOMoveY(transform.position.y - moveAmount, moveTimeAmount);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.DOMoveX(transform.position.x - moveAmount, moveTimeAmount);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            this.transform.DOMoveX(transform.position.x + moveAmount, moveTimeAmount);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.DOMoveZ(transform.position.z + moveAmount, moveTimeAmount);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            this.transform.DOMoveZ(transform.position.z - moveAmount, moveTimeAmount);
        }
    }

}
