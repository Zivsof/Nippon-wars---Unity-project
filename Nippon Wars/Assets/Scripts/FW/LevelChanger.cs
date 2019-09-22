using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public string LevelToLoad;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void FadeToLevel(string levelName)
    {
        LevelToLoad = levelName;
        if (SceneManager.GetActiveScene().name != "Testing 2")
        {
            FWmanager.Instance.GetPlayerPosition();
        }

        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}