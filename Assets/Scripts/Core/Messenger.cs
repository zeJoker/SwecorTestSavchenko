using System;
using System.Collections.Generic;

public delegate void Callback();
public delegate void Callback<T>(T arg1);
public delegate void Callback<T, U>(T arg1, U arg2);
public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);

static internal class Messenger
{
    static private Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    //AddListener
    //No parameters
    static public void AddListener(string eventType, Callback handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        eventTable[eventType] = (Callback)eventTable[eventType] + handler;
    }

    //Single parameter
    static public void AddListener<T>(string eventType, Callback<T> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        eventTable[eventType] = (Callback<T>)eventTable[eventType] + handler;
    }

    //Two parameters
    static public void AddListener<T, U>(string eventType, Callback<T, U> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        eventTable[eventType] = (Callback<T, U>)eventTable[eventType] + handler;
    }

    //Three parameters
    static public void AddListener<T, U, V>(string eventType, Callback<T, U, V> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] + handler;
    }

    //RemoveListener
    //No parameters
    static public void RemoveListener(string eventType, Callback handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }

        eventTable[eventType] = (Callback)eventTable[eventType] - handler;
    }

    //Single parameter
    static public void RemoveListener<T>(string eventType, Callback<T> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }

        eventTable[eventType] = (Callback<T>)eventTable[eventType] - handler;
    }

    //Two parameters
    static public void RemoveListener<T, U>(string eventType, Callback<T, U> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }

        eventTable[eventType] = (Callback<T, U>)eventTable[eventType] - handler;
    }

    //Three parameters
    static public void RemoveListener<T, U, V>(string eventType, Callback<T, U, V> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }

        eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] - handler;
    }

    //Broadcast
    static public void Broadcast(string eventType)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        Callback handler = eventTable[eventType] as Callback;
        if (handler != null)
        {
            handler();
        }
    }

    static public void Broadcast<T>(string eventType, T arg1)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        Callback<T> handler = eventTable[eventType] as Callback<T>;
        if (handler != null)
        {
            handler(arg1);
        }
    }

    static public void Broadcast<T, U>(string eventType, T arg1, U arg2)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        Callback<T, U> handler = eventTable[eventType] as Callback<T, U>;
        if (handler != null)
        {
            handler(arg1, arg2);
        }
    }

    static public void Broadcast<T, U, V>(string eventType, T arg1, U arg2, V arg3)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }

        Callback<T, U, V> handler = eventTable[eventType] as Callback<T, U, V>;
        if (handler != null)
        {
            handler(arg1, arg2, arg3);
        }
    }
}
