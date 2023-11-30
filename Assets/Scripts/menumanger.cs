using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private float menuCameraSize = 5f; // Set this to your menu's camera size
    [SerializeField] private Vector3 menuCameraPosition = new Vector3(0, 0, -10); // Set this to your menu's camera position
    [SerializeField] private float gameCameraSize = 5f; // Set this to your game's camera size
    [SerializeField] private Vector3 gameCameraPosition = new Vector3(0, 0, -10); // Set this to your game's camera position

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AdjustCamera(scene.name);
    }

    private void AdjustCamera(string sceneName)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        if (sceneName == "Main_menu")
        {
            mainCamera.orthographicSize = menuCameraSize;
            mainCamera.transform.position = menuCameraPosition;
        }
        else if (sceneName == "Tutorial")
        {
            mainCamera.orthographicSize = gameCameraSize;
            mainCamera.transform.position = gameCameraPosition;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Tutorial"); // Load the game scene

   
        HeartSystem heartSystem = FindObjectOfType<HeartSystem>();
        if (heartSystem != null)
        {
            heartSystem.ResetHearts();
        }
    }




}
