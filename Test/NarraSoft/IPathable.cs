using System;
using System.Collections.Generic;
using System.Text;

namespace NarraSoft {
    public interface IPathable {

        bool PathExistsTo( string endingRoomName );
    }
}
