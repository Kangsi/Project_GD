// John van den Berg

/*=============================================================================
	Event Manager
=============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * A flexible event system using .Net functionality.
 * 
 * Why it is used:
 * 
 * Event-driven programming is used to increase program efficiency.
 * Using the Unity Update() method to handle all game logic every 
 * single frame (interactive polling) is inefficient and will quickly 
 * slow down our program by increasing CPU cycles. Event systems only
 * execute code when an event has occured. 
 * 
 * What it does:
 * 
 * The .Net Dictionary is used to keep track of all event listeners. 
 * Unity objects can post events to this system and the event manager 
 * will send a notification to all the appropriate listeners telling 
 * them that the event occured. 
 * 
 * How it works:
 * 
 * The .Net Dictionary allows us to communicate between objects by 
 * taking in two arguments <TKey, TValue>. In this case we use a string
 * and a list of unity objects. The dictionary works by communicating
 * the string to the list of objects. The .Net Dictionary uses seperate
 * lists for each event, rather than storing them in one big list, to
 * provide fast and efficient communication between objects
 * 
 */
namespace Manager
{
    public class EventManager : MonoBehaviour
    {
        ///////////////////////
        // Public variables
        //----------------------------------------
        // Public Instance
        public static EventManager Instance
        {
            get
            {
                if (instance == null) instance = new EventManager();

                return instance;
            }
        }

        ///////////////////////
        // Private variables
        private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component>>();
        //---------------------------------------
        // Internal reference
        private static EventManager instance = null;

        ///////////////////////
        // Unity
        void Awake()
        {
            //Check if an instance already exists 
            if (instance)
                DestroyImmediate(gameObject); //Delete duplicate
            else
            {
                instance = this; //Make this object the only instance
                DontDestroyOnLoad(gameObject); //Set as do not distroy
            }
        }

        ///////////////////////
        // Funcions
        //---------------------------------------
        // Function to add listener for an event to the listeners list
        public void AddListener(Component Sender, string EventName)
        {            
            //Add listener to dictionary
            if (!Listeners.ContainsKey(EventName))
                Listeners.Add(EventName, new List<Component>());

            //Add object to listener list for this event
            Listeners[EventName].Add(Sender);
        }
        //---------------------------------------
        // Function to remove a listener for an event
        public void RemoveListener(Component Sender, string EventName)
        {
            //If no key in the dictionary exists, then exit
            if (!Listeners.ContainsKey(EventName))
                return;

            //Cycle through listeners and identify component, and then remove using bottom up method
            for (int i = Listeners[EventName].Count-1; i >= 0; i--)
            {
                //Check instance ID
                if (Listeners[EventName][i].GetInstanceID() == Sender.GetInstanceID())
                    Listeners[EventName].RemoveAt(i);
            }
        }
        //---------------------------------------
        // Function to post an event to a listener
        public void PostEvent(Component Sender, string EventName)
        {
            //If no key in the dictionary exists, then exit
            if (!Listeners.ContainsKey(EventName))
                return;

            //Else post event to all matching listeners
            foreach (Component Listener in Listeners[EventName])
                Listener.SendMessage(EventName, Sender, SendMessageOptions.DontRequireReceiver);
        }
        //---------------------------------------
        // Function to clear all listeners
        public void ClearListeners()
        {
            //Removes all listeners
            Listeners.Clear();
        }
        //---------------------------------------
        // Function to remove all redundant listeners - deleted and removed listeners
        public void RemoveRedundancies()
        {
            //Create new dictionary
            Dictionary<string, List<Component>> TempListeners = new Dictionary<string, List<Component>>();

            //Cycle through all dictionary entries
            foreach (KeyValuePair<string, List<Component>> Item in Listeners)
            {
                for(int i = Item.Value.Count-1; i>=0; i--)
                {
                    //If null, then remove item
                    if (Item.Value[i] == null)
                        Item.Value.RemoveAt(i);
                }

                //If items remain in list for this event, then add this to Temp dictionary
                if (Item.Value.Count > 0)
                    TempListeners.Add(Item.Key, Item.Value);
            }

            //Replace listeners object with the new optimized dictionary
            Listeners = TempListeners;
        }
        //---------------------------------------
        // Called when a new level is loaded; remove redundant entries from dictionary; in case left-over from previous level
        void OnLevelWasLoaded()
        {
            //Clear redundancies
            RemoveRedundancies();
        }
    }
}

