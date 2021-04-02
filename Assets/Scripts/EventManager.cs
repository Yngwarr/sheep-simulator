using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    static EventManager eventManager;

    public static EventManager Get() {
        if (eventManager == null) {
            eventManager = new EventManager();
        }
        return eventManager;
    }

    // TODO make a generic version with dynamically added events
    public UnityEvent<Vector3> foodEaten = new UnityEvent<Vector3>();
}
