using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI currentScore;

    public int score = 0;
    public float height;
    private Rigidbody2D rb;

    private float timeElapsed = 0f;
    public float speed;

    private string mainMenu = "MainMenu";
       

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        score = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        getScore();
        timeElapsed += Time.deltaTime;
        speed = transform.position.y / timeElapsed;
        //Debug.Log(transform.position.y / timeElapsed);
    }

    void getScore()
    {
        height = transform.position.y;
        if((int)height > score)
        {
            score = (int)height;   
            currentScore.text = score.ToString();
        }
             

    } 

    public void Die()
    {
        SceneManager.LoadScene(mainMenu);        
    }

}
