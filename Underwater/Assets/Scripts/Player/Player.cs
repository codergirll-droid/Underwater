using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int breath = 100;

    public static Player Instance;


    bool decreaseB;

    public float breathDecreaseTime;
    public int breathAmountToDecrease;

    public GameManager manager;

    public int activeSceneIndex = 0;

    private void Start()
    {

        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

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

        decreaseBreathByDefault(breathAmountToDecrease);

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

    public void decreaseBreathByDefault(int breathValue)
    {
        if (!decreaseB)
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


            decreaseB = true;
            Invoke(nameof(resetBreathDecrease), breathDecreaseTime);
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

    void resetBreathDecrease()
    {
        decreaseB = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("portal"))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            activeSceneIndex = sceneIndex + 1;
            GameManager.Instance.saveGame();
            
            manager.LoadLevel(activeSceneIndex);


        }
    }


}
