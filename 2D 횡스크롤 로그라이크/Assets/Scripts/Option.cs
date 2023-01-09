using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Option : MonoBehaviour
{
    public Text message;
    public Text result;
    public Slider slider;
    public Button button;

    private int min = 0;
    private int max = 100;


    void Start()
    {
        SetFunction_UI();
    }

    private void SetFunction_UI()
    {
        //Reset
        ResetFunction_UI();

        button.onClick.AddListener(Function_Button);
        slider.onValueChanged.AddListener(Function_Slider);

    }

    private void Function_Button()
    {
        result.text = slider.value.ToString();
        Debug.LogError("Slider Result!\n" + slider.value);
    }
    private void Function_Slider(float _value)
    {
        message.text = _value.ToString();
        Debug.Log("Slider Dragging!\n" + _value);
    }

    private void ResetFunction_UI()
    {
        button.onClick.RemoveAllListeners();
        slider.onValueChanged.RemoveAllListeners();
        slider.maxValue = max;
        slider.minValue = min;
        slider.wholeNumbers = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExit()
    {
        Debug.Log("¿É¼Ç");
        SceneManager.LoadScene("MainMenuScene");
    }
}
