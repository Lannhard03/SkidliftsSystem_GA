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
            tempBindingSourceBindingSource.DataSource = new List<TempBindingSource>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time"
            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Occupants"
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            //Initializing data
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            foreach(Location l in Simulate.allLocations)
            {
                List<double> Occupantss = new List<double>();
                foreach (int personCount in l.timeBasedOccupantCounts)
                {
                    Occupantss.Add(personCount);
                }
                series.Add(new LineSeries() { Title = l.name, Values = new ChartValues<double>(Occupantss) });
            }
            cartesianChart1.Series = series;
        }
    }
}
