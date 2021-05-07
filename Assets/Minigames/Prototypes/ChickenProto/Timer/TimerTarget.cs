using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenPrototype.Timer
{
    public class TimerTarget : MonoBehaviour, ITimerOut
    {
        virtual public void TimeOut() { }
    }
}
