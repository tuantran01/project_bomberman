using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    #region Singleton class: UI
    public static UImanager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion
    public GameObject optionPanels;
    public GameObject mapImage;
    public Slider slider;
    public Sprite[] mapImages;
    

    public static int mapIndex = 0;

    public void showOptionPanel()
    {
        optionPanels.SetActive(true);
    }

    public void hideOptionPanel()
    {
        optionPanels.SetActive(false);
    }

    public void showMap(int index)
    {
        for(int i = 0; i < mapImages.Length; i++)
        {
            if(i == index)
            {
                mapImage.GetComponent<Image>().sprite = mapImages[i];
            }
        }
    }

    public void leftBtnClicked(){
        if(mapIndex > 0){
            mapIndex--;
            showMap(mapIndex);
        }
    }

    public void rightBtnClicked(){
        if(mapIndex < mapImages.Length - 1){
            mapIndex++;
            showMap(mapIndex);
        }
    }


    public void startGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void sliderValue()
    {
        slider = AudioManager.instance.volumeSlider;
    }
}
