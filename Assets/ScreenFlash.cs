using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image image;
    
    void update() {
        
    }

    public void setactive()
    {
        image.enabled = true;
    }
     public void deactive()
    {
        image.enabled = false;
    }
}
