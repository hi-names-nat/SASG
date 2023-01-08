using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "New Order", menuName = "SASG/Order", order = 0)]
    public class Order : ScriptableObject
    {
        public ComponentTypes type;
        public float timeToWaitAfter;
        public float acceptableDownTime;
        public bool hasBeenFulfilled;
        public bool isVictory;
        public int requestedPowerAmt;
        [SerializeField, TextArea] public string message;

        public void checkIfFulfilled(ShipComponent component)
        {
            //the 'wait' case.
            if (type == ComponentTypes.None)
            {
                hasBeenFulfilled = true;
                return;
            }

            if (component.isBroken)
            {
                hasBeenFulfilled = false;
                return;
            }
            
            if (component.Type == type && component.GetPowerInput() == requestedPowerAmt)
            {
                hasBeenFulfilled = true;
            }
            else hasBeenFulfilled = false;

        }
    }
}