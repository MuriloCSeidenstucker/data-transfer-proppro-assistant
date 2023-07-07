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
            this.Btn_FileSelector = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_FileSelector
            // 
            this.Btn_FileSelector.Location = new System.Drawing.Point(293, 119);
            this.Btn_FileSelector.Name = "Btn_FileSelector";
            this.Btn_FileSelector.Size = new System.Drawing.Size(75, 23);
            this.Btn_FileSelector.TabIndex = 0;
            this.Btn_FileSelector.Text = "button1";
            this.Btn_FileSelector.UseVisualStyleBackColor = true;
            this.Btn_FileSelector.Click += new System.EventHandler(this.Btn_FileSelector_Click);
            // 
            // Form_PropTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_FileSelector);
            this.Name = "Form_PropTransfer";
            this.Text = "PropPro Assistant";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_FileSelector;
    }
}