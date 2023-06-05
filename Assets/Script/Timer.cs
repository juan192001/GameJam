using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float elapsedTime = 0f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        // Format the time in minutes and seconds
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = Mathf.Floor(elapsedTime % 60).ToString("00");

        // Update the UI Text component with the time
        timerText.text = minutes + ":" + seconds;
    }
}
