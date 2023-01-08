using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    //there should be some punishment if the player doesn't do the task at the right time, what should it be?
    // shot with guaranteed system failure?
    [Serializable]
    public class OrdersManager : SerializedMonoBehaviour
    {
        [OdinSerialize] private Queue<Order> _orderQueue;
        private Order _currentOrder;

        [SerializeField] private WarningUI UI;

        private float _currentTime = 0;

        [SerializeField] private ShipManager ship;

        [SerializeField] private ShipComponent LifeSupport, Blasters, Torpedo, Shields, Ftl, Helm;

        private bool hasFiredOnce = false;

        private void Awake()
        {
        }

        private void Start()
        {
            if (_orderQueue.Count == 0) return;
            _currentOrder = _orderQueue.Dequeue();
        }

        private void Update()
        {
            if (_orderQueue.Count == 0) return;
            _currentTime += Time.deltaTime;


            switch (_currentOrder.type)
            {
                case ComponentTypes.Blaster:
                    _currentOrder.checkIfFulfilled(Blasters);
                    break;
                case ComponentTypes.Ftl:
                    _currentOrder.checkIfFulfilled(Ftl);
                    break; 
                case ComponentTypes.Helm:
                    _currentOrder.checkIfFulfilled(Helm);
                    break;
                case ComponentTypes.LifeSupport:
                    _currentOrder.checkIfFulfilled(Helm);
                    break;
                case ComponentTypes.Torpedo:
                    _currentOrder.checkIfFulfilled(Torpedo);
                    break;
                case ComponentTypes.Shield:
                    _currentOrder.checkIfFulfilled(Shields);
                    break;
                case ComponentTypes.None:
                    _currentOrder.checkIfFulfilled(null);
                    break;
            }

            if (_currentOrder.hasBeenFulfilled)
            {
                UI.DisableOrderMessage();
                
                if (_currentTime >= _currentOrder.timeToWaitAfter)
                {
                    print("Order Success");
                    _currentOrder = _orderQueue.Dequeue();
                    if (_currentOrder.isVictory)
                    {
                        ship.EndGame(true);
                        return;
                    }

                    hasFiredOnce = false;
                    UI.SetOrderMessage(_currentOrder.message);
                    _currentTime = 0;

                }
            }
            else
            {
                UI.SetOrderMessage(_currentOrder.message);
            }
            if (!_currentOrder.hasBeenFulfilled && !hasFiredOnce && _currentTime >= _currentOrder.acceptableDownTime)
            {
                ship.Shot();
                hasFiredOnce = true;
            }

            
        }
    }
}