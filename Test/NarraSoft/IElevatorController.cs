using System;
using System.Collections.Generic;
using System.Text;

namespace NarraSoft {
    public interface IElevatorController {
        void SummonButtonPushed( int floor );
        void FloorButtonPushed( int floor );

        event Action<int> ReachedSummonedFloor;
        event Action<int> ReachedDestinationFloor;
    }
}
