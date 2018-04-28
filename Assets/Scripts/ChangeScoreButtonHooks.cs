using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScoreButtonHooks : MonoBehaviour {

    public void IncrementScore() {
        PlayerState.Instance.IncrementPlayerScore(1);
	}

    public void DecrementScore() {
        PlayerState.Instance.DecrementPlayerScore(1);
    }
}
