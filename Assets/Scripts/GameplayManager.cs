using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private UIManager UI;
    private int size = 1;
    private int numToWin = 25;
    private float timer = 5f;

    void OnEnable()
    {
        Snowball.CollectEvent += CollectedItem;
    }

    void Update()
    {
        // Instatiate snow whirl
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            Vector3 newWhirl = player.Snowball.transform.position + (player.Snowball.LastMove * 10);
            GameObject.Instantiate((GameObject)Resources.Load(ResourcePath.SnowWhirl), newWhirl, Quaternion.identity);
            timer = Random.Range(30, 60);
        }
    }

    void CollectedItem()
    {
        size++;
        if (size == numToWin)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        UI.ShowWinScreen();
    }

    void OnDisable()
    {
        Snowball.CollectEvent -= CollectedItem;
    }
}
