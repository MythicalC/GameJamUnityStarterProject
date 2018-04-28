
using UnityEngine;
using UnityEngine.UI;

public class ScoreListener : MonoBehaviour {

    public Text _scoreCounter;

	// Use this for initialization
	void Start () {
        PlayerState.Instance.OnScoreUpdate += OnScoreChanged;
	}

    private void OnScoreChanged (int newScore) {
        _scoreCounter.text = string.Format("{0}", newScore);
	}

    void OnDestroy() {
        PlayerState.Instance.OnScoreUpdate -= OnScoreChanged;
    }
}
