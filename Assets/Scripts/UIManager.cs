using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_WinScreen;

    [SerializeField]
    private Text m_SizeNum;

    void OnEnable()
    {
        Snowball.GrowSize += SetSnowballSize;
    }

    public void SetSnowballSize(int size)
    {
        m_SizeNum.text = size.ToString();
    }

    public void ShowWinScreen()
    {
        m_WinScreen.SetActive(true);
    }

    public void Continue()
    {
        m_WinScreen.SetActive(false);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneIndex.Gameplay);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnDisable()
    {
        Snowball.GrowSize -= SetSnowballSize;
    }
}
