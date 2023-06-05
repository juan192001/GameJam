using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameJam");
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }

    // Add any other menu functionality you need
}
