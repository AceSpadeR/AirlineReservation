using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsPassengerManager
    {
        public List<clsPassenger> lstPassengers;




        /// <summary>
        /// gets the passsagners
        /// </summary>
        /// <returns> list of passengers </returns>
        public List<clsPassenger> GetPassenger(string sflightID)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                lstPassengers = new List<clsPassenger>();

                //execute Statment
                ds = db.ExecuteSQLStatement(clsSql.GetPassengers(sflightID), ref iRet);

                //loop through dataset
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassenger passenger = new clsPassenger();
                    passenger.sPassengerID = dr[0].ToString();
                    passenger.sFirstName = dr[1].ToString();
                    passenger.sLastName = dr[2].ToString();
                    passenger.sSeatNumber = dr[3].ToString();
                    lstPassengers.Add(passenger);
                }
                return lstPassengers;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// updates seats
        /// </summary>

        public void ChangeSeat(string seatNumber, string flightID, string passengerID)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                //execute Statment
                db.ExecuteNonQuery(clsSql.UpdateSeat(seatNumber, flightID, passengerID));

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// insert passenger
        /// </summary>

        public void InsertPassenger(string firstName, string lastName)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                //execute Statment
                db.ExecuteNonQuery(clsSql.InsertPassenger(firstName, lastName));

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete Passenger
        /// </summary>
        /// <returns> list of passengers </returns>
        public void DeletePassenger(string flightID, string passengerID)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                //execute Statment
                db.ExecuteNonQuery(clsSql.DeleteLink(flightID, passengerID));
                db.ExecuteNonQuery(clsSql.DeletePassenger(passengerID));

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Inserts link
        /// </summary>
        public void InsertLinkTable(string seatNumber, string flightID, string passengerID)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                //execute Statment
                db.ExecuteNonQuery(clsSql.InsertLinkTable(seatNumber, flightID, passengerID));

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// get passenger ID
        /// </summary>
        /// <returns> passengerID</returns>
        public string GetPassenger(string firstName, string lastName)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;

                //execute Statment
                ds = db.ExecuteSQLStatement(clsSql.GetPassenger(firstName, lastName), ref iRet);
                DataTable dt = ds.Tables[0]; 
                DataRow row = dt.Rows[0]; 
                string passengerID = row["Passenger_ID"].ToString();
                return passengerID;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
