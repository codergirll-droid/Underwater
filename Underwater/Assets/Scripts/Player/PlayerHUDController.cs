using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{

    public static PlayerHUDController Instance;

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider breathSlider;
    public Image healthSliderFill;
    public Image breathSliderFill;
    Color32 green = new Color32(193, 255, 204, 255);
    Color32 blue = new Color32(193, 255, 249, 255);
    Color32 red = new Color32(255, 194, 193, 255);
    Color32 orange = new Color32(255, 245, 193, 255);

    [Header("Inventory")]
    public GameObject Inventory;


    bool isInventoryOpen = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    void Update()
    {

        healthSliderFill.color = Color.Lerp(red, green, healthSlider.value / 100);
        breathSliderFill.color = Color.Lerp(orange, blue, breathSlider.value / 100);

        openCloseInventory();
    }

    void openCloseInventory()
    {
        if(Input.GetKeyDown(KeyCode.I) && isInventoryOpen)
        {
            InventoryManager.Instance.EnableRemove.isOn = false;
            Inventory.SetActive(false);
            isInventoryOpen = false;

            //Cursor.visible = false;
            //Cursor.lockState=  CursorLockMode.Locked;

        }
        else if(Input.GetKeyDown(KeyCode.I) && !isInventoryOpen)
        {
            Inventory.SetActive(true);
            isInventoryOpen = true;

            InventoryManager.Instance.ListItems();

            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;

        }
    }

}
