using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels: MonoBehaviour
{
    [SerializeField]
    private Image _soundImage;

    
    public void Evolution_1_Gameplay_Scene()
    {
        SceneManager.LoadScene(Constants.DATA.MENU_SCENE_1);
    }public void Evolution_2_Gameplay_Scene()
    {
        SceneManager.LoadScene(Constants.DATA.MENU_SCENE_2);
    }public void Evolution_3_Gameplay_Scene()
    {
        SceneManager.LoadScene(GameManager.Evolution_3_Menu);
    }

  

  
}