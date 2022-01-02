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
            var Lifts = (from o in tempBindingSourceBindingSource.DataSource as List<TempBindingSource>
                         select new { Lift = o.Lift }).Distinct();
            foreach (var Lift in Lifts)
            {
                List<double> Occupantss = new List<double>();
                for (int Time = 0; Time <= 9; Time++)
                {
                    double Occupants = 0;
                    var data = from o in tempBindingSourceBindingSource.DataSource as List<TempBindingSource>
                               where o.Lift.Equals(Lift.Lift) && o.Time.Equals(Time)
                               orderby o.Time ascending
                               select new { o.Occupants, o.Time };
                    if (data.SingleOrDefault() != null)
                        Occupants = data.SingleOrDefault().Occupants;
                    Occupantss.Add(Occupants);
                }
                series.Add(new LineSeries() { Title = Lift.Lift.ToString(), Values = new ChartValues<double>(Occupantss) });
            }
            cartesianChart1.Series = series;
        }
    }
}
