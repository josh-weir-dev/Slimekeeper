using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class Loading : MonoBehaviour
{
    public LevelGeneration level;
    // Start is called before the first frame update
    void Start()
    {
       GameObject Generator = GameObject.FindWithTag("Generation");
       level = Generator.GetComponent<LevelGeneration>();
       
    }
    // creating separate game objects for the two pieces of text
    public TextMeshProUGUI Ltext;
    public TextMeshProUGUI Ctext;
    private void Update()
    {
        if (level.stopgeneration)
        {
            // Setting the loading text to disabled and the continue text to enabled
            Ltext.gameObject.SetActive(false);
            Ctext.gameObject.SetActive(true);
            
        }
    }
    public void load()
    {
        // if the continue text is active then the player can continue
        if (Ctext.gameObject.activeInHierarchy)
        {
            SceneManager.UnloadSceneAsync("Loading Scene");
            
        }
    }
}
