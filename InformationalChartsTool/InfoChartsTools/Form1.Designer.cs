
namespace InformationalChartsTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chartWindow = new LiveCharts.WinForms.CartesianChart();
            this.tempBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClick = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tempBindingSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chartWindow
            // 
            this.chartWindow.Location = new System.Drawing.Point(0, 0);
            this.chartWindow.Name = "chartWindow";
            this.chartWindow.Size = new System.Drawing.Size(1194, 528);
            this.chartWindow.TabIndex = 0;
            this.chartWindow.Text = "cartesianChart1";
            // 
            // tempBindingSourceBindingSource
            // 
            this.tempBindingSourceBindingSource.DataSource = typeof(InformationalChartsTool.TempBindingSource);
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(1186, 12);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(118, 23);
            this.btnClick.TabIndex = 2;
            this.btnClick.Text = "Load Lift Graphs";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.LoadLifts);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1186, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Load Slope Graphs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadSlopes);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1186, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Load Statics Graphs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.LoadStatics);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1316, 594);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClick);
            this.Controls.Add(this.chartWindow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charting Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tempBindingSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart chartWindow;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.BindingSource tempBindingSourceBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

