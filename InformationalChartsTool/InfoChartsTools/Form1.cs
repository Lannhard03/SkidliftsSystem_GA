using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace InformationalChartsTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //setup Graphing/charting environment
        private void Form1_Load(object sender, EventArgs e)
        {
            //Labels have to be placed with equal spacing, since stepsized of seperators is fixed.

            //recalculate regionlength
            int endTime = 32400;
            int regionLength = endTime / Simulation.allLocations[1].timeBasedOccupantCounts.Count;

            int s = Simulation.allLocations[1].timeBasedOccupantCounts.Count / 9; //Rounds down whihc is nessecary due to equal spacing requirment
            List<string> labels = new List<string>();

            //Determine Labes to be placed in time direction.
            for (int i = 0; i <= endTime; i += regionLength)
            {
                //convert "timestamp" to AB:CD:EF format, keeping in mind that time begins at 9:00:00
                int hour = 9 + i / 3600;
                int minute = (i - 3600 * (hour - 9)) / 60;
                int second = i - 3600 * (hour - 9) - 60 * minute;
                string time = string.Format("{0}:{1}:{2}", hour, minute, second);

                labels.Add(time);
            }

            //setup Axies of graph
            chartWindow.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time",
                Labels = labels,
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = s > 1 ? s : 1, // Assumes all locations "timeBasedOccupantCounts" list share the same List size, which they do
                    IsEnabled = true
                }
            });
            chartWindow.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Occupants"
            });
            chartWindow.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        //Graphs Person count of Lifts
        private void LoadLifts(object sender, EventArgs e)
        {
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (Lift l in Simulation.allLocations.Where(x => x is Lift))
            {
                series.Add(new LineSeries()
                {
                    Title = l.name,
                    Values = new ChartValues<int>(l.timeBasedOccupantCounts),
                    PointGeometrySize = 6.9
                });

            }
            chartWindow.Series = series;
        }

        //Graphs Person count of Slopes
        private void LoadSlopes(object sender, EventArgs e)
        {
            //Initializing data
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (Slope l in Simulation.allLocations.Where(x => x is Slope))
            {
                series.Add(new LineSeries()
                {
                    Title = l.name,
                    Values = new ChartValues<int>(l.timeBasedOccupantCounts),
                    PointGeometrySize = 6.9
                });

            }
            chartWindow.Series = series;
        }

        //Graphs Person count of all non-Liftqueue, non-Slope, and non-Lift locations
        private void LoadStatics(object sender, EventArgs e)
        {
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (Location l in Simulation.allLocations.Where(x => !(x is Slope) && !(x is Lift) && !(x is LiftQueue)))
            {
                series.Add(new LineSeries()
                {
                    Title = l.name,
                    Values = new ChartValues<int>(l.timeBasedOccupantCounts),
                    PointGeometrySize = 6.9
                });

            }
            chartWindow.Series = series;
        }

        //Graphs Person count of LiftQueues
        private void LoadQueues(object sender, EventArgs e)
        {
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (Location l in Simulation.allLocations.Where(x => x is LiftQueue))
            {
                series.Add(new LineSeries()
                {
                    Title = l.name,
                    Values = new ChartValues<int>(l.timeBasedOccupantCounts),
                    PointGeometrySize = 6.9
                });
            }
            chartWindow.Series = series;
        }

        //Uses reflections to find all subclasses of Location and graphs the total Person count of each subclass.
        private void LoadByLocation(object sender, EventArgs e)
        {
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            //Loops through each subclass, then each location of that type, summing their occupants.
            foreach (Type type in Assembly.GetAssembly(typeof(Location)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Location))))
            {
                int[] occupants = new int[Simulation.allLocations[0].timeBasedOccupantCounts.Count]; //this assumes all locations have equal list size

                foreach (Location l in Simulation.allLocations.Where(x => x.GetType() == type))
                {
                    for (int i = 0; i < l.timeBasedOccupantCounts.Count; i++)
                    {
                        occupants[i] += l.timeBasedOccupantCounts[i];
                    }
                }
                series.Add(new LineSeries()
                {
                    Title = type.Name,
                    Values = new ChartValues<int>(occupants),
                    PointGeometrySize = 6.9
                });
            }

            chartWindow.Series = series;
        }

        //Simply calls PrintConsoleData method.
        private void button4_Click(object sender, EventArgs e)
        {
            Simulation.PrintConsoleData();
        }
    }
}
