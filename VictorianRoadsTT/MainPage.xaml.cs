using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VictorianRoadsTT.Resources;
using System.IO;
using System.Text;

namespace VictorianRoadsTT
{
    public partial class MainPage : PhoneApplicationPage
    {
        private String webResult { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            readHtmlPage("http://traffic.vicroads.vic.gov.au/getRecords.asp");
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public String readHtmlPage(string url)
        {
            //setup some variables end


            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                objRequest.BeginGetRequestStream(new AsyncCallback(ReadCallback), objRequest);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";
        }

        private static void ResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest objRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.EndGetResponse(asynchronousResult);

            using (StreamReader sr =
               new StreamReader(objResponse.GetResponseStream()))
            {
                string webResult = sr.ReadToEnd();

                // Close and clean up the StreamReader
                sr.Close();
            }
            objResponse.Close();
        }


        private static void ReadCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            Stream postStream = request.EndGetRequestStream(asynchronousResult);
            //setup some variables

            String requestStr = "SQL_4";
            String functionStr = "TableFrame.fillTravelTimes()";
            String category = "5";
            String currentTime = "01/01/1970 12:12:12 AM";

            String strPost = "requestStr=" + requestStr+ "&functionStr=" + functionStr+ "&category=" + category+ "&currentTIme=" + currentTime;

            request.ContentLength = strPost.Length;

            // Convert the sttring into a byte array
            byte[] byteArray = Encoding.UTF8.GetBytes(strPost);
            postStream.Write(byteArray, 0, strPost.Length);
            postStream.Close();
//            allDone.Set();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}