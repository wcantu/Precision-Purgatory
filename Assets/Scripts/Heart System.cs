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

    public Text pointsText;

    void Start()
    {
        ResetHearts();
        points = 0;
        UpdatePointsText();
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

    // Change the method to public so it can be accessed from other scripts.
    public void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }
}
