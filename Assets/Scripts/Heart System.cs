using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;
    public int points;
    public int pointsToWin = 3;
    public string nextStageName;
    [SerializeField]
    private AudioSource audioSource;
    public Text pointsText;

    void Start()
    {
        ResetGame();
    }


    // Subscribing to the OnUpdatePoints event from GameEvents when the GameObject is enabled.
    void OnEnable()
    {
        GameEvents.OnUpdatePoints += UpdatePointsDisplay;
    }
    // Unsubscribing from the OnUpdatePoints event when the GameObject is disabled.
    void OnDisable()
    {
        GameEvents.OnUpdatePoints -= UpdatePointsDisplay;
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < life);
        }

        if (life <= 0)
        {
            SceneManager.LoadScene("Dead menu");
        }

        if (points >= pointsToWin)
        {
            Debug.Log("Current points: " + points);
            LoadNextStage();
        }
    }

    public void TakeDamage(int d)
    {
        life -= d;
        life = Mathf.Max(life, 0);

        if (life <= 0)
        {
            // Actions before transitioning to the Dead menu (e.g., playing sound or animation)
           
        }
        audioSource.Play();
    }

    public void ResetHearts()
    {
        life = 3;
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < life);
        }
    }

    public void LoadNextStage()
    {
        if (!string.IsNullOrEmpty(nextStageName))
        {
            SceneManager.LoadScene(nextStageName);
        }
    }

    private void UpdatePointsDisplay(int newPoints)
    {
        points = newPoints;
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }

    public void ResetGame()
    {
        ResetHearts();
        points = 0;
        UpdatePointsDisplay(points);
    }

    // Method to report points changes to the GameEvents (notifying the event).
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        GameEvents.ReportPoints(points);
    }
}
