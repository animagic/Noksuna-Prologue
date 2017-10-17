using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public static EventManager StaticEventManager;

    public static class EventNames
    {
        public static string UsePylonAbility = "UsePylonAbility";
        public static string UpdatePlayerUIValues = "UpdatePlayerUIValues";
    }

    Dictionary<string, UnityEvent> EventDictionary = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
        if(StaticEventManager == null)
        {
            DontDestroyOnLoad(this.gameObject);
            StaticEventManager = this;
        }
        else if (StaticEventManager != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void StartListening (string _name, UnityAction _listener)
    {
        UnityEvent _event = null;

        if(StaticEventManager.EventDictionary.TryGetValue(_name, out _event))
        {
            _event.AddListener(_listener);
        }
        else
        {
            _event = new UnityEvent();
            _event.AddListener(_listener);
            StaticEventManager.EventDictionary.Add(_name, _event);
        }
    }

    public static void StopListening(string _name, UnityAction _listener)
    {
        if (StaticEventManager == null)
            return;
        if (StaticEventManager.EventDictionary == null)
            return;

        UnityEvent _event = null;
        if (StaticEventManager.EventDictionary.TryGetValue(_name, out _event))
        {
            _event.RemoveListener(_listener);
        }
    }

    public static void TriggerEvent(string _name)
    {
        UnityEvent _event = null;
        if(StaticEventManager.EventDictionary.TryGetValue(_name, out _event))
        {
            _event.Invoke();
        }
    }
}
