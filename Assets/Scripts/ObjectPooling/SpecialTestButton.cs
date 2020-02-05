using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SpecialTestButton : Button
{
    private TextMeshProUGUI _text;
    private int _index;

    protected override void Start()
    {
        //This is an inefficent function where you can assign via the inspector if not call it only once in OnStart or Awake
        //true tells it to check the component evem when disabled
        _text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>(true); 

        base.Start();
    }

    public void OnButtonCreated(int index)
    {
        _index = index;

        if(_text == null)
        {
            _text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        _text.SetText(string.Format("SpecialTestButton: {0}", _index));
    }

    public void OnButtonClick()
    {
        LoggingUtil.LogInfo(string.Format("The {0} button was clicked", _index), "magenta");
    }

}
