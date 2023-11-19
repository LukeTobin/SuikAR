using System;
using System.Collections.Generic;

namespace SuikAR.Events
{
    public static class EventManager
    {
        private static Dictionary<Event, Delegate> eventDictionary = new Dictionary<Event, Delegate>();

        public enum Event
        {
            OnFruitCombine,
            OnFruitQueued
        }
        
        public static void Subscribe<T>(Event eventType, Action<T> listener)
        {
            eventDictionary.TryAdd(eventType, null);
            eventDictionary[eventType] = Delegate.Combine(eventDictionary[eventType], listener);
        }
        
        public static void Unsubscribe<T>(Event eventType, Action<T> listener)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType] = Delegate.Remove(eventDictionary[eventType], listener);
            }
        }

        public static void Invoke<T>(Event eventType, T value)
        {
            if (eventDictionary.TryGetValue(eventType, out Delegate eventDelegate))
            {
                (eventDelegate as Action<T>)?.Invoke(value);
            }
        }
    }
}