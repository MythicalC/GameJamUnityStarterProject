using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUISpriteFromResources : MonoBehaviour {

    public Image _UIImageToAssignSpriteTo;
    public string _spriteName = "ExampleBackground";

    private Sprite _loadedImage;

	// Use this for initialization
	void Start () {
        //This loads the sprite on object initialisation only here for example purposes :)
        LoadSprite();
	}
	
    public void LoadSprite()
    {
        _loadedImage = SpriteManager.Instance.LoadSprite(_spriteName);

        if (_loadedImage == null)
        {
            LoggingUtil.LogError("There was an error loading your sprite. Please ensure that it is in the resources directory and the format of the image is sprite (2D & UI)");
        }
        else
        {
            _UIImageToAssignSpriteTo.sprite = _loadedImage;
        }

    }

    void OnDestroy() {
        //SpriteManager.Instance.UnloadSprite(_spriteName);
    }
}
