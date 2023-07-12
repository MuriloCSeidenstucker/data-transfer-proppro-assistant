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
            this.Btn_MainSheetSelector = new System.Windows.Forms.Button();
            this.Btn_ModelSheetSelector = new System.Windows.Forms.Button();
            this.Btn_DataTransfer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_MainSheetSelector
            // 
            this.Btn_MainSheetSelector.Location = new System.Drawing.Point(84, 63);
            this.Btn_MainSheetSelector.Name = "Btn_MainSheetSelector";
            this.Btn_MainSheetSelector.Size = new System.Drawing.Size(75, 23);
            this.Btn_MainSheetSelector.TabIndex = 0;
            this.Btn_MainSheetSelector.Text = "button1";
            this.Btn_MainSheetSelector.UseVisualStyleBackColor = true;
            this.Btn_MainSheetSelector.Click += new System.EventHandler(this.Btn_FileSelector_Click);
            // 
            // Btn_ModelSheetSelector
            // 
            this.Btn_ModelSheetSelector.Location = new System.Drawing.Point(435, 63);
            this.Btn_ModelSheetSelector.Name = "Btn_ModelSheetSelector";
            this.Btn_ModelSheetSelector.Size = new System.Drawing.Size(75, 23);
            this.Btn_ModelSheetSelector.TabIndex = 1;
            this.Btn_ModelSheetSelector.Text = "button1";
            this.Btn_ModelSheetSelector.UseVisualStyleBackColor = true;
            this.Btn_ModelSheetSelector.Click += new System.EventHandler(this.Btn_ModelSheetSelector_Click);
            // 
            // Btn_DataTransfer
            // 
            this.Btn_DataTransfer.Location = new System.Drawing.Point(267, 246);
            this.Btn_DataTransfer.Name = "Btn_DataTransfer";
            this.Btn_DataTransfer.Size = new System.Drawing.Size(75, 23);
            this.Btn_DataTransfer.TabIndex = 2;
            this.Btn_DataTransfer.Text = "button1";
            this.Btn_DataTransfer.UseVisualStyleBackColor = true;
            this.Btn_DataTransfer.Click += new System.EventHandler(this.Btn_DataTransfer_Click);
            // 
            // Form_PropTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_DataTransfer);
            this.Controls.Add(this.Btn_ModelSheetSelector);
            this.Controls.Add(this.Btn_MainSheetSelector);
            this.Name = "Form_PropTransfer";
            this.Text = "PropPro Assistant";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_MainSheetSelector;
        private System.Windows.Forms.Button Btn_ModelSheetSelector;
        private System.Windows.Forms.Button Btn_DataTransfer;
    }
}