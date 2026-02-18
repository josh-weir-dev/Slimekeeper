using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Load()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        SceneManager.LoadScene("Loading Scene", LoadSceneMode.Additive);
    }

}
