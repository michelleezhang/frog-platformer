using TMPro;
using UnityEngine;

/// <summary>
/// Displays the score in whatever text component is store in the same game object as this
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class ScoreKeeper : MonoBehaviour
{
    /// <summary>
    /// There will only ever be one ScoreKeeper object, so we just store it in
    /// a static field so we don't have to call FindObjectOfType(), which is expensive.
    /// </summary>
    public static ScoreKeeper Singleton;

    /// <summary>
    /// Add points to the score
    /// </summary>
    /// <param name="points">Number of points to add to the score; can be positive or negative</param>
    public static void ScorePoints(int points)
    {
        Singleton.ScorePointsInternal(points);
    }

    /// <summary>
    /// Current score
    /// </summary>
    public int Score;

    /// <summary>
    /// Text component for displaying the score
    /// </summary>
    private TMP_Text scoreDisplay;

    public int winScore = 9;

    // Assets associated with scoring
    public TMP_Text winText;
    public TMP_Text lossText;

    public AudioSource[] audioSources;

    /// <summary>
    /// Initialize Singleton and ScoreDisplay.
    /// </summary>
    void Start()
    {
        Singleton = this;
        scoreDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        Score = 0;
        scoreDisplay.text = "Score: 0";
        winText.gameObject.SetActive(false);
        lossText.gameObject.SetActive(false);
        audioSources = GetComponents<AudioSource>();
    }

    /// <summary>
    /// Internal, non-static, version of ScorePoints
    /// </summary>
    /// <param name="delta"></param>
    private void ScorePointsInternal(int delta)
    {
        audioSources[0].PlayOneShot(audioSources[0].clip);
        // Increments the score by the specified number of points
        // Then update the.text field of scoreDisplay.
        Score += delta;
        scoreDisplay.text = "Score: " + Score.ToString();
    }

    private void Lose()
    {
        // Called if player loses
        Time.timeScale = 0;
        lossText.gameObject.SetActive(true);
        audioSources[2].PlayOneShot(audioSources[2].clip);
    }

    public static void TriggerLose()
    {
        Singleton.Lose();
    }

    private void Win(GameObject cup)
    {
        if (Score == winScore)
        {
            // Called if player wins
            Time.timeScale = 0;
            winText.gameObject.SetActive(true);
            // Destroy cup
            Destroy(cup);
            audioSources[1].PlayOneShot(audioSources[1].clip);
        }
        
    }

    public static void TriggerWin(GameObject cup)
    {

        Singleton.Win(cup);
    }

}
