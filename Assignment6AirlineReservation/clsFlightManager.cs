using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsFlightManager
    {
        public List<clsFlight> lstFlight;


        /// <summary>
        /// gets the flights list
        /// </summary>
        /// <returns> list of flights </returns>
        public List<clsFlight> GetFlight()
        {
            try
            {
                // get access to database
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();

                int iRetVal = 0;
                lstFlight = new List<clsFlight>();
                string sSQL = clsSql.GetFlights();
                //execute Statment
                ds = db.ExecuteSQLStatement(sSQL, ref iRetVal);

                //loop through dataset
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsFlight flight = new clsFlight();
                    flight.sFlightID = dr[0].ToString();
                    flight.sFlightNum = dr[1].ToString();
                    flight.sAircraftType = dr[2].ToString();
                    lstFlight.Add(flight);
                }
                return lstFlight;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }
}
