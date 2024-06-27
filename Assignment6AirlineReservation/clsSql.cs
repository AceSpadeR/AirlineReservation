using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsSql
    {
        /// <summary>
        /// gets sql statment for getflights
        /// </summary>
        /// <returns> string sql </returns>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// gets sql statment for passenger with flight id
        /// </summary>
        /// <returns> string sql </returns>
        public static string GetPassengers(string flightID)
        {
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                      "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                  "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                  "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                  "FLIGHT.FLIGHT_ID = " + flightID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// get execution statment for updating seats
        /// </summary>
        /// <returns> string sql </returns>
        public static string UpdateSeat(string seatNumber, string flightID, string passengerID)
        {
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
           "SET Seat_Number = '" + seatNumber +
           "' WHERE FLIGHT_ID = " + flightID + " AND PASSENGER_ID = " + passengerID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// get execution statment for inserting seats
        /// </summary>
        /// <returns> string sql </returns>
        public static string InsertPassenger(string firstName, string lastName)
        {
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('" + firstName + "','" + lastName + "')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// get execution statment for inserting link
        /// </summary>
        /// <returns> string sql </returns>
        public static string InsertLinkTable(string seatNumber, string flightID, string passengerID)
        {
            try
            {
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " + "VALUES( " + flightID + " , " + passengerID + " , " + seatNumber + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        //Deleting the link
        public static string DeleteLink(string flightID, string passengerID)
        {
            try
            {
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
           "WHERE FLIGHT_ID = " + flightID + " AND " +
           "PASSENGER_ID = " + passengerID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        //Delete the passenger
        public static string DeletePassenger(string passengerID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER " + "WHERE PASSENGER_ID = " + passengerID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        //Get the passenger's ID
        public static string GetPassenger(string firstName, string lastName)
        {
            try
            {
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '" + firstName + "' AND Last_Name = '" + lastName + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


    }
}
