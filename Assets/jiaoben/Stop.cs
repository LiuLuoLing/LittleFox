using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Stop : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Setvolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}
