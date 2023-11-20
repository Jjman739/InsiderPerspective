using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public int maxScore;

    private int score = ScoreTracker.Instance.treasureValue;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Final Score: " +  score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
