using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayMain()
    {
        SceneManager.LoadScene("Boat");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
