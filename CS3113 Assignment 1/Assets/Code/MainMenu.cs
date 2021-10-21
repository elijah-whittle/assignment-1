using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // credits?
    public GameObject quitBtn;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        quitBtn.SetActive(false);
#endif
    }

    public void Play()
    {
        SceneManager.LoadScene("intro");
    }

    // public void Credits()

    public void Quit()
    {
        Application.Quit();
    }

}
