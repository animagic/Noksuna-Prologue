using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMonoBehaviour : MonoBehaviour
{
    //Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component>();

    public void Invoke(Action task, float _time)
    {
        Invoke(task.Method.Name, _time);
    }
    public void CancelInvoke(Action task)
    {
        CancelInvoke(task.Method.Name);
    }

    public Color ColorWithAlpha(Color _color, float _alpha)
    {
        Color newColor = new Color();

        newColor.r = _color.r;
        newColor.g = _color.g;
        newColor.b = _color.b;
        newColor.a = _alpha;

        return newColor;
    }

    public T WithComponent<T>(Action<T> action) where T : Component
    {
        T _component = transform.WithComponent<T>(action);

        if (_component)
            action(_component);

        return _component;
    }

    public T WithComponentInParent<T> (Action<T> action) where T : Component
    {
        T _component = transform.WithComponentInParent<T>(action);

        if (_component)
            action(_component);

        return _component;
    }

    public T[] WithComponentsInParent<T> (Action<T> action) where T : Component
    {
        
        T[] _components = transform.WithComponentsInParent<T>(action);
        if(_components.Length > 0)
            for(int i = 0; i < _components.Length; i++)
            {
                action(_components[i]);
            }
        return _components;
    }

    public T WithComponentInChildren<T> (Action<T> action) where T : Component
    {
        T _component = transform.WithComponentInChildren<T>(action);

        if (_component)
            action(_component);

        return _component;
    }

    public T[] WithComponentsInChildren<T>(Action<T> action) where T : Component
    {
        T[] _components = transform.WithComponentsInChildren<T>(action);
        if(_components.Length > 0)
            for(int i = 0; i <_components.Length; i++)
            {
                action(_components[i]);
            }
        return _components;
    }


    //EXAMPLE with component caching
    //public T WithComponent<T> (Action<T> action) where T : Component
    //{
    //    //var _type = typeof(T);
    //    //T _cached = null;
    //    //bool _contains = cachedComponents.ContainsKey(_type);

    //    //// if the dictionary contains the component already, assign it to _cached as its type
    //    //if (_contains)
    //    //    _cached = cachedComponents[_type] as T;

    //    //// if the dictionary contains the component and it hasn't been removed from the object, complete the action
    //    //if (_contains && _cached)
    //    //{
    //    //    action(_cached);
    //    //    return _cached;
    //    //}

    //    // assign the component to a variable of its type 
    //    T _component = transform.WithComponent<T>(action);

    //    // if the component is not null, add it to the dictionary
    //    //if (_component)
    //    //    cachedComponents[_type] = _component;
    //    //// if the component does exist in the dictionary but is not on the object, it was purposely removed from the object so 
    //    //else if (_contains)
    //    //    cachedComponents.Remove(_type);

    //    if (_component)
    //        action(_component);

    //    return _component;
    //}


}
