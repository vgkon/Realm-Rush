using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject escPanel;
    bool playing = true;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escPanel.activeSelf)
            {
                escPanel.SetActive(true);
                Time.timeScale = 0;
                playing = false;
            }
            else
            {
                escPanel.SetActive(false);
                Time.timeScale = 1;
                playing = true;
            }
        }
    }

    public bool IsPlaying()
    {
        return playing;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }
}
