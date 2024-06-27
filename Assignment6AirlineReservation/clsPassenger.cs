using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsPassenger
    {

        //PassengerID, firstName, LastName
        /// <summary>
        /// gets the passsagnerID
        /// </summary>

        public string sPassengerID { get; set; }

        /// <summary>
        /// gets the passsagner firstname
        /// </summary>

        public string sFirstName { get; set; }

        /// <summary>
        /// gets the passsagners lastname
        /// </summary>
        
        public string sLastName { get; set; }


        /// <summary>
        /// gets the seat number
        /// </summary>
        public string sSeatNumber { get; set; }

        //ToString
        /// <summary>
        /// gets the sting of var
        /// </summary>
        /// <returns> list of passengers </returns> 
        public override string ToString()
        {
            return $"Passenger ID: {sPassengerID}, First Name: {sFirstName}, Last Name: {sLastName}, Seat Number: {sSeatNumber}";
        }

    }
}
