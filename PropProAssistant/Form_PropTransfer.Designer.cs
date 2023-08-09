namespace PropProAssistant
{
    partial class Form_PropTransfer
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
            this.Btn_ModelWorksheetSelector = new System.Windows.Forms.Button();
            this.Btn_DataTransfer = new System.Windows.Forms.Button();
            this.Btn_PriceBidWorksheetSelector = new System.Windows.Forms.Button();
            this.Cbx_WorksheetModels = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Btn_ModelWorksheetSelector
            // 
            this.Btn_ModelWorksheetSelector.Location = new System.Drawing.Point(0, 29);
            this.Btn_ModelWorksheetSelector.Name = "Btn_ModelWorksheetSelector";
            this.Btn_ModelWorksheetSelector.Size = new System.Drawing.Size(75, 23);
            this.Btn_ModelWorksheetSelector.TabIndex = 1;
            this.Btn_ModelWorksheetSelector.Text = "button1";
            this.Btn_ModelWorksheetSelector.UseVisualStyleBackColor = true;
            this.Btn_ModelWorksheetSelector.Click += new System.EventHandler(this.Btn_ModelWorksheetSelector_Click);
            // 
            // Btn_DataTransfer
            // 
            this.Btn_DataTransfer.Location = new System.Drawing.Point(0, 58);
            this.Btn_DataTransfer.Name = "Btn_DataTransfer";
            this.Btn_DataTransfer.Size = new System.Drawing.Size(75, 23);
            this.Btn_DataTransfer.TabIndex = 2;
            this.Btn_DataTransfer.Text = "button1";
            this.Btn_DataTransfer.UseVisualStyleBackColor = true;
            this.Btn_DataTransfer.Click += new System.EventHandler(this.Btn_DataTransfer_Click);
            // 
            // Btn_PriceBidWorksheetSelector
            // 
            this.Btn_PriceBidWorksheetSelector.Location = new System.Drawing.Point(0, 0);
            this.Btn_PriceBidWorksheetSelector.Name = "Btn_PriceBidWorksheetSelector";
            this.Btn_PriceBidWorksheetSelector.Size = new System.Drawing.Size(75, 23);
            this.Btn_PriceBidWorksheetSelector.TabIndex = 3;
            this.Btn_PriceBidWorksheetSelector.Text = "button1";
            this.Btn_PriceBidWorksheetSelector.UseVisualStyleBackColor = true;
            this.Btn_PriceBidWorksheetSelector.Click += new System.EventHandler(this.Btn_PriceBidWorksheetSelector_Click);
            // 
            // Cbx_WorksheetModels
            // 
            this.Cbx_WorksheetModels.FormattingEnabled = true;
            this.Cbx_WorksheetModels.Location = new System.Drawing.Point(0, 87);
            this.Cbx_WorksheetModels.Name = "Cbx_WorksheetModels";
            this.Cbx_WorksheetModels.Size = new System.Drawing.Size(121, 21);
            this.Cbx_WorksheetModels.TabIndex = 4;
            // 
            // Form_PropTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 357);
            this.Controls.Add(this.Cbx_WorksheetModels);
            this.Controls.Add(this.Btn_PriceBidWorksheetSelector);
            this.Controls.Add(this.Btn_DataTransfer);
            this.Controls.Add(this.Btn_ModelWorksheetSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form_PropTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PropPro Assistant";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_ModelWorksheetSelector;
        private System.Windows.Forms.Button Btn_DataTransfer;
        private System.Windows.Forms.Button Btn_PriceBidWorksheetSelector;
        private System.Windows.Forms.ComboBox Cbx_WorksheetModels;
    }
}