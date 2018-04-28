using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour {

    public string _sceneName = "GameScene";

    public void TriggerOpenScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
