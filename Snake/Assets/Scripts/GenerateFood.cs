using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class GenerateFood : MonoBehaviour {

    static private GenerateFood m_instance;
    static public GenerateFood Instance
    {
        get           
        {
            return m_instance;
        }
    }

    private int xLeft = -12;
    private int xRight = 22;
    private int UpBottom = 13;
    public Sprite[] m_sprites;
    public GameObject m_food;
    public Transform m_parent;

    public GameObject m_rewardPrefab;

    private void Awake()
    {
        m_instance = this;
    }

    // Use this for initialization
    void Start () {
        RandomGenerateFood(false);
        //Instantiate(test, new Vector3(200, 200, 0), Quaternion.identity);
		//for(int i = 0; i < 100; ++i)
  //      {
  //          RandomGenerateFood(false);           
  //      }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RandomGenerateFood(bool IsReward)
    {
        int x = Random.Range(xLeft, xRight);
        int y = Random.Range(-UpBottom + 1, UpBottom);
        GameObject food = Instantiate(m_food);
        food.transform.SetParent(m_parent, false);
        int iIndex = Random.Range(0, m_sprites.Length);
        food.GetComponent<Image>().sprite = m_sprites[iIndex];
        food.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        //Debug.Log("x: " + x + ", y: " + y);
        if (IsReward)
        {
            x = Random.Range(xLeft, xRight);
            y = Random.Range(-UpBottom + 1, UpBottom);
            food = Instantiate(m_rewardPrefab);
            food.transform.SetParent(m_parent, false);        
            food.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }
    }
}
