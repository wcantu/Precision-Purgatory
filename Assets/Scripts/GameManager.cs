using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    [SerializeField] private float menuCameraSize = 5f; // Set this to your menu's camera size
    [SerializeField] private Vector3 menuCameraPosition = new Vector3(0, 0, -10); // Set this to your menu's camera position
    [SerializeField] private float gameCameraSize = 5f; // Set this to your game's camera size
    [SerializeField] private Vector3 gameCameraPosition = new Vector3(0, 0, -10); // Set this to your game's camera position

    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Check the name of the scene and adjust the camera accordingly
            if (scene.name == "Main_menu") // Replace with your actual menu scene name
            {
                mainCamera.orthographicSize = menuCameraSize;
                mainCamera.transform.position = menuCameraPosition;
            }
            else if (scene.name == "Tutorial") // Replace with your actual game scene name
            {
                mainCamera.orthographicSize = gameCameraSize;
                mainCamera.transform.position = gameCameraPosition;
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}