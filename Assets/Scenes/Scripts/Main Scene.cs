using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainScene : MonoBehaviour
{
    public EventSystem eventSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (!SceneManager.GetSceneByName("Loading Scene").isLoaded)
        {
            eventSystem.enabled = true;
        }
    }
}

    


