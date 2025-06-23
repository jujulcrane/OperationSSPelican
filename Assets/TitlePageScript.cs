using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitlePageScript : MonoBehaviour
{
    
    public void play()
    {
        StateNameController.hard = false;
        SceneManager.LoadScene("BattleScene");
    }

    public void hard()
    {
        StateNameController.hard = true;
        SceneManager.LoadScene("BattleLevel2");
    }

    public void quit()
    {
        Application.Quit();
    }

}
