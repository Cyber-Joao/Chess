using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturntoMenuFromtheCred : MonoBehaviour
{
    public void SceneChange()
    {
        // Load a Scene
        SceneManager.LoadScene("MainMenu");
    }
}
