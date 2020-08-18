using System;
using System.Collections.Generic;
using System.Text;

namespace NarraSoft {
    public class RoomFactory {

        //Singleton instance of RoomFactory
        private static RoomFactory instance;

        //List of unique rooms created via name
        private Dictionary<string, Room> uniqueRooms = new Dictionary<string, Room> ();

        /*Method for calling RoomFactory instance. 
         */
        public static RoomFactory GetInstance() {
            if(instance == null) {
                instance = new RoomFactory ();
            }
            return instance;
        }

        /*Each Room has a unique name, therefore RoomFactory has the only authority to
         * Create Room using a unique name
         * 
         * param string roomName - the name of the created Room
         * 
         * returns Room - the newly created Room with a unique roomName
         * 
         * throws ArgumentException - if the roomName is null or Empty or Exist
         */
        public Room CreateRoom (string roomName) {

            if(uniqueRooms.ContainsKey(roomName) ) {
                throw new System.ArgumentException ( "Room name already Exist.", "roomName" );
            }

            if(String.IsNullOrEmpty ( roomName )) {
                throw new System.ArgumentException ( "Parameter cannot be null or Empty", "roomName" );
            }

            Room room = new Room (roomName);
            uniqueRooms.Add (room.roomName, room);
            return room;
        }
    }
}
