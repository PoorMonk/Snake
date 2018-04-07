using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    private static UIController instance;
    public static UIController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    public Image m_pauseImage;
    public Sprite[] m_pausePlaySprite;
    public Text m_msgText;
    public Text m_scoreText;
    public Text m_lengthText;
    public Image m_bgImage;
    public int m_score = 0;
    public int m_length = 0;

    public GameObject m_head;
    public Sprite m_blueHead;
    public Sprite m_yellowHead;

    [HideInInspector]
    public bool m_isPause = false;

    public void EatFood(int score = 5, int length = 1)
    {
        m_score += score;
        m_length += length;
        UpdateUI();
    }

    public void UpdateUI()
    {
        m_scoreText.text = "得 分:\n" + m_score;
        m_lengthText.text = "长 度:\n" + m_length;
        switch (m_score / 100)
        {
            case 3:
                m_msgText.text = "阶 段 2";
                m_bgImage.color = Color.red;
                break;
            case 5:
                m_msgText.text = "阶 段 3";
                m_bgImage.color = Color.blue;
                break;
            case 7:
                m_msgText.text = "阶 段 4";
                m_bgImage.color = Color.yellow;
                break;
            case 9:
                m_msgText.text = "阶 段 5";
                m_bgImage.color = Color.gray;
                break;
            case 11:
                m_msgText.text = "无 尽 模 式";
                m_bgImage.color = Color.green;
                break;
        }
    }

    public void Pause()
    {
        m_isPause = !m_isPause;
        if (m_isPause)
        {
            Time.timeScale = 0;
            m_pauseImage.sprite = m_pausePlaySprite[0];
        }
        else
        {
            Time.timeScale = 1;
            m_pauseImage.sprite = m_pausePlaySprite[1];
        }
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString("SnakeColor", "blue") == "blue")
        {
            m_head.GetComponent<Image>().sprite = m_blueHead;
        }
        else
        {
            m_head.GetComponent<Image>().sprite = m_yellowHead;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
