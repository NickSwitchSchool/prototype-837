using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void ChangeScene(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
