using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour
{
    public void Load(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
