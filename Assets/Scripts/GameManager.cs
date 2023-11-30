using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float menuCameraSize = 5f;
    [SerializeField] private Vector3 menuCameraPosition = new Vector3(0, 0, -10);
    [SerializeField] private float gameCameraSize = 5f;
    [SerializeField] private Vector3 gameCameraPosition = new Vector3(0, 0, -10);




    private void Awake()
    {
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
            if (scene.name == "Main_menu")
            {
                mainCamera.orthographicSize = menuCameraSize;
                mainCamera.transform.position = menuCameraPosition;
            }
            else if (scene.name == "Tutorial")
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
