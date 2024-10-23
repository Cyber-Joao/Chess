using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void SceneChange()
    {
        // Load a Scene
        SceneManager.LoadScene("Credits_Screen");
    }
}
