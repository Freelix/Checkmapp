using System;
using System.Runtime.Serialization;

namespace CheckMapp.Model.Utils
{
    [DataContract]
    public class TripLocalisation
    {
        [DataMember]
        private double longitude;
        [DataMember]
        private double latitude;
        [DataMember]
        private Position position;

        public enum Position
        {
            None,
            Departure,
            Destination
        };

        public TripLocalisation()
        {
            longitude = 0.0;
            latitude = 0.0;
            position = Position.None;
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public Position GetPosition()
        {
            return position;
        }

        public void SetPosition(Position position)
        {
            this.position = position;
        }
    }
}
