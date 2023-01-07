using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    //there should be some punishment if the player doesn't do the task at the right time, what should it be?
    // shot with guaranteed system failure?
    [Serializable]
    struct Order
    {
        public ComponentTypes type;
        public float TimeToWaitAfter;
        public float acceptableDownTime;
        public bool hasBeenFulfilled;
    }
    public class OrdersManager : SerializedMonoBehaviour
    {
        [OdinSerialize] private Queue<Order> orderQueue;
        private Order currentOrder;

        private float currentTime = 0;

        private void Awake()
        {
            orderQueue = new Queue<Order>();
        }

        private void Start()
        {
            currentOrder = orderQueue.Dequeue();
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentOrder.hasBeenFulfilled && currentTime >= currentOrder.TimeToWaitAfter)
            {
                currentOrder = orderQueue.Dequeue();
                currentTime = 0;
            }
            else if (currentTime >= currentOrder.acceptableDownTime)
            {
                //bad thing!
            }
        }

        public void ComponentUpdated(ComponentTypes type, int value)
        {
            if (currentOrder.type != type || value != 2) return;
            
            currentOrder.hasBeenFulfilled = true;
            currentTime = 0;
        }
    }
}