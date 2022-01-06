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
        private void Form1_Load(object sender, EventArgs e)
        {
            chartWindow.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time",
                Labels = new[] {"09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"},
                Separator = new LiveCharts.Wpf.Separator
                {
                    
                    Step = Simulation.allLocations[0].timeBasedOccupantCounts.Count/1800, // Assuming all locations "timeBasedOccupantCounts" list share the same List size, which they do
                    IsEnabled = false
                }
            });
            chartWindow.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Occupants"
            });
            chartWindow.LegendLocation = LiveCharts.LegendLocation.Right;
        }

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
        private void LoadQueues(object sender, EventArgs e)
        {
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

                foreach (Location l in Simulation.allLocations.Where(x=> x is LiftQueue))
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

        private void LoadByLocation(object sender, EventArgs e)
        {
            chartWindow.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (Type type in Assembly.GetAssembly(typeof(Location)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Location))))
            {
                int[] occupants = new int[Simulation.allLocations[0].timeBasedOccupantCounts.Count]; //this assumes all locations have equal list size

                foreach (Location l in Simulation.allLocations)
                {
                    if (l.GetType() == type)
                    {
                        for (int i = 0; i < l.timeBasedOccupantCounts.Count; i++)
                        {
                            occupants[i] += l.timeBasedOccupantCounts[i];
                        }
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

    }
}
