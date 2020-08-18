using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Linq;
using NarraSoft;

namespace Test {
    public class NarraSoftTest {
        static void Main( string[] args ) {
            //unique digit Test
            Console.WriteLine ( NarraSoftUtil.AllDigitsUnique ( 48778584 ) );
            Console.WriteLine ( NarraSoftUtil.AllDigitsUnique ( 17308459 ) );

            //sort Letters Test
            string inputStr = "trion world network";
            string sortStr = "oinewkrtdl";
            byte[] inputAndOutputBytes = System.Text.Encoding.ASCII.GetBytes ( inputStr );
            byte[] sortBytes = System.Text.Encoding.ASCII.GetBytes ( sortStr );
            NarraSoftUtil.SortLetters ( inputAndOutputBytes, sortBytes );
            string outputStr = System.Text.Encoding.ASCII.GetString ( inputAndOutputBytes );
            Console.WriteLine (outputStr);

            //Maze Inter-connected Room Test
            //rooms are not bi directional, if room1 is connected to room2, room2 might not be connected to room1,unless you connect them
            //rooms are not spatially coherent, if room1 is connected to room2 via South Door, then room1 may not be room2's North Door
            Room room1 = RoomFactory.GetInstance().CreateRoom  ( "Room1" );
            Room room2 = RoomFactory.GetInstance ().CreateRoom ( "Room2" );
            Room room3 = RoomFactory.GetInstance ().CreateRoom ( "Room3" );
            Room room4 = RoomFactory.GetInstance ().CreateRoom ( "Room4" );
            Room room5 = RoomFactory.GetInstance ().CreateRoom ( "Room5" );

            //room1 connects to room2 using south door
            room1.Connect (Room.Door.SOUTH,room2);
            //room2 connects to room3 using north door
            room2.Connect (Room.Door.NORTH,room3);
            //room3 should be reachable by room1
            Console.WriteLine ( "room1 path exist to room3 " + room1.PathExistsTo (room3.roomName) );
            //room4 connects to room2 using south west door
            room4.Connect (Room.Door.WEST, room2);
            //room3 should be reachable by room4 because room2 is already connected to room3
            Console.WriteLine ( "room4 path exist to room3 " + room4.PathExistsTo ( room3.roomName ) );
            //lets replace room2's north door to room5
            room2.Connect ( Room.Door.NORTH, room5 );
            //room3 is no longer reachable by room4
            Console.WriteLine ( "room4 path exist to room3 "+room4.PathExistsTo ( room3.roomName ) );

            //Elavator Controller Test

            //initialize Elevator Controller with 10 floors (including the ground floor = 1)
            ElevatorController controller = new ElevatorController (10);
            //adding Event when Destination is reached
            controller.ReachedDestinationFloor += delegate ( int floor ) {
                Console.WriteLine ("reached Destination floor "+floor);
            };
            //adding Event when Summoned Floor is reached
            controller.ReachedSummonedFloor += delegate ( int floor ) {
                Console.WriteLine ( "reached Summoned floor " + floor );
            };

            //Inside The Elevator every Person needs to Press their floor
            controller.FloorButtonPushed ( 5 );     //first Person
            controller.FloorButtonPushed ( 3 );     //second Person
            //outside the elevator other Person pushed the summoned button from another floor
            controller.SummonButtonPushed ( 5 );
            //people inside the elevator closed the elevator
            controller.CloseElevator ();            //starting from ground floor(1st floor )this should reach floor 3 then floor 5
            //From Current Floor which the Elevator Stops, another Person(s) press the floor buttons
            controller.FloorButtonPushed ( 10 );    //going up first
            controller.FloorButtonPushed ( 3 );     //then going down
            //After Closing the elevator, the first person who pressed will be the priority.
            controller.CloseElevator ();            //this should go to 10th floor first then back to 3rd floor
            //Even if the elevator is Empty, Some Person can summon the elevator
            controller.SummonButtonPushed ( 1 );    //someone summon the elevator from 1st floor
            controller.SummonButtonPushed ( 4 );    //someone summon the elevator from 4th floor

            //You can also Set some Event For Example, when someone enter the elevator after summoning.
            Action<int> act1 = null;
            act1 = delegate ( int floor ) {
                if(floor == 1) {
                    controller.FloorButtonPushed ( 3 ); //the person who pressed 1st floor enter the elevator and pressed 3rd Floor as destination
                    controller.ReachedSummonedFloor -= act1;
                }
            };
            controller.ReachedSummonedFloor += act1;
            controller.CloseElevator ();    //the elevator should go from 3rd floor(last) to 1st Floor(first person to summon) then to 3rd Floor again and finally to 4th floor
        }
    }
}
