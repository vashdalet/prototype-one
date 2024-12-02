using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject player;
     private GameObject start;
    private Vector3 playerSpawnPos;
    private string currentScene = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        start = GameObject.FindGameObjectWithTag("Respawn");
        playerSpawnPos = start.transform.position;
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentScene) {
            currentScene = scene.name;
            start = GameObject.FindGameObjectWithTag("Respawn");
            playerSpawnPos = start.transform.position;
        }

        Instantiate(player, playerSpawnPos, transform.rotation); 


    }
    public void SetPlayerSpawnPos(Vector3 playerSpawnPos)
    {
        this.playerSpawnPos = playerSpawnPos;
    }

}

