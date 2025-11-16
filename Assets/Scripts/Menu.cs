using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayMain()
    {
        SceneManager.LoadScene("Boat");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
