using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{


    [Header("Sliders")]
    public Slider healthSlider;
    public Image healthSliderFill;
    Color32 green = new Color32(193, 255, 204, 255);
    Color32 red = new Color32(255, 194, 193, 255);




    private void Awake()
    {


    }


    void Update()
    {

        healthSliderFill.color = Color.Lerp(red, green, healthSlider.value / 100);

    }

   

}
