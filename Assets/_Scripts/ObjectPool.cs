using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; set; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }
    private List<T> pool;

    public ObjectPool(T prefab, int count, Transform container, bool Expandable = true)
    {
        Prefab = prefab;
        Container = container;
        AutoExpand = Expandable;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.Prefab, this.Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var poolObject in pool)
        {
            if (!poolObject.gameObject.activeInHierarchy)
            {
                element = poolObject;
                poolObject.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;
        if (AutoExpand)
            return CreateObject(true);

        throw new Exception($"No Free Elements of type {typeof(T)}");
    }

    public void DeactivatePool()
    {
        foreach (var element in pool)
            if (element.gameObject.activeInHierarchy)
                element.gameObject.SetActive(false);
    }
}