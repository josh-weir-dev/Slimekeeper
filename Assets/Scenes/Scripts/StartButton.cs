using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void start()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        SceneManager.LoadScene("Loading Scene", LoadSceneMode.Additive);
    }
}
