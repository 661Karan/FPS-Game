using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Handler : MonoBehaviour
{
    public Menu[] Menus;

    public void OpenMenu(string menuName)
    {
        for(int i=0; i < Menus.Length; i++)
        {
            if (Menus[i].name == menuName)
            {
                Menus[i].open();
            }
            else
            {
                Menus[i].close();
            }
        }
    }
   public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickQuit()
    {
        Application.Quit(); 
    }
}
