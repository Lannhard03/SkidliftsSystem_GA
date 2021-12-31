
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
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClick = new System.Windows.Forms.Button();
            this.tempBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.liftDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.occupantsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempBindingSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(0, 3);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(590, 232);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.liftDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.occupantsDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tempBindingSourceBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(22, 241);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(568, 179);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(596, 12);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(75, 23);
            this.btnClick.TabIndex = 2;
            this.btnClick.Text = "Load Chart";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // tempBindingSourceBindingSource
            // 
            this.tempBindingSourceBindingSource.DataSource = typeof(InformationalChartsTool.TempBindingSource);
            // 
            // liftDataGridViewTextBoxColumn
            // 
            this.liftDataGridViewTextBoxColumn.DataPropertyName = "Lift";
            this.liftDataGridViewTextBoxColumn.HeaderText = "Lift";
            this.liftDataGridViewTextBoxColumn.Name = "liftDataGridViewTextBoxColumn";
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            // 
            // occupantsDataGridViewTextBoxColumn
            // 
            this.occupantsDataGridViewTextBoxColumn.DataPropertyName = "Occupants";
            this.occupantsDataGridViewTextBoxColumn.HeaderText = "Occupants";
            this.occupantsDataGridViewTextBoxColumn.Name = "occupantsDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(686, 450);
            this.Controls.Add(this.btnClick);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cartesianChart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charting Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempBindingSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.DataGridViewTextBoxColumn liftDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn occupantsDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource tempBindingSourceBindingSource;
    }
}

