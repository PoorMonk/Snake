using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeHead : MonoBehaviour {

    public int step;
    public float speed = 0.35f;
    private int x;
    private int y;

    private List<Transform> m_list = new List<Transform>();
    public Sprite[] m_bodySprites = new Sprite[4];
    public GameObject m_bodyPrefab;
    private Transform m_parent;

    private bool m_isDie = false;
    public GameObject m_dieEffect;

    private bool m_isBlue;
    private bool m_isBorder;

    public AudioClip m_dieClip;
    public AudioClip m_eatClip;

    // Use this for initialization
    void Start () {
        m_parent = GameObject.Find("Canvas").gameObject.transform;
        InvokeRepeating("Move", 0, speed);

        m_isBlue = (PlayerPrefs.GetString("SnakeColor", "blue") == "blue") ? true : false;
        m_isBorder = (PlayerPrefs.GetInt("Border", 1) == 1) ? true : false; 
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W) && y != -step && UIController.Instance.m_isPause == false
            && !m_isDie)
        {
            x = 0;
            y = step;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A) && x != step && UIController.Instance.m_isPause == false
            && !m_isDie)
        {
            x = -step;
            y = 0;
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.S) && y != step && UIController.Instance.m_isPause == false
            && !m_isDie)
        {
            x = 0;
            y = -step;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.D) && x != -step && UIController.Instance.m_isPause == false
            && !m_isDie)
        {
            x = step;
            y = 0;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }

        if (Input.GetKeyDown(KeyCode.Space) && UIController.Instance.m_isPause == false
            && !m_isDie)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, speed - 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && UIController.Instance.m_isPause == false 
            && !m_isDie)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, speed);
        }
    }

    private void Move()
    {
        Vector3 pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x + x, pos.y + y, pos.z);
        
        if (m_list.Count > 0)
        {
            //m_list.Last().localPosition = pos;
            //m_list.Insert(0, m_list.Last());
            //m_list.RemoveAt(m_list.Count - 1);

            for (int i = m_list.Count - 1; i > 0; --i)
            {
                m_list[i].localPosition = m_list[i - 1].localPosition;
            }
            m_list[0].localPosition = pos;
        }
          
    }

    void Grow()
    {
        AudioSource.PlayClipAtPoint(m_eatClip, gameObject.transform.position);
        int iIndex = (m_list.Count % 2 == 0) ? 0 : 1;
        if (m_isBlue)
            iIndex += 2;
        GameObject gBody = Instantiate(m_bodyPrefab);
        gBody.transform.localPosition = new Vector3(2000, 2000, 0);
        gBody.GetComponent<Image>().sprite = m_bodySprites[iIndex];
        gBody.transform.SetParent(m_parent, false);
        m_list.Add(gBody.transform);
        
    }

    private void Die()
    {
        m_isDie = true;
        AudioSource.PlayClipAtPoint(m_dieClip, Vector3.zero);
        CancelInvoke();
        Instantiate(m_dieEffect);
        PlayerPrefs.SetInt("lastl", UIController.Instance.m_length);
        PlayerPrefs.SetInt("lasts", UIController.Instance.m_score);
        if (PlayerPrefs.GetInt("bests", 0) < UIController.Instance.m_score)
        {
            PlayerPrefs.SetInt("bestl", UIController.Instance.m_length);
            PlayerPrefs.SetInt("bests", UIController.Instance.m_score);
        }
        StartCoroutine(ReLoad(2f));
    }

    IEnumerator ReLoad(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Food"))
        {
            //Debug.Log(collision.gameObject.transform.localPosition);
            Destroy(collision.gameObject);
            UIController.Instance.EatFood(5);
            Grow();
            
            GenerateFood.Instance.RandomGenerateFood((Random.Range(0,100)) < 20 ? true : false);
        }
        else if (collision.tag.Equals("Body"))
        {
            Die();
        }
        else if (collision.tag.Equals("Reward"))
        {
            Destroy(collision.gameObject);
            UIController.Instance.EatFood(Random.Range(5,15) * 10);
            Grow();
        }
        else if (collision.tag.Equals("Border"))
        {
            if (m_isBorder)
            {
                Die();
                return;
            }
                
            //Debug.Log(collision.gameObject.name);
            switch(collision.gameObject.name)
            {
                case "Up":
                    transform.localPosition = new Vector3(transform.localPosition.x,
                        -transform.localPosition.y + step, transform.localPosition.z);
                    break;
                case "Bottom":
                    transform.localPosition = new Vector3(transform.localPosition.x,
                        -transform.localPosition.y - step, transform.localPosition.z);
                    break;
                case "Left":
                    transform.localPosition = new Vector3(-transform.localPosition.x + 210,
                        transform.localPosition.y, transform.localPosition.z);
                    break;
                case "Right":
                    transform.localPosition = new Vector3(-transform.localPosition.x + 270,
                        transform.localPosition.y, transform.localPosition.z);
                    break;
            }
        }
    }
}
