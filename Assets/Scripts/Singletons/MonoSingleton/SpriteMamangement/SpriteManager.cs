using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviourSingleton<SpriteManager> {

    private Dictionary<string, Sprite> _spriteDictionary;

    public Sprite LoadSprite(string spriteName)
    {
        if(_spriteDictionary == null)
        {
            _spriteDictionary = new Dictionary<string, Sprite>();
        }

        if (_spriteDictionary.ContainsKey(spriteName))
        {
            LoggingUtil.LogInfo(string.Format("Loading sprite from Cache {0}", spriteName));
            return _spriteDictionary[spriteName];
        }
        else
        {
            Sprite loadedImage = Resources.Load<Sprite>(spriteName);
            if (loadedImage == null)
            {
                LoggingUtil.LogDebugError("There was an error loading your sprite. Please ensure that it is in the resources directory and the format of the image is sprite (2D & UI)");
                return null;
            }
            else
            {
                _spriteDictionary.Add(spriteName, loadedImage);
                return _spriteDictionary[spriteName];
            }
        }
    }

    public void UnloadSprite(string spriteName, bool removeFromDictionary = true)
    {
        if (_spriteDictionary == null)
        {
            return;
        }

        if (_spriteDictionary.ContainsKey(spriteName))
        {
            Sprite toRemove = _spriteDictionary[spriteName];

            if (removeFromDictionary)
            {
                _spriteDictionary.Remove(spriteName);
            }

            Resources.UnloadAsset(toRemove);
        }
    }


    void OnDestroy()
    {
        if (_spriteDictionary != null)
        {
            foreach (KeyValuePair<string, Sprite> kvp in _spriteDictionary)
            {
                //We need to make sure each asset is unloaded before we clear the dictionary
                UnloadSprite(kvp.Key, false);
            }

            _spriteDictionary.Clear();
        }
    }
}
