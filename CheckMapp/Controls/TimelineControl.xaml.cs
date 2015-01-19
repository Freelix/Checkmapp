using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CheckMapp.Model.Tables;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Globalization;

namespace CheckMapp.Controls
{
    public partial class TimelineControl : UserControl
    {
        public Canvas canvas;
        public Rectangle mainRectangle;

        public static readonly DependencyProperty TripsProperty =
     DependencyProperty.Register("EventsList", typeof(List<Trip>), typeof(TimelineControl), null);

        public TimelineControl()
        {
            InitializeComponent();
            Country country = new Country();
            country.Name = "Canada";

            Country country1 = new Country();
            country1.Name = "United States";

            Country country2 = new Country();
            country2.Name = "Gabon";
            Trips = new List<Trip>();
            Trips.Add(new Trip { Name="Test1", BeginDate = DateTime.Now, EndDate = DateTime.Now });
            Trips.Add(new Trip { Name = "Test2", BeginDate = DateTime.Now.AddMonths(-2), EndDate = DateTime.Now });
            Trips.Add(new Trip { Name = "Test3", BeginDate = DateTime.Now.AddMonths(-3).AddDays(10), EndDate = DateTime.Now });
            Trips.Add(new Trip { Name = "Test4", BeginDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now });
            Trips.Add(new Trip { Name = "Test5", BeginDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now });
            Trips.Add(new Trip { Name = "Test6", BeginDate = DateTime.Now.AddYears(-3).AddDays(20).AddMonths(-1), EndDate = DateTime.Now });

            AdjustTimeLine();
        }

        /// <summary>
        /// Liste des évènements de l'utilisateur
        /// </summary>
        public List<Trip> Trips
        {
            get { return base.GetValue(TripsProperty) as List<Trip>; }
            set { base.SetValue(TripsProperty, value); }
        }

        /// <summary>
        /// Type de date (mois, année)
        /// </summary>
        public enum TypeDate
        {
            Mois,
            Annee,
        };

        /// <summary>
        /// Crée les bordure pour les années et les mois
        /// </summary>
        /// <param name="date"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Border CreateBorder(TypeDate date, string text)
        {
            Border border = new Border();
            border.Background = new SolidColorBrush(Colors.Black);
            border.BorderBrush = new SolidColorBrush(Colors.White);
            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            textBlock.Foreground = new SolidColorBrush(Colors.White);
            textBlock.Text = text;
            border.Child = textBlock;
            border.Width = date == TypeDate.Annee ? 140 : 100;
            border.Height = date == TypeDate.Annee ? 80 : 60;
            border.BorderThickness = new Thickness(date == TypeDate.Annee ? 6 : 4);
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBlock.FontSize = date == TypeDate.Annee ? 28 : 22;
            canvas.Children.Add(border);
            return border;
        }

        /// <summary>
        /// Obtient le décalage de l'élément pour être au milieu
        /// </summary>
        /// <returns></returns>
        public double MiddleLeft(FrameworkElement canvas, FrameworkElement targetElement)
        {
            return (canvas.Width - targetElement.Width) / 2;
        }

        /// <summary>
        /// Crée le visuel de la ligne du temps selon la liste des events
        /// </summary>
        public void AdjustTimeLine()
        {
            canvas = new Canvas();
            canvas.Width = 400;
            canvas.Opacity = 0.9;

            mainRectangle = new Rectangle();
            mainRectangle.Width = 10;
            mainRectangle.Height = 0;
            mainRectangle.Fill = new SolidColorBrush(Colors.White);

            canvas.Children.Add(mainRectangle);

            Canvas.SetLeft(mainRectangle, 0);

            //Obtient la liste des années
            var yearList = Trips.Select(item => new List<string>() { item.BeginDate.Year.ToString(), item.EndDate.Value.Year.ToString() })
                .SelectMany(group => group).Distinct();

            double previousBorderTop = 0;
            foreach (string year in yearList.OrderByDescending(x => x))
            {
                Border borderYear = CreateBorder(TypeDate.Annee, year);
                Canvas.SetTop(borderYear, previousBorderTop == 0 ? 0 : previousBorderTop + 250);
                previousBorderTop = Canvas.GetTop(borderYear);

                bool firstMonth = true;

                //On obtient la liste des mois selon l'année
                var monthList = Trips.Where(x => x.BeginDate.Year.ToString() == year)
                    .Select(item => new List<int>() { item.BeginDate.Month/*, item.EndDate.Month*/ })
                    .SelectMany(group => group).Distinct().OrderBy(x => x);

                //TODO : implanter base de données
                foreach (int month in monthList)
                {
                    DateTimeFormatInfo info = new DateTimeFormatInfo();
                    string monthStr = info.GetAbbreviatedMonthName(month);
                    Border borderMonth = CreateBorder(TypeDate.Mois, monthStr.Substring(0, 3));
                    //Si c'est le premier mois, alors plus près du border année
                    if (firstMonth)
                        Canvas.SetTop(borderMonth, Canvas.GetTop(borderYear) + borderYear.Height + 10);
                    else
                        Canvas.SetTop(borderMonth, previousBorderTop + 250);

                    firstMonth = false;
                    previousBorderTop = Canvas.GetTop(borderMonth);

                    foreach (Trip activites in Trips.Where(x => x.BeginDate.Month == month && x.BeginDate.Year.ToString() == year))
                    {
                        //Crée le cercle indiquant la position dans le mois¸¸
                        Ellipse ellipse = new Ellipse();
                        ellipse.Fill = new SolidColorBrush(Colors.White);
                        ellipse.Height = mainRectangle.Width + 10;
                        ellipse.Width = mainRectangle.Width + 10;
                        Canvas.SetTop(ellipse, Canvas.GetTop(borderMonth) + borderMonth.Height + (activites.BeginDate.Day * 3));
                        Canvas.SetLeft(ellipse, -5);

                        //Le rectangle relié au control de détail des éléments
                        Rectangle rect = new Rectangle();
                        rect.Width = 120;
                        rect.Height = 7;
                        rect.Fill = new SolidColorBrush(Colors.White);
                        Canvas.SetTop(rect, Canvas.GetTop(ellipse) + rect.Height);
                        double leftRecta = MiddleLeft(canvas, rect);
                        Canvas.SetLeft(rect, 0);

                        //Le contrôle affichant les détails d'un event
                        TimelineElementControl control = new TimelineElementControl(rect.Fill);
                        control.Tap += control_Tap;
                        control.Trip = activites;
                        canvas.Children.Add(control);
                        Canvas.SetTop(control, Canvas.GetTop(rect) - control.Height / 2);
                        Canvas.SetLeft(control, rect.Width);

                        canvas.Children.Add(rect);
                        canvas.Children.Add(ellipse);
                    }
                }

            }

            mainRectangle.Height = previousBorderTop + 200;
            canvas.Height = mainRectangle.Height + 100;
            LayoutRoot.Children.Add(canvas);
        }

        public event EventHandler UserControlElementTap;

        private void OnUserControlElementTap(Trip trip)
        {
            if (UserControlElementTap != null)
            {
                UserControlElementTap(trip, EventArgs.Empty);
            }
        }

        public void control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            OnUserControlElementTap((sender as TimelineElementControl).Trip);
        }

    }
}
