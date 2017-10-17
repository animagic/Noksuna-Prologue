using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Extensions
{
    public static T WithComponent<T>(this Transform transform, Action<T> action) where T : Component
    {
        var _component = transform.GetComponent<T>();

        if (_component)
        {
            action(_component);
        }

        return _component;
    }

    public static T WithComponent<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        return gameObject.transform.WithComponent(action);
    }

    public static T WithComponentInChildren<T>(this Transform transform, Action<T> action) where T : Component
    {
        T _component = transform.GetComponentInChildren<T>();

        if (_component)
        {
            action(_component);
        }

        return _component;
    }

    public static T WithComponentInChildren<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        return gameObject.transform.WithComponentInChildren(action);
    }

    public static T[] WithComponentsInChildren<T>(this Transform transform, Action<T> action) where T : Component
    {
        T[] _components = transform.GetComponentsInChildren<T>();

        foreach(T _component in _components)
        {
            action(_component);
        }

        return _components;
    }

    public static T[] WithComponentsInChildren<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        return gameObject.transform.WithComponentsInChildren(action);
    }

    public static T WithComponentInParent<T>(this Transform transform, Action<T> action) where T : Component
    {
        T _component = transform.GetComponentInParent<T>();
        
        if (_component)
        {
            action(_component);
        }

        return _component;
    }

    public static T WithComponentInParent<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        return gameObject.transform.WithComponentInParent(action);
    }

    public static T[] WithComponentsInParent<T>(this Transform transform, Action<T> action) where T : Component
    {
        T[] _components = transform.GetComponentsInParent<T>();
        foreach(T _component in _components)
        {
             action(_component);
        }
        return _components;
    }

    public static T[] WithComponentsInParent<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        return gameObject.transform.WithComponentsInParent(action);
    }

    public static bool RectOverlaps(this RectTransform draggedRect, RectTransform checkedRect)
    {
        Rect a = new Rect(draggedRect.localPosition.x, draggedRect.localPosition.y, draggedRect.rect.width, draggedRect.rect.height);
        Rect b = new Rect(checkedRect.localPosition.x, checkedRect.localPosition.y, checkedRect.rect.width, checkedRect.rect.height);

        return a.Overlaps(b);
    }

}

