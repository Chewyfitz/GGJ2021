using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject timeText;
    
    float time = 0.0f;

    int score = 0;

    public void addScore(int score){
        if(score<0){
            this.score -= score;
            scoreText.GetComponent<Text>().text = this.score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.GetComponent<Text>().text = time.ToString("0.0");
    }
}
