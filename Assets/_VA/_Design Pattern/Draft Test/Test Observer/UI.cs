//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using VA.DesignPattern;

//public class UI : MonoBehaviour
//{
//    public static readonly string SCORE_UPDATE = "Score Update";
//    public static readonly string SCORE_RESET = "Score Reset";
//    public TextMeshProUGUI scoreText;
//    public int score = 0;
//    private void OnEnable()
//    {
//        Observer.AddObserver<int>(SCORE_UPDATE, ScoreUpdate);
//        Observer.AddObserver(SCORE_UPDATE, ChangeText);

//        Observer.AddObserver(SCORE_RESET, ResetScore);
//        Observer.AddObserver(SCORE_RESET, ChangeText);
//    }
//    private void OnDisable()
//    {
//        Observer.RemoveObserver<int>(SCORE_UPDATE, ScoreUpdate);
//        Observer.RemoveObserver(SCORE_UPDATE, ChangeText);

//        Observer.RemoveObserver(SCORE_RESET, ResetScore);
//        Observer.RemoveObserver(SCORE_RESET, ChangeText);
//    }
//    private void ScoreUpdate(int val)
//    {
//        score = score + val;       
//    }
//    private void ChangeText()
//    {
//        scoreText.text = $"Score: {score}";
//    }    
//    private void ResetScore()
//    {
//        score = 0;
//    }    
//    private void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.A))
//        {
//            Observer.Notify(SCORE_UPDATE, 1);
//        }
//        else if(Input.GetKeyDown(KeyCode.S))
//        {
//            Observer.Notify(SCORE_UPDATE, 2);
//        }
//        else if (Input.GetKeyDown(KeyCode.D))
//        {
//            Observer.Notify(SCORE_UPDATE, 3);
//        }
//        else if (Input.GetKeyDown(KeyCode.F))
//        {
//            Observer.Notify(SCORE_UPDATE, 4);
//        }
//        else if (Input.GetKeyDown(KeyCode.X))
//        {
//            Observer.Notify(SCORE_RESET);
//        }
//    }
//}
