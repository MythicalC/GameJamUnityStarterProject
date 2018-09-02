using UnityEngine;
using System.Collections;

public class DoNotDestroryOnLevelChange : MonoBehaviour
{
    private bool _created = false;
    void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }
}
