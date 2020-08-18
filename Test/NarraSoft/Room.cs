using System;
using System.Collections.Generic;
using System.Text;

namespace NarraSoft {

    public class Room : IPathable {
        /*Available Doors for this room
         */
        public enum Door {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        //unique room name
        public string roomName;

        //doors connected to this room, must be unique
        Dictionary<Door, Room> connectedRooms = new Dictionary<Door, Room> ();

        /*Constructor when creating a new Room
         * param string name - the name of this room
         */
        public Room(string name) {
            this.roomName = name;
        }

        /*Method for Connecting this Room to a Room through doors
         * if the door is already used, then we will replace it
         * 
         * param Room.Door door - the door in which this room is connected
         * param Room room - the room to connect with
         */
        public void Connect(Door door,Room room) {
            //check if the door is already connected to another room
            if(connectedRooms.ContainsKey(door)) {
                //replace the existing room with the new room
                connectedRooms[door] = room;
            } else {
                //if the door is not yet connected then add the room with a door
                connectedRooms.Add ( door, room );
            }
        }

        /*method to determine if any path exists between a starting 
         * room and an ending room with a given name.
         * param string endingRoomName - the room name to check if theres a path available.
         * 
         * returns bool true - if endingRoomName is equal to one of the connected Room
         * returns bool false - if endingRoomName is not Equal to any connected Rooms
         */
        public bool PathExistsTo( string endingRoomName ) {
            if(endingRoomName.Equals ( roomName ))
                return true;

            //iterate through connected Rooms
            foreach(KeyValuePair<Door, Room> entry in connectedRooms) {
                Room room = entry.Value;
                //if the roomName connected to this room is the Room that we are looking for
                if(endingRoomName.Equals(roomName)) {
                    return true;
                } else {
                    //check also the path connected to this room, Recursion
                    return room.PathExistsTo (endingRoomName);
                }
            }
            return false;
        }
    }
}
