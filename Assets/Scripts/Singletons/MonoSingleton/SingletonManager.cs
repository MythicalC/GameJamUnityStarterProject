using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] monoBehaviourSingletons;
    [SerializeField] Transform _container;


    private List<GameObject> instantiatedSingletons;
    private static bool _created = false;

    // Use this for initialization
    void Awake()
    {
        if(!_created)
        {
            if (_container == null)
            {
                _container = this.transform;
            }

            DontDestroyOnLoad(_container.gameObject);

            for (int i = 0; i < monoBehaviourSingletons.Length; i++)
            {
                if (instantiatedSingletons == null)
                {
                    instantiatedSingletons = new List<GameObject>();
                }

                GameObject item = Instantiate(monoBehaviourSingletons[i]);
                item.transform.parent = _container;
                instantiatedSingletons.Add(item);
            }

            _created = true;
        }
    }
}
