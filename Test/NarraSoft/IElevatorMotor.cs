using System;
using System.Collections.Generic;
using System.Text;

namespace NarraSoft {
    public interface IElevatorMotor {
        Direction CurrentDirection { get; }
        int CurrentFloor { get; }

        event Action<int> ReachedFloor;

        void GoToFloor( int floor );
    }
}
