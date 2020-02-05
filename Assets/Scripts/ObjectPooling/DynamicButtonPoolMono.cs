using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicButtonPoolMono : MonoBehaviour {

    [SerializeField] private GameObject _prefab;
    [SerializeField] private RectTransform _container;

    private DynamicObjectPool<SpecialTestButton> _buttonPool;

    //important if you have muliple pools with the same parent;
    private static int _nextObjectCount = 1;
    private bool _showing = false;

    // Use this for initialization
    void Start () 
    {
        ShowButtons(true);
    }

    public void DespawnButtons()
    {
        //do this whenever you need, as this is an example I trigger it on enable so I can hide on disable
        ShowButtons(true);

    }

    public void SpawnButtons()
    {
        ShowButtons(false);
    }

    private void OnDestroy()
    {
        if(_buttonPool != null)
        {
            _buttonPool.DestroyAll();
        }
    }

    private void ShowButtons(bool show)
    {
        if(!_showing)
        {
            AddButtons(4);
            _showing = true;
        }
        else
        {
            if(_buttonPool != null)
            {
                _buttonPool.RemoveAll();
            }
            _showing = false;
        }

    }

    private void AddButtons(int count)
    {
        if(_buttonPool == null)
        {
            if (_prefab != null && _container != null)
            {
                _buttonPool = new DynamicObjectPool<SpecialTestButton>();
                _buttonPool.Init(_prefab, _container, OnButtonCreated);
            }
            else
            {
                LoggingUtil.LogDebugError("Dynamic Pools require a prefab and container rect.");
                return;
            }
        }

        /* If you are doing muiltiplechanges and the attached object has a layout group attached,
        as is the case with UI, it is important to disable the container. This prevents the layout 
        being recalculated for every object added. */
        _container.gameObject.SetActive(false);

        for (int i = 0; i < count; i++)
        {
            _buttonPool.AddItem(ref _nextObjectCount);
        }


        //reactivate after all items added;
        _container.gameObject.SetActive(true);
    }
	

    private void OnButtonCreated(SpecialTestButton createdButton)
    {
        /* This is a bit silly but it shows that you can do unique initialization such as binding data etc 
        when the item is created */
        createdButton.OnButtonCreated(_nextObjectCount);
    }
    
}
