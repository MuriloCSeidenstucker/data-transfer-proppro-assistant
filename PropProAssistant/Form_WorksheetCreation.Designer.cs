namespace PropProAssistant
{
    partial class Form_WorksheetCreation
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
            this.Btn_CreateWorksheet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_CreateWorksheet
            // 
            this.Btn_CreateWorksheet.Location = new System.Drawing.Point(354, 182);
            this.Btn_CreateWorksheet.Name = "Btn_CreateWorksheet";
            this.Btn_CreateWorksheet.Size = new System.Drawing.Size(75, 23);
            this.Btn_CreateWorksheet.TabIndex = 0;
            this.Btn_CreateWorksheet.Text = "button1";
            this.Btn_CreateWorksheet.UseVisualStyleBackColor = true;
            this.Btn_CreateWorksheet.Click += new System.EventHandler(this.Btn_CreateWorksheet_Click);
            // 
            // Form_WorksheetCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_CreateWorksheet);
            this.Name = "Form_WorksheetCreation";
            this.Text = "Form_WorksheetCreation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_CreateWorksheet;
    }
}