using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectPool<T_PoolOf> where T_PoolOf : MonoBehaviour
{

    private GameObject _prefab;
    private RectTransform _container;
    private List<T_PoolOf> _objectPool;
    private int _itemCount = 0;

    public delegate void ObjectCreatedDelegate(T_PoolOf created);
    private ObjectCreatedDelegate OnObjectCreatedEvent;

    public void Init(GameObject prefab, RectTransform container, ObjectCreatedDelegate onObjectCreated = null)
    {
        _prefab = prefab;
        _container = container;
        if (onObjectCreated != null)
        {
            OnObjectCreatedEvent += onObjectCreated;
        }
    }

    //Getting the object at index so you can use pool[i] to set and get
    public T_PoolOf this[int key]
    {
        get
        {
            return GetValue(key);
        }
        set
        {
            SetValue(key, value);
        }
    }

    private void SetValue(int key, T_PoolOf value)
    {
        throw new NotImplementedException();
    }

    private T_PoolOf GetValue(int key)
    {
        throw new NotImplementedException();
    }

    public List<T_PoolOf> GetPooledObjects(bool activeOnly = true)
    {
        if(_objectPool != null)
        {
            if (activeOnly)
            {
                List<T_PoolOf> activeItems = null;
                for (int i = 0; i < _objectPool.Count; i++)
                {
                    if(_objectPool[i].gameObject.activeInHierarchy)
                    {
                        if(activeItems == null)
                        {
                            activeItems = new List<T_PoolOf>();
                        }

                        activeItems.Add(_objectPool[i]);
                    }
                }

                return activeItems;
            }
            else
            {
                return _objectPool;
            }

        }

        return null;
    }

    public void AddItem(ref int nextChildCount)
    {
        if(_objectPool == null)
        {
            _objectPool = new List<T_PoolOf>();
        }

        if(_itemCount >= _objectPool.Count || _objectPool[_itemCount] == null)
        {
            GameObject item = UnityEngine.Object.Instantiate(_prefab);
            item.transform.SetParent(_container, false);
            item.transform.localScale = Vector3.one;
            item.transform.SetSiblingIndex(nextChildCount);
            item.name = string.Format("item_{0}_{1}", nextChildCount, (typeof(T_PoolOf)).ToString());
            T_PoolOf pooledObject = item.GetComponent<T_PoolOf>();
            _objectPool.Add(pooledObject);
            _itemCount++;
            nextChildCount++;

            if(OnObjectCreatedEvent != null)
            {
                OnObjectCreatedEvent(pooledObject);
            }
        }
        else
        {
            T_PoolOf item = _objectPool[_itemCount];
            item.transform.SetSiblingIndex(nextChildCount);
            item.name = string.Format("item_{0}_{1}", nextChildCount, (typeof(T_PoolOf)).ToString());
            _itemCount++;
            nextChildCount++;

            item.gameObject.SetActive(true);

            if (OnObjectCreatedEvent != null)
            {
                OnObjectCreatedEvent(item);
            }
        }
    }

    public void RemoveAll()
    {
        if (_objectPool != null)
        {
            int destroyItemsCount = _objectPool.Count;
            for (int i = _objectPool.Count - 1; i >= 0; i--)
            {
                RemoveItem(i, ref destroyItemsCount);
            }
        }
    }

    public void RemoveItem(int index, ref int nextChildCount)
    {
        if(_objectPool == null)
        {
            return;
        }

        if(index >= _objectPool.Count || _objectPool[index] == null)
        {
            return;
        }

        if(_objectPool[index].isActiveAndEnabled)
        {
            T_PoolOf item = _objectPool[index];
            item.gameObject.SetActive(false);

            _itemCount--;
            nextChildCount--;
        }

    }

    public void DestroyAll()
    {
        if(_objectPool != null)
        {
            int destroyItemsCount = _objectPool.Count;
            for (int i = _objectPool.Count - 1; i >= 0; i--)
            {
                DestroyItem(i, ref destroyItemsCount);
            }
        }
    }

    public void DestroyItem(int index, ref int nextChildCount)
    {
        if (_objectPool == null)
        {
            return;
        }

        if (index >= _objectPool.Count)
        {
            return;
        }

        if (_objectPool[index] != null)
        {
            T_PoolOf item = _objectPool[index];
            _objectPool.RemoveAt(index);
            item.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(item);

        }
        else
        {
            _objectPool.RemoveAt(index);
        }

        _itemCount--;
        nextChildCount--;
    }


}
