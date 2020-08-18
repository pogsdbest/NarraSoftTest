using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace NarraSoft {
    public class ElevatorController : IElevatorController {

        //maximum floors the elevator can reach
        private int maxFloors;
        //Elevator Object
        private ElevatorMotor elevator;
        //List of floor destination pressed by person inside the elevator
        private List<int> floorDestination;
        //List of floor summoned by person outside the elevator
        private List<int> floorSummoned;

        //*** implemented variables ***
        //Event Invoked when summoned floor is Reached
        public event Action<int> ReachedSummonedFloor;
        //Event Invoked when destination floor is Reached
        public event Action<int> ReachedDestinationFloor;

        /*ElevatorController Constructor
         * Initialized maximum floors based on the passing parameters
         * Initialized ElevatorMotor Object
         * Initialized ReachedFloor as OnReachedFloor for handling reached floors destinations and summoned floors
         * Initialized floorDestination and floorSummoned list
         * 
         * param int floors - maximum floors that the elevator can reached
         */
        public ElevatorController(int floors) {
            this.maxFloors = floors;
            elevator = new ElevatorMotor ();
            elevator.ReachedFloor += OnReachedFloor;
            floorDestination = new List<int> ();
            floorSummoned = new List<int> ();
        }

        /*Event for Invoking ReachedDestination Event and ReachedSummon Event
         * Removes Reached destination floors and summoned floors on the list
         * Changes the Direction when ElevatorMotor.CurrentFloor Reached the maximum Floors or 0
         * Sets the Elevator Direction to STOP, when floorDestination and floorSummoned is Empty
         * 
         * param int floor - reached floor
         */
        public void OnReachedFloor(int floor) {

            if(floorDestination.Contains(floor)) {
                Action<int> e = ReachedDestinationFloor;
                e?.Invoke ( floor );

                floorDestination.Remove (floor);
                
            }
            if(floorSummoned.Contains(floor)) {
                Action<int> e = ReachedSummonedFloor;
                e?.Invoke ( floor );

                floorSummoned.Remove (floor);
            }
            
            if(floor >= maxFloors) {
                elevator.CurrentDirection = Direction.DOWN;
            } else if(floor <= 0) {
                elevator.CurrentDirection = Direction.UP;
            }

            if(floorDestination.Count == 0 && floorSummoned.Count == 0) {
                elevator.CurrentDirection = Direction.STOP;
            }

        }

        /*Method to Start the Process of ElevatorMotor to start moving UP or DOWN or remain STOP
         * 
         */
        public void CloseElevator() {
            //get the current floor where the elevator stop
            int currentFloor = elevator.CurrentFloor;
            //get the status of the elevator
            Direction direction = elevator.CurrentDirection;

            //if the direction is currently on STOP. then first floor destination is the priority
            if(direction == Direction.STOP) {
                int firstFloorPriority = currentFloor;
                if(floorDestination.Count > 0) {
                    firstFloorPriority = floorDestination[0];
                } else if(floorSummoned.Count > 0) {
                    //if floorDestination has no entry, then summoned Floor will be the priority
                    firstFloorPriority = floorSummoned[0];
                } else {
                    //otherwise, the elevator will closed, and remained STOP
                    return;
                }

                if(currentFloor > firstFloorPriority) {
                    direction = Direction.DOWN;
                } else if(currentFloor < firstFloorPriority) {
                    direction = Direction.UP;
                }
                elevator.CurrentDirection = direction;
            }

            if(direction == Direction.UP) {
                elevator.GoToFloor ( ++currentFloor );
            } else if(direction == Direction.DOWN) {
                elevator.GoToFloor ( --currentFloor );
            }

        }

        //***  implemented Methods  ***

        /*Method used by The User Inside The elevator.
         * param int floor - destination floor of the user, only accepts integer from 0 to maxFloors
         */
        public void FloorButtonPushed( int floor ) {
            if(floor <= 0 || floor > maxFloors) {
                throw new System.ArgumentException ( "Invalid Floor, Floor must be from 1 to "+maxFloors, "floor" );
            }
            //pressing floor button on the current floor means nothing
            if(floor == elevator.CurrentFloor)
                return;

            //add floors
            if(!floorDestination.Contains(floor))
                floorDestination.Add (floor);
        }

        /*Method used by The User Outside The elevator.
         * param int floor - floor in which the user want to summon the elevator, only accepts integer from 0 to maxFloors
         */
        public void SummonButtonPushed( int floor ) {
            if(floor <= 0 || floor > maxFloors) {
                throw new System.ArgumentException ( "Invalid Floor, Floor must be from 1 to " + maxFloors, "floor" );
            }
            if(!floorSummoned.Contains ( floor ))
                floorSummoned.Add ( floor );

        }
    }
}
