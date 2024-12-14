using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void loadSurface(){
        SceneManager.LoadScene(1);
    }

    public void loadSimpleCollision(){
        SceneManager.LoadScene(2);
    }

    public void loadObject(){
        SceneManager.LoadScene(3);
    }

    public void loadRoom(){
        SceneManager.LoadScene(4);
    }

    public void loadSensors(){
        SceneManager.LoadScene(5);
    }

    public void loadTextures(){
        SceneManager.LoadScene(6);
    }

    public void loadRoomScan(){
        SceneManager.LoadScene(7);
    }

    public void loadImage(){
        SceneManager.LoadScene(8);
    }

    public void loadLight(){
        SceneManager.LoadScene(8);
    }

}
