using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{

    public Slider healthSlider;
    public Slider breathSlider;
    public Image healthSliderFill;
    public Image breathSliderFill;
    Color32 green = new Color32(193, 255, 204, 255);
    Color32 blue = new Color32(193, 255, 249, 255);
    Color32 red = new Color32(255, 194, 193, 255);
    Color32 orange = new Color32(255, 245, 193, 255);
    //Slider breathSlider;


    void Awake()
    {
    }

    void Update()
    {
        healthSliderFill.color = Color.Lerp(red, green, healthSlider.value / 100);
        breathSliderFill.color = Color.Lerp(orange, blue, breathSlider.value / 100);
    }


}
