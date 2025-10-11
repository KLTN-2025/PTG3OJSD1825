using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EndingScene : MonoBehaviour
{
    public Text subtitleText;
    public Button resetButton;
    private List<string> subtitles = new List<string>();

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        subtitles.Add("Xin Chào...");
        subtitles.Add("'Ngươi là ai vậy?'");
        subtitles.Add("Ta là kẻ đã dõi theo mọi khổ đau trong cuộc đời ngươi.");
        subtitles.Add("Cả thân thể lẫn tâm hồn ngươi đều đã tổn thương... chắc hẳn mệt mỏi lắm.");
        subtitles.Add("Ngươi nghĩ... cuộc sống là gì?");
        subtitles.Add("...");
        subtitles.Add("Nếu ta cho ngươi cơ hội bắt đầu lại một lần nữa, ngươi có muốn không?");
        subtitles.Add("Ta rất mong chờ xem ngươi sẽ đưa ra lựa chọn nào.");
        subtitles.Add("'Ngươi có muốn 'tái sinh' để bắt đầu cuộc đời mới không?'");
        resetButton.gameObject.SetActive(false);
        StartCoroutine(ShowSubtitles());
    }

    IEnumerator ShowSubtitles()
    {
        foreach (string subtitle in subtitles)
        {
            yield return new WaitForSeconds(3f);
            subtitleText.text = subtitle;
            
            if (subtitle == "'Ngươi có muốn 'tái sinh' để bắt đầu cuộc đời mới không?'")
            {
                resetButton.gameObject.SetActive(true);
                Cursor.visible = true;
            }
        }
    }

    public void OnClickResetButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
