using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadFirstLevel : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeTime = 0.2f;

    public void OnEnable()
    {
        FadeScreen.FadeIn(fadeScreen,fadeTime);
        WaitForFade(fadeTime);
    }

    public void Start()
    {
        FadeScreen.FadeIn(fadeScreen,fadeTime);
        WaitForFade(fadeTime);
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            FadeScreen.FadeOut(fadeScreen, fadeTime);
            StartCoroutine(WaitForFade(fadeTime));
        }
    }

    private IEnumerator WaitForFade(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(1);
    }

}
