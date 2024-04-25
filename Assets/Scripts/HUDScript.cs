using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HUD
/// </summary>
public class HUDScript : MonoBehaviour
{
    // scoring support
    [SerializeField]
    Text scoreText;
    public int score;
    //public static int points = 1;
    const string ScorePrefix = "score: ";

    private void Awake()
    {
        //FishCollectorEvents.fishTake.AddListener(AddPoints);
        FishCollectorEvents.givePoints.AddListener(AddPoints);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        scoreText.text = ScorePrefix + score.ToString();
	}
	
    /// <summary>
    /// Adds the given points to the score
    /// </summary>
    /// <param name="points">points</param>
    public void AddPoints(int point)
    {
        //score += points;
        score += point;
        scoreText.text = ScorePrefix + score.ToString();
    }
}
