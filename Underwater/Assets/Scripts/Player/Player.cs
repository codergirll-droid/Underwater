using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int breath = 100;

    public static Player Instance;


    bool decreaseB = true;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }


        PlayerHUDController.Instance.breathSlider.value = breath;
        PlayerHUDController.Instance.healthSlider.value = health;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            addHealth(10);
            increaseBreath(10);

        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            decreaseBreath(10);
            takeDamage(10);

        }

        //StartCoroutine(decreaseBreathCoroutine());

                
    }



    public void takeDamage(int damageValue)
    {
        if(health - damageValue > 0)
        {
            health -= damageValue;
            PlayerHUDController.Instance.healthSlider.value -= damageValue;
        }
        else
        {
            health = 0;
            Debug.Log("Player died");
            PlayerHUDController.Instance.healthSlider.value -= damageValue;

        }

    }

    public void addHealth(int addHealthValue)
    {
        if(health + addHealthValue < 100)
        {
            health += addHealthValue;
            PlayerHUDController.Instance.healthSlider.value += addHealthValue;
        }
        else
        {
            health = 100;
            PlayerHUDController.Instance.healthSlider.value += addHealthValue;

        }

    }

    public void decreaseBreath(int breathValue)
    {
        if (breath - breathValue > 0)
        {
            breath -= breathValue;
            PlayerHUDController.Instance.breathSlider.value -= breathValue;
        }
        else
        {
            breath = 0;
            Debug.Log("Player started to lose health");
            PlayerHUDController.Instance.breathSlider.value -= breathValue;

        }
    }

    public void increaseBreath(int breathValue)
    {
        if (breath + breathValue < 100)
        {
            breath += breathValue;
            PlayerHUDController.Instance.breathSlider.value += breathValue;
        }
        else
        {
            breath = 100;
            PlayerHUDController.Instance.breathSlider.value += breathValue;

        }
    }

    IEnumerator decreaseBreathCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            decreaseBreath(1);
        }


    }

}
