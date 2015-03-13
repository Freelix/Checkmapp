using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows;
using System.Device.Location;

namespace CheckMapp.Utils
{
    public static class Utility
    {

        #region Images Functions

        public static BitmapImage ByteArrayToImage(byte[] imageByteArray, bool compress)
        {
            BitmapImage img = new BitmapImage();
            if (compress)
            {
                img.DecodePixelHeight = 100;
                img.DecodePixelType = DecodePixelType.Logical;
                img.CreateOptions = BitmapCreateOptions.DelayCreation;
            }
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.SetSource(memStream);
            }

            return img;
        }

        public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            byte[] data = null;
            using (MemoryStream stream = new MemoryStream())
            {
                WriteableBitmap wBitmap = new WriteableBitmap(bitmapImage);
                wBitmap.SaveJpeg(stream, wBitmap.PixelWidth, wBitmap.PixelHeight, 0, 100);
                stream.Seek(0, SeekOrigin.Begin);
                data = stream.GetBuffer();
            }
            return data;
        }

        #endregion

        #region Stream Functions

        /// <summary>
        /// Transforme le stream d'une photo en tableau de byte
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            if (input == null)
                return null;

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion

        #region Internet connection Functions
        /// <summary>
        /// Vérifie qu'il y est une connexion active
        /// </summary>
        /// <param name=></param>
        /// <returns>true si connecté à un réseaux</returns>
        public static bool checkNetworkConnection()
        {
            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        #endregion

        #region Map
        public static async Task AddLocation(Microsoft.Phone.Maps.Controls.Map myMap, Microsoft.Phone.Controls.PhoneTextBox myTextBox, System.Windows.Input.GestureEventArgs e, double latitude, double longitude, bool completeAddress = false, string pinContent = "")
        {
            ReverseGeocodeQuery query;
            List<MapLocation> mapLocations;
            string pushpinContent = "";
            MapLocation mapLocation;

            query = new ReverseGeocodeQuery();
            if (e != null)
                query.GeoCoordinate = myMap.ConvertViewportPointToGeoCoordinate(e.GetPosition(myMap));
            else
                query.GeoCoordinate = new GeoCoordinate(latitude, longitude);

            mapLocations = (List<MapLocation>)await query.GetMapLocationsAsync();
            mapLocation = mapLocations.FirstOrDefault();

            if (mapLocation != null)
            {
                MapLayer pinLayout = new MapLayer();
                Pushpin MyPushpin = new Pushpin();
                MapOverlay pinOverlay = new MapOverlay();
                if (myMap.Layers.Count > 0)
                {
                    myMap.Layers.RemoveAt(myMap.Layers.Count - 1);
                }

                myMap.Layers.Add(pinLayout);

                MyPushpin.GeoCoordinate = mapLocation.GeoCoordinate;


                pinOverlay.Content = MyPushpin;
                pinOverlay.GeoCoordinate = mapLocation.GeoCoordinate;
                pinOverlay.PositionOrigin = new Point(0, 1);
                pinLayout.Add(pinOverlay);

                if (!completeAddress)
                    pushpinContent = getAddress(mapLocation);
                else
                    pushpinContent = getCompleteAddress(mapLocation);

                if (!string.IsNullOrEmpty(pinContent))
                    pushpinContent = pinContent;

                MyPushpin.Content = pushpinContent.Trim();
                if (myTextBox != null)
                    myTextBox.Text = MyPushpin.Content.ToString();
            }
        }

        private static string getAddress(MapLocation mapLocation)
        {
            string Address = "";
            string region = MapHelper.getRegion(mapLocation);
            string city = mapLocation.Information.Address.City;
            string country = mapLocation.Information.Address.Country;

            if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                Address = "";
            else if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(city))
                Address = country;
            else if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(country))
                Address = city;
            else if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                Address = region;
            else if (string.IsNullOrEmpty(region))
                Address = string.Format("{0}, {1} ", city, country);
            else if (string.IsNullOrEmpty(city))
                Address = string.Format("{0}, {1} ", region, country);
            else if (string.IsNullOrEmpty(country))
                Address = string.Format("{0}, {1} ", city, region);
            else
                Address = string.Format("{0}, {1}, {2} ", city, region, country);

            return Address;
        }

        private static string getCompleteAddress(MapLocation mapLocation)
        {
            string houseNumber = mapLocation.Information.Address.HouseNumber;
            string street = mapLocation.Information.Address.Street;
            string Address = "";
            string region = MapHelper.getRegion(mapLocation);
            string city = mapLocation.Information.Address.City;
            string country = mapLocation.Information.Address.Country;

            if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                Address = "";
            else if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(city))
                Address = country;
            else if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(country))
                Address = city;
            else if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                Address = region;
            else if (string.IsNullOrEmpty(region))
                Address = string.Format("{0}, {1} ", city, country);
            else if (string.IsNullOrEmpty(city))
                Address = string.Format("{0}, {1} ", region, country);
            else if (string.IsNullOrEmpty(country))
                Address = string.Format("{0}, {1} ", city, region);
            else if (string.IsNullOrEmpty(houseNumber) && string.IsNullOrEmpty(street))
                Address = string.Format("{0}, {1}, {2} ", city, region, country);
            else if (string.IsNullOrEmpty(houseNumber))
                Address = string.Format("{0}, {1}, {2}, {3}", street, city, region, country);
            else if (string.IsNullOrEmpty(street))
                Address = string.Format("{0}, {1}, {2}, {3}", houseNumber, city, region, country);
            else
                Address = string.Format("{0}, {1}, {2}, {3}, {4}", houseNumber, street, city, region, country);

            return Address;
        }
        
        #endregion

        #region XML

        public static string GetAppVersion()
        {
            var xmlReaderSettings = new XmlReaderSettings
            {
                XmlResolver = new XmlXapResolver()
            };

            using (var xmlReader = XmlReader.Create("WMAppManifest.xml", xmlReaderSettings))
            {
                xmlReader.ReadToDescendant("App");

                return xmlReader.GetAttribute("Version");
            }
        }

        #endregion

        #region friend list

        public static List<string> FriendToList(string friends)
        {
            if (String.IsNullOrEmpty(friends))
            {
                return new List<string>();
            }
            return friends.Split(',').Where(x => !String.IsNullOrEmpty(x)).ToList();
        }

        public static string FriendToString(List<string> friends)
        {
            if (friends == null)
                return string.Empty;

            string friendString = null;
            foreach (string friend in friends)
            {
                friendString += friend + ",";
            }

            return friendString;
        }

        #endregion friend list
    }
}
