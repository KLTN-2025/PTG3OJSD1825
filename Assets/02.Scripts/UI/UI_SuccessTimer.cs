using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_SuccessTimer : MonoBehaviour
{
    public float elapsedTime = 0f;
    private bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerRunning = false;
            PlayerPrefs.SetFloat("LevelTime", elapsedTime);
            SceneManager.LoadScene("EndingScene");
        }
    }
}
