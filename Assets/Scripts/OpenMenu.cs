using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour {

    public GameObject _menu;
    public bool _visibleAtStart = false;

    private bool _isVisible;

	// Use this for initialization
	void Start () {
        _menu.SetActive(_visibleAtStart);
        _isVisible = _visibleAtStart;
	}
	
    public void TriggerOpenMenu()
    {
        if (!_isVisible)
        {
            _menu.SetActive(true);
            _isVisible = true;
        }
        else
        {
            _menu.SetActive(false);
            _isVisible = false;
        }

    }
}
