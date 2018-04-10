using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour {

    public Text m_lastText;
    public Text m_bestText;
    public Toggle m_blue;
    public Toggle m_yellow;
    public Toggle m_border;
    public Toggle m_noBorder;

    private void Start()
    {
        m_lastText.text = "last:长度:" + PlayerPrefs.GetInt("lastl", 0) + ",得分:" + 
            PlayerPrefs.GetInt("lasts", 0);
        m_bestText.text = "best:长度:" + PlayerPrefs.GetInt("bestl", 0) + ",得分:" +
            PlayerPrefs.GetInt("bests", 0);

        if (PlayerPrefs.GetString("SnakeHead", "sh01") == "sh01")
        {
            m_blue.isOn = true;
            //PlayerPrefs.SetString("SnakeColor", "blue");
            SelectedBlue();
        }
        else
        {
            m_yellow.isOn = true;
            //PlayerPrefs.SetString("SnakeColor", "yellow");
            SelectedYellow();
        }

        if (PlayerPrefs.GetInt("Border", 1) == 1)
        {
            m_border.isOn = true;
            PlayerPrefs.SetInt("Border", 1);
        }
        else
        {
            m_noBorder.isOn = true;
            PlayerPrefs.SetInt("Border", 0);
        }
    }

    public void SelectedBlue()
    {
        PlayerPrefs.SetString("SnakeHead", "sh01");
        PlayerPrefs.SetString("SnakeBody1", "sb0101");
        PlayerPrefs.SetString("SnakeBody2", "sb0102");
    }

    public void SelectedYellow()
    {
        PlayerPrefs.SetString("SnakeHead", "sh02");
        PlayerPrefs.SetString("SnakeBody1", "sb0201");
        PlayerPrefs.SetString("SnakeBody2", "sb0202");
    }

    public void SelectedBorder()
    {
        PlayerPrefs.SetInt("Border", 1);
    }

    public void SelectedNoBorder()
    {
        PlayerPrefs.SetInt("Border", 0);
    }

    public void StartButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        Debug.Log("Start Game...");
    }
}
