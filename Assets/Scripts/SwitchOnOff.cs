using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System;

//This is a simple On/Off switch though I would look at using Unity toggles and toggle groups for more complex switches
public class SwitchOnOff : MonoBehaviour
{
    [SerializeField] private Button _button;

    [SerializeField] private GameObject _onState;
    [SerializeField] private GameObject _offState;

    [Serializable]
     public class SwitchEvent : UnityEvent { }

    public SwitchEvent OnEvents;
    public SwitchEvent OffEvents;

    private bool _isOn = true;

    private void Start()
    {
        _onState.SetActive(_isOn);
        _offState.SetActive(!_isOn);

        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _isOn = !_isOn;
        _onState.SetActive(_isOn);
        _offState.SetActive(!_isOn);

        if(_isOn && OnEvents != null)
        {
            OnEvents.Invoke();
        }
        else if(!_isOn && OffEvents != null)
        {
            OffEvents.Invoke();
        }
    }

}
