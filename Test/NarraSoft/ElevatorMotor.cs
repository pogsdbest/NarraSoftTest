using System;
using System.Collections.Generic;
using System.Text;

namespace NarraSoft {
    class ElevatorMotor : IElevatorMotor {
        //Tells the Current Direction of this ElevatorMotor UP,DOWN,STOP
        public Direction CurrentDirection { get; set; }
        //Tells the current Floor Of This ElevatorMotor
        public int CurrentFloor { get; set; }
        //Event trigger everytime the floor is reached
        public event Action<int> ReachedFloor;
        /*Constructor for ElevatorMotor
         * initialize CurrentDirection as STOP by default
         * initialize CurrentFloor to 1 by default
         */
        public ElevatorMotor() {
            CurrentDirection = Direction.STOP;
            CurrentFloor = 1;
        }

        /*Method to move this elevator floor by floor
         * depending on the current direction.CurrentFloor increased by 1 if
         * Direction is UP and decrease by 1 if Direction is DOWN, returns immidiatly
         * if Direction is STOP
         * 
         * param int floor - next floor to go.
         */
        public void GoToFloor( int floor ) {
            this.CurrentFloor = floor;
            ReachedFloor?.Invoke (floor);
            switch(CurrentDirection) {
                case Direction.UP:
                    floor++;
                    break;
                case Direction.DOWN:
                    floor--;
                    break;
                case Direction.STOP:
                    return;
            }
            //recursion, repeats until it stops
            GoToFloor ( floor );
        }
    }
}
