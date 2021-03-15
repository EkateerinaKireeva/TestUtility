namespace WeatherClientApp
{
    partial class WeatherCityForm
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
            this.cityNameLabel = new System.Windows.Forms.Label();
            this.weatherList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cityNameLabel
            // 
            this.cityNameLabel.AutoSize = true;
            this.cityNameLabel.Location = new System.Drawing.Point(375, 39);
            this.cityNameLabel.Name = "cityNameLabel";
            this.cityNameLabel.Size = new System.Drawing.Size(101, 17);
            this.cityNameLabel.TabIndex = 0;
            this.cityNameLabel.Text = "cityNameLabel";
            // 
            // weatherList
            // 
            this.weatherList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.weatherList.FormattingEnabled = true;
            this.weatherList.ItemHeight = 16;
            this.weatherList.Location = new System.Drawing.Point(12, 101);
            this.weatherList.Name = "weatherList";
            this.weatherList.Size = new System.Drawing.Size(764, 324);
            this.weatherList.TabIndex = 1;
            // 
            // WeatherCityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.weatherList);
            this.Controls.Add(this.cityNameLabel);
            this.Name = "WeatherCityForm";
            this.Text = "WeatherCityForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cityNameLabel;
        private System.Windows.Forms.ListBox weatherList;
    }
}