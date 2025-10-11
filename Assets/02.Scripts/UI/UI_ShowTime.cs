using UnityEngine;
using UnityEngine.UI;

public class UI_ShowTime : MonoBehaviour
{
    public Text timeText;  

    void Start()
    {
        float levelTime = PlayerPrefs.GetFloat("LevelTime", 0);
        int levelTimeMinutes = Mathf.FloorToInt(levelTime / 60);
        timeText.text = "Time: " + levelTimeMinutes.ToString() + " minutes";
    }
}
