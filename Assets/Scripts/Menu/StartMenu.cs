using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    public AudioMixer anAudioMixer;

    public void OnPlayButton()
    {
        // Load the first scene, which is the maze
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float aVolume)
    {
        anAudioMixer.SetFloat("MainMixerVolume", aVolume);
    }


}
