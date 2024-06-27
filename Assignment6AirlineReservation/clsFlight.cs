using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsFlight
    {
        //FLightID, FlightNum, AircraftType
        /// <summary>
        /// get/set the flightID
        /// </summary>
        public string sFlightID { get; set; }

        /// <summary>
        /// get/set the flight name
        /// </summary>

        public string sFlightNum { get; set; }

        /// <summary>
        /// get/set the flightIDaircraft type
        /// </summary>
        public string sAircraftType { get; set; }

        /// <summary>
        /// gets the passsagners
        /// </summary>
        /// <returns> list of flights </returns>
        public override string ToString()
        {
            return $"Flight ID: {sFlightID}, Flight Number: {sFlightNum}, Aircraft Type: {sAircraftType}";
        }
    }
}
