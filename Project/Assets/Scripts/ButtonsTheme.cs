using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsTheme : MonoBehaviour
{
    public ThemeData themeData;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;

    List<Button> buttons = new List<Button>();
    
    void Start()
    {
        buttons.Add(button1); 
        buttons.Add(button2); 
        buttons.Add(button3); 
        buttons.Add(button4); 
        buttons.Add(button5); 
        buttons.Add(button6); 
        buttons.Add(button7);
    }

    
    void Update()
    {
        if (themeData.Theme == 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i])
                {
                    buttons[i].image.color = Color.black;
                }
            }
        }
        if (themeData.Theme == 1)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i])
                {
                    buttons[i].image.color = Color.white;
                }
            }
        }
    }

    public void SwithThemes()
    {
        switch (themeData.Theme)
        {
            case 0:
                themeData.Theme = 1;
                break;
            case 1:
                themeData.Theme = 0;
                break;
        }
    }
}
