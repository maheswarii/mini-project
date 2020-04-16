using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject audioOnIcon;
    public GameObject audioOffIcon;
    public Text txtHighScore;

    // Start is called before the first frame update
    void Start()
    {
       SetSoundState();
       txtHighScore.text = PlayerPrefs.GetFloat("HighScore",0).ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        Application.LoadLevel("SampleScene");
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted",0) == 0)
            {
                PlayerPrefs.SetInt("Muted", 1);
            }
        else
            {
                PlayerPrefs.SetInt("Muted", 0);
            }

            SetSoundState();
    }

    public void SetSoundState()
    {
        if (PlayerPrefs.GetInt("Muted",0) == 0)
            {
                AudioListener.volume = 1;
                audioOnIcon.SetActive(true);
                audioOffIcon.SetActive(false);
            }
        else 
            {
                AudioListener.volume = 0;
                audioOnIcon.SetActive(false);
                audioOffIcon.SetActive(true);
            }
    }
}
