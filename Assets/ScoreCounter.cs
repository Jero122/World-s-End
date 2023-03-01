using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int score;
    
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = "0";
        score = 0;
    }

    public void incrementScore()
    {
        score += 1;
        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
