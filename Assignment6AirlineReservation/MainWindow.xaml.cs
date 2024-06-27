using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        clsFlight clsSelectedFlight;
        clsPassengerManager clsPassengerManager;
        clsPassenger clsSelectedPassenger;
        clsPassenger previousPassengerSeat;
        List<clsPassenger> lPassenger;
        string sPassFirstName, sPassLastName;
        wndAddPassenger wndAddPass;
        bool bSelectedPassenger = false;
        bool bChangeSeat = false;
        bool bAddPassenger = false;

        /// <summary>
        /// Used to pass data from window to window
        /// </summary>
        public static class clsAddPassData
        {
            public static string sPassFirstName;
            public static string sPassLastName;
            public static bool bSaved;
        }

        /// <summary>
        /// constructor get flights in cbox
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                clsFlightManager clsFlight = new clsFlightManager();
                cbChooseFlight.ItemsSource = clsFlight.GetFlight();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// change the flight and load passengers.
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //enable the other boxes
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                DataSet ds = new DataSet();
                int iRet = 0;

                //Should be using a flight object to get the flight ID here
                clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                if (clsSelectedFlight.sFlightID == "1")
                {
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Hidden;
                }
                else
                {

                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                //Get passengers
                clsPassengerManager = new clsPassengerManager();
                lPassenger = clsPassengerManager.GetPassenger(clsSelectedFlight.sFlightID);
                cbChoosePassenger.ItemsSource = lPassenger;
                //Set to red
                for (int i = 0; i < lPassenger.Count; i++)
                {
                    Label seatNum = LoadSeat(lPassenger[i]);
                    seatNum.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFD0000"));
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// updates after change in cbox
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbChoosePassenger.SelectedItem != null)
            {
                // get selectedPassenger seat number
                clsSelectedPassenger = (clsPassenger)cbChoosePassenger.SelectedItem;
                lblPassengersSeatNumber.Content = clsSelectedPassenger.sSeatNumber;
                if (previousPassengerSeat != null)
                {
                    //set previous seat to color
                    Label previousSeat = LoadSeat(previousPassengerSeat);
                    var LabelColor = (SolidColorBrush)previousSeat.Background;
                    if (LabelColor.Color.ToString() == "#FF00FD00" && (bChangeSeat == false || bAddPassenger == true))
                    {

                        previousSeat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFD0000"));

                        bChangeSeat = false;
                    }
                    else if (LabelColor.Color.ToString() == "#FFFD0000" && bAddPassenger == true)
                    {
                        bAddPassenger = false;
                    }
                    else if (bChangeSeat) { 

                    previousSeat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0023FD"));
                    bChangeSeat = false;
                    }

            }
            Label seatNum = LoadSeat(clsSelectedPassenger);
            seatNum.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00FD00"));
            previousPassengerSeat = clsSelectedPassenger;
            bSelectedPassenger = true;
            }
        }



        /// <summary>
        /// adds passenger to database
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                bChangeSeat = false;
                wndAddPass = new wndAddPassenger(clsSelectedFlight.sFlightID);
                wndAddPass.ShowDialog();
                if (clsAddPassData.bSaved == true)
                {
                    //get ready to set seat
                    DisableAll();
                    bAddPassenger = true;
                    sPassFirstName = clsAddPassData.sPassFirstName;
                    sPassLastName = clsAddPassData.sPassLastName;
                    clsPassengerManager.InsertPassenger(sPassFirstName, sPassLastName);
                    if (previousPassengerSeat != null)
                    {
                        Label previousSeat = LoadSeat(previousPassengerSeat);
                        var LabelColor = (SolidColorBrush)previousSeat.Background;
                        if (LabelColor.Color.ToString() == "#FF00FD00")
                        {
                            previousSeat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFD0000"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// delete passenger to database
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            if (cbChoosePassenger.SelectedItem != null)
            {
                clsPassengerManager.DeletePassenger(clsSelectedFlight.sFlightID, clsSelectedPassenger.sPassengerID);
                cbChoosePassenger.SelectedItem = null;
                bSelectedPassenger = false;
                lPassenger = clsPassengerManager.GetPassenger(clsSelectedFlight.sFlightID);
                cbChoosePassenger.ItemsSource = lPassenger;
                Label seatNum = LoadSeat(clsSelectedPassenger);
                seatNum.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0023FD"));

            }

        }


        /// <summary>
        /// selects seat
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void UserClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //select seat based on addpassenger and changeseat
                string passengerID;
                Label clickedLabel = (Label)sender;
                var LabelColor = (SolidColorBrush)clickedLabel.Background;
                if (bAddPassenger)
                {
                    if (LabelColor.Color.ToString() == "#FF0023FD")
                    {
                        clsSelectedPassenger = (clsPassenger)cbChoosePassenger.SelectedItem;
                        passengerID = clsPassengerManager.GetPassenger(sPassFirstName, sPassLastName);

                        clsPassengerManager.InsertLinkTable(clickedLabel.Content.ToString(), clsSelectedFlight.sFlightID.ToString(), clsPassengerManager.GetPassenger(sPassFirstName, sPassLastName));
                        //set the boxes and enable everthing
                        cbChoosePassenger.SelectedItem = clsSelectedPassenger;
                        
                        lblPassengersSeatNumber.Content = clickedLabel.Content;
                        gbColorKey.IsEnabled = true;
                        gbPassengerInformation.IsEnabled = true;
                        cmdAddPassenger.IsEnabled = true;
                        cmdChangeSeat.IsEnabled = true;
                        cmdDeletePassenger.IsEnabled = true;
                        lPassenger = clsPassengerManager.GetPassenger(clsSelectedFlight.sFlightID);
                        cbChoosePassenger.ItemsSource = lPassenger;
                        //set colors and find selected passenger
                        for (int i = 0; i < lPassenger.Count; i++)
                        {
                            Label seatNum = LoadSeat(lPassenger[i]);
                            seatNum.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFD0000"));
                        }
                        foreach (var item in cbChoosePassenger.Items)
                        {
                            var passenger = item as clsPassenger;
                            if (passenger != null && passengerID == passenger.sPassengerID)
                            {
                                cbChoosePassenger.SelectedItem = item;
                            }
                        }
                        clickedLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00FD00"));
                        previousPassengerSeat = clsSelectedPassenger;

                    }

                }
                else if (bChangeSeat)
                {
                    clsSelectedPassenger = (clsPassenger)cbChoosePassenger.SelectedItem;
                    //change passenger seat, and change color if blue

                    if (LabelColor.Color.ToString() == "#FF0023FD")
                    {
                        passengerID = clsSelectedPassenger.sPassengerID;
                        clsPassengerManager = new clsPassengerManager();
                        clsPassengerManager.ChangeSeat(clickedLabel.Content.ToString(), clsSelectedFlight.sFlightID, clsSelectedPassenger.sPassengerID);
                        lPassenger = clsPassengerManager.GetPassenger(clsSelectedFlight.sFlightID);
                        cbChoosePassenger.ItemsSource = lPassenger;
                        //set colors and find selected passenger
                        for (int i = 0; i < lPassenger.Count; i++)
                        {
                            Label seatNum = LoadSeat(lPassenger[i]);
                            seatNum.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFD0000"));
                        }
                        foreach (var item in cbChoosePassenger.Items)
                        {
                            var passenger = item as clsPassenger;
                            if (passenger != null && passengerID == passenger.sPassengerID)
                            {
                                cbChoosePassenger.SelectedItem = item;
                            }
                        }
                        cbChoosePassenger.SelectedItem = clsSelectedPassenger;
                        
                        lblPassengersSeatNumber.Content = clsSelectedPassenger.sSeatNumber;
                        gbColorKey.IsEnabled = true;
                        gbPassengerInformation.IsEnabled = true;
                        cmdAddPassenger.IsEnabled = true;
                        cmdChangeSeat.IsEnabled = true;
                        cmdDeletePassenger.IsEnabled = true;
                        clickedLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00FD00"));
                        
                    }
                    previousPassengerSeat = clsSelectedPassenger;

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// disable to select seat
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            if (bSelectedPassenger)
            {
                bAddPassenger = false;
                bChangeSeat = true;
                DisableAll();
            }


        }

        /// <summary>
        /// gets seat base on object
        /// </summary>
        /// <param lpassenger>object for passenger</param>
        private Label LoadSeat(clsPassenger lPassenger)
        {
            try
            {
                if (CanvasA380.Visibility == Visibility.Hidden)
                {

                    foreach (var control in c767_Seats.Children)
                    {
                        if (control is Label)
                        {
                            Label seatNum = control as Label;
                            if (seatNum.Name == "Seat" + lPassenger.sSeatNumber.ToString())
                            {
                                return seatNum;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var control in cA380_Seats.Children)
                    {
                        if (control is Label)
                        {
                            Label seatNum = control as Label;
                            if (seatNum.Name == "SeatA" + lPassenger.sSeatNumber.ToString())
                            {
                                return seatNum;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Disable all for selection
        /// </summary>
        /// <param lpassenger>object for passenger</param>
        private void DisableAll()
        {
            try
            { 
                gbPassengerInformation.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// errors display
        /// </summary>
        /// <param string sClass> class </param>
        /// <param string sMethod>method in question</param>
        /// <param string sMessage>message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }



    }
}
