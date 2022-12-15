using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() {
        instance = this;
    }
    public GameObject[] players;
    public GameObject[] playersPrefabs;
    public Sprite[] playersHeathbars;
    public GameObject[] maps;
    public GameObject[] spawnPos;
    public GameObject winnerPanel;
    public GameObject[] lifeBar;

    private void Start() {
        mapSpawn();
        playerSpawn();
    }


    public void mapSpawn()
    {
        Debug.Log(UImanager.mapIndex);
        GameObject tempPrefab =  Instantiate(maps[UImanager.mapIndex], new Vector3(2.5f, 0, 0), Quaternion.identity);
    }

    public void playerSpawn()
    {
        for(int i = 0; i < players.Length; i++)
        {
            playersPrefabs[i] = Instantiate(players[i], spawnPos[i].transform.position, Quaternion.identity);
        }
    }


  
    public void checkWinners()
    {

        foreach (GameObject player in playersPrefabs)
        {
            if(player.GetComponent<MovementController>().lifeCount == 0)
            {
                AudioManager.instance.Play("win");
                winnerPanel.SetActive(true);
                break;
            }
        }

    }

    public void updateLifeBar()
    {
        for(int i = 0; i < playersPrefabs.Length; i++)
        {
            lifeBar[i].GetComponent<Image>().sprite = playersHeathbars[playersPrefabs[i].GetComponent<MovementController>().lifeCount];
            Debug.Log(playersPrefabs[i].GetComponent<MovementController>().lifeCount);
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
        winnerPanel.SetActive(false);
    }
    

     public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        winnerPanel.SetActive(false);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }
    
    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void changeVolume()
    {
        AudioManager.instance.changeVolume();
    }
}
  
