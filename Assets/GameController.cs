using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;


    public GameController()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
}
