using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    private static bool _exists = false;

    public GameObject player;
    
    public GameController()
    {
        if (!_exists)
        {
            _instance = this;
        }
    }
    private void Awake()
    {
        if (_exists)
        {
            Destroy(this);
        }
        else
        {
            _exists = true;
            DontDestroyOnLoad(this);
        }
    }


    public void ChangeScene(string sceneName, Vector2 playerPosition)
    {
        SceneManager.LoadScene(sceneName);
        player.transform.position = new Vector3(playerPosition.x,playerPosition.y,0);
        
    }
    
}
