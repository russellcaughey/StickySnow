using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsLoader : MonoBehaviour {

    float timeToLoadScene = 8f;

	void Update () {
        if (timeToLoadScene > 0)
            timeToLoadScene -= Time.deltaTime;
        else
            SceneManager.LoadScene(2);
	}
}
