using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Informational_Charts_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            inDataBindingSource.DataSource = new List<InData>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time(hours)",
                Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8" }
            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Occupants",
                LabelFormatter = occupants => occupants.ToString("C")
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            //Initializes data
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var lifts = (from o in inDataBindingSource.DataSource as List<InData>
                        select new { Lift = o.Lift }).Distinct();
            foreach (var lift in lifts)
            {
                //List<>
            }
        }
    }
}
