using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField] Texture2D _arrow;

    private void Start()
    {
        Cursor.SetCursor(_arrow, Vector2.zero, CursorMode.ForceSoftware);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
