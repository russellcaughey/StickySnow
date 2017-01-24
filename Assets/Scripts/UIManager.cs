using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Minimap m_Minimap;

    [SerializeField]
    private GameObject m_WinScreen;

    [SerializeField]
    private Text m_SizeNum;

    private Snowball Snowball;

    void OnEnable()
    {
        Snowball.CollectEvent += SetSnowballSize;
    }

    void Start()
    {
        Snowball = GameObject.FindObjectOfType<Snowball>();
        m_Minimap.SetSnowballTarget(Snowball);
    }

    public void SetSnowballSize()
    {
        m_SizeNum.text = GameObject.FindObjectOfType<Snowball>().SnowballSize.ToString();
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
        Snowball.CollectEvent -= SetSnowballSize;
    }
}
