using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythumProto.Timer
{
    public class TimerTarget : MonoBehaviour, ITimerOut
    {
        virtual public void TimeOut() { }
    }
}
