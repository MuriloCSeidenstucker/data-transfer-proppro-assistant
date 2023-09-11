namespace PropProAssistant
{
    partial class Form_ProcessInfo
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
            this.TxtB_ProcessNumber = new System.Windows.Forms.TextBox();
            this.TxtB_ProcessObject = new System.Windows.Forms.TextBox();
            this.Gp_ProcessModality = new System.Windows.Forms.GroupBox();
            this.Rb_InPersonAuction = new System.Windows.Forms.RadioButton();
            this.Rb_ElectronicAuction = new System.Windows.Forms.RadioButton();
            this.Gp_ProcessNumber = new System.Windows.Forms.GroupBox();
            this.Gp_ProcessObject = new System.Windows.Forms.GroupBox();
            this.Gp_ProcessJudgmentsType = new System.Windows.Forms.GroupBox();
            this.Rb_BiggestDiscount = new System.Windows.Forms.RadioButton();
            this.Rb_LowestPrice = new System.Windows.Forms.RadioButton();
            this.Gp_ProcessCity = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Gp_ProcessState = new System.Windows.Forms.GroupBox();
            this.Cbx_ProcessState = new System.Windows.Forms.ComboBox();
            this.Gp_ProcessDateTime = new System.Windows.Forms.GroupBox();
            this.Dtp_ProcessDateTime = new System.Windows.Forms.DateTimePicker();
            this.Gp_ProcessPlatform = new System.Windows.Forms.GroupBox();
            this.Rb_Bll = new System.Windows.Forms.RadioButton();
            this.Rb_Licitanet = new System.Windows.Forms.RadioButton();
            this.Rb_Pcp = new System.Windows.Forms.RadioButton();
            this.Gp_ProcessModality.SuspendLayout();
            this.Gp_ProcessNumber.SuspendLayout();
            this.Gp_ProcessObject.SuspendLayout();
            this.Gp_ProcessJudgmentsType.SuspendLayout();
            this.Gp_ProcessCity.SuspendLayout();
            this.Gp_ProcessState.SuspendLayout();
            this.Gp_ProcessDateTime.SuspendLayout();
            this.Gp_ProcessPlatform.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtB_ProcessNumber
            // 
            this.TxtB_ProcessNumber.Location = new System.Drawing.Point(6, 19);
            this.TxtB_ProcessNumber.Name = "TxtB_ProcessNumber";
            this.TxtB_ProcessNumber.Size = new System.Drawing.Size(63, 20);
            this.TxtB_ProcessNumber.TabIndex = 9;
            this.TxtB_ProcessNumber.Text = "xx/xxxx";
            // 
            // TxtB_ProcessObject
            // 
            this.TxtB_ProcessObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtB_ProcessObject.Location = new System.Drawing.Point(6, 19);
            this.TxtB_ProcessObject.Multiline = true;
            this.TxtB_ProcessObject.Name = "TxtB_ProcessObject";
            this.TxtB_ProcessObject.Size = new System.Drawing.Size(764, 75);
            this.TxtB_ProcessObject.TabIndex = 10;
            this.TxtB_ProcessObject.Text = "Objeto . . .";
            // 
            // Gp_ProcessModality
            // 
            this.Gp_ProcessModality.Controls.Add(this.Rb_InPersonAuction);
            this.Gp_ProcessModality.Controls.Add(this.Rb_ElectronicAuction);
            this.Gp_ProcessModality.Location = new System.Drawing.Point(18, 175);
            this.Gp_ProcessModality.Name = "Gp_ProcessModality";
            this.Gp_ProcessModality.Size = new System.Drawing.Size(200, 68);
            this.Gp_ProcessModality.TabIndex = 5;
            this.Gp_ProcessModality.TabStop = false;
            this.Gp_ProcessModality.Text = "Modalidade";
            // 
            // Rb_InPersonAuction
            // 
            this.Rb_InPersonAuction.AutoSize = true;
            this.Rb_InPersonAuction.Location = new System.Drawing.Point(7, 43);
            this.Rb_InPersonAuction.Name = "Rb_InPersonAuction";
            this.Rb_InPersonAuction.Size = new System.Drawing.Size(111, 17);
            this.Rb_InPersonAuction.TabIndex = 1;
            this.Rb_InPersonAuction.TabStop = true;
            this.Rb_InPersonAuction.Text = "Pregão Presencial";
            this.Rb_InPersonAuction.UseVisualStyleBackColor = true;
            // 
            // Rb_ElectronicAuction
            // 
            this.Rb_ElectronicAuction.AutoSize = true;
            this.Rb_ElectronicAuction.Location = new System.Drawing.Point(7, 20);
            this.Rb_ElectronicAuction.Name = "Rb_ElectronicAuction";
            this.Rb_ElectronicAuction.Size = new System.Drawing.Size(109, 17);
            this.Rb_ElectronicAuction.TabIndex = 0;
            this.Rb_ElectronicAuction.TabStop = true;
            this.Rb_ElectronicAuction.Text = "Pregão Eletrônico";
            this.Rb_ElectronicAuction.UseVisualStyleBackColor = true;
            // 
            // Gp_ProcessNumber
            // 
            this.Gp_ProcessNumber.Controls.Add(this.TxtB_ProcessNumber);
            this.Gp_ProcessNumber.Location = new System.Drawing.Point(12, 12);
            this.Gp_ProcessNumber.Name = "Gp_ProcessNumber";
            this.Gp_ProcessNumber.Size = new System.Drawing.Size(80, 51);
            this.Gp_ProcessNumber.TabIndex = 0;
            this.Gp_ProcessNumber.TabStop = false;
            this.Gp_ProcessNumber.Text = "Número";
            // 
            // Gp_ProcessObject
            // 
            this.Gp_ProcessObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gp_ProcessObject.Controls.Add(this.TxtB_ProcessObject);
            this.Gp_ProcessObject.Location = new System.Drawing.Point(12, 69);
            this.Gp_ProcessObject.Name = "Gp_ProcessObject";
            this.Gp_ProcessObject.Size = new System.Drawing.Size(776, 100);
            this.Gp_ProcessObject.TabIndex = 4;
            this.Gp_ProcessObject.TabStop = false;
            this.Gp_ProcessObject.Text = "Objeto";
            // 
            // Gp_ProcessJudgmentsType
            // 
            this.Gp_ProcessJudgmentsType.Controls.Add(this.Rb_BiggestDiscount);
            this.Gp_ProcessJudgmentsType.Controls.Add(this.Rb_LowestPrice);
            this.Gp_ProcessJudgmentsType.Location = new System.Drawing.Point(228, 175);
            this.Gp_ProcessJudgmentsType.Name = "Gp_ProcessJudgmentsType";
            this.Gp_ProcessJudgmentsType.Size = new System.Drawing.Size(200, 65);
            this.Gp_ProcessJudgmentsType.TabIndex = 6;
            this.Gp_ProcessJudgmentsType.TabStop = false;
            this.Gp_ProcessJudgmentsType.Text = "Critérios de Julgamento";
            // 
            // Rb_BiggestDiscount
            // 
            this.Rb_BiggestDiscount.AutoSize = true;
            this.Rb_BiggestDiscount.Location = new System.Drawing.Point(7, 43);
            this.Rb_BiggestDiscount.Name = "Rb_BiggestDiscount";
            this.Rb_BiggestDiscount.Size = new System.Drawing.Size(100, 17);
            this.Rb_BiggestDiscount.TabIndex = 1;
            this.Rb_BiggestDiscount.TabStop = true;
            this.Rb_BiggestDiscount.Text = "Maior Desconto";
            this.Rb_BiggestDiscount.UseVisualStyleBackColor = true;
            // 
            // Rb_LowestPrice
            // 
            this.Rb_LowestPrice.AutoSize = true;
            this.Rb_LowestPrice.Location = new System.Drawing.Point(7, 20);
            this.Rb_LowestPrice.Name = "Rb_LowestPrice";
            this.Rb_LowestPrice.Size = new System.Drawing.Size(86, 17);
            this.Rb_LowestPrice.TabIndex = 0;
            this.Rb_LowestPrice.TabStop = true;
            this.Rb_LowestPrice.Text = "Menor Preço";
            this.Rb_LowestPrice.UseVisualStyleBackColor = true;
            // 
            // Gp_ProcessCity
            // 
            this.Gp_ProcessCity.Controls.Add(this.textBox1);
            this.Gp_ProcessCity.Location = new System.Drawing.Point(98, 14);
            this.Gp_ProcessCity.Name = "Gp_ProcessCity";
            this.Gp_ProcessCity.Size = new System.Drawing.Size(117, 49);
            this.Gp_ProcessCity.TabIndex = 1;
            this.Gp_ProcessCity.TabStop = false;
            this.Gp_ProcessCity.Text = "Cidade";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Nome da cidade . . .";
            // 
            // Gp_ProcessState
            // 
            this.Gp_ProcessState.Controls.Add(this.Cbx_ProcessState);
            this.Gp_ProcessState.Location = new System.Drawing.Point(221, 14);
            this.Gp_ProcessState.Name = "Gp_ProcessState";
            this.Gp_ProcessState.Size = new System.Drawing.Size(69, 49);
            this.Gp_ProcessState.TabIndex = 2;
            this.Gp_ProcessState.TabStop = false;
            this.Gp_ProcessState.Text = "Estado";
            // 
            // Cbx_ProcessState
            // 
            this.Cbx_ProcessState.FormattingEnabled = true;
            this.Cbx_ProcessState.Items.AddRange(new object[] {
            "GO",
            "MG"});
            this.Cbx_ProcessState.Location = new System.Drawing.Point(7, 20);
            this.Cbx_ProcessState.Name = "Cbx_ProcessState";
            this.Cbx_ProcessState.Size = new System.Drawing.Size(50, 21);
            this.Cbx_ProcessState.TabIndex = 0;
            // 
            // Gp_ProcessDateTime
            // 
            this.Gp_ProcessDateTime.Controls.Add(this.Dtp_ProcessDateTime);
            this.Gp_ProcessDateTime.Location = new System.Drawing.Point(296, 14);
            this.Gp_ProcessDateTime.Name = "Gp_ProcessDateTime";
            this.Gp_ProcessDateTime.Size = new System.Drawing.Size(268, 56);
            this.Gp_ProcessDateTime.TabIndex = 3;
            this.Gp_ProcessDateTime.TabStop = false;
            this.Gp_ProcessDateTime.Text = "Data/Hora da Abertura";
            // 
            // Dtp_ProcessDateTime
            // 
            this.Dtp_ProcessDateTime.Location = new System.Drawing.Point(7, 20);
            this.Dtp_ProcessDateTime.Name = "Dtp_ProcessDateTime";
            this.Dtp_ProcessDateTime.Size = new System.Drawing.Size(227, 20);
            this.Dtp_ProcessDateTime.TabIndex = 0;
            // 
            // Gp_ProcessPlatform
            // 
            this.Gp_ProcessPlatform.Controls.Add(this.Rb_Pcp);
            this.Gp_ProcessPlatform.Controls.Add(this.Rb_Licitanet);
            this.Gp_ProcessPlatform.Controls.Add(this.Rb_Bll);
            this.Gp_ProcessPlatform.Location = new System.Drawing.Point(434, 175);
            this.Gp_ProcessPlatform.Name = "Gp_ProcessPlatform";
            this.Gp_ProcessPlatform.Size = new System.Drawing.Size(200, 100);
            this.Gp_ProcessPlatform.TabIndex = 7;
            this.Gp_ProcessPlatform.TabStop = false;
            this.Gp_ProcessPlatform.Text = "Plataforma";
            // 
            // Rb_Bll
            // 
            this.Rb_Bll.AutoSize = true;
            this.Rb_Bll.Location = new System.Drawing.Point(7, 20);
            this.Rb_Bll.Name = "Rb_Bll";
            this.Rb_Bll.Size = new System.Drawing.Size(88, 17);
            this.Rb_Bll.TabIndex = 0;
            this.Rb_Bll.TabStop = true;
            this.Rb_Bll.Text = "BLL Compras";
            this.Rb_Bll.UseVisualStyleBackColor = true;
            // 
            // Rb_Licitanet
            // 
            this.Rb_Licitanet.AutoSize = true;
            this.Rb_Licitanet.Location = new System.Drawing.Point(7, 43);
            this.Rb_Licitanet.Name = "Rb_Licitanet";
            this.Rb_Licitanet.Size = new System.Drawing.Size(65, 17);
            this.Rb_Licitanet.TabIndex = 1;
            this.Rb_Licitanet.TabStop = true;
            this.Rb_Licitanet.Text = "Licitanet";
            this.Rb_Licitanet.UseVisualStyleBackColor = true;
            // 
            // Rb_Pcp
            // 
            this.Rb_Pcp.AutoSize = true;
            this.Rb_Pcp.Location = new System.Drawing.Point(7, 66);
            this.Rb_Pcp.Name = "Rb_Pcp";
            this.Rb_Pcp.Size = new System.Drawing.Size(154, 17);
            this.Rb_Pcp.TabIndex = 2;
            this.Rb_Pcp.TabStop = true;
            this.Rb_Pcp.Text = "Portal de Compras Públicas";
            this.Rb_Pcp.UseVisualStyleBackColor = true;
            // 
            // Form_ProcessInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Gp_ProcessPlatform);
            this.Controls.Add(this.Gp_ProcessDateTime);
            this.Controls.Add(this.Gp_ProcessState);
            this.Controls.Add(this.Gp_ProcessCity);
            this.Controls.Add(this.Gp_ProcessJudgmentsType);
            this.Controls.Add(this.Gp_ProcessObject);
            this.Controls.Add(this.Gp_ProcessNumber);
            this.Controls.Add(this.Gp_ProcessModality);
            this.Name = "Form_ProcessInfo";
            this.Text = "Form_ProcessInfo";
            this.Gp_ProcessModality.ResumeLayout(false);
            this.Gp_ProcessModality.PerformLayout();
            this.Gp_ProcessNumber.ResumeLayout(false);
            this.Gp_ProcessNumber.PerformLayout();
            this.Gp_ProcessObject.ResumeLayout(false);
            this.Gp_ProcessObject.PerformLayout();
            this.Gp_ProcessJudgmentsType.ResumeLayout(false);
            this.Gp_ProcessJudgmentsType.PerformLayout();
            this.Gp_ProcessCity.ResumeLayout(false);
            this.Gp_ProcessCity.PerformLayout();
            this.Gp_ProcessState.ResumeLayout(false);
            this.Gp_ProcessDateTime.ResumeLayout(false);
            this.Gp_ProcessPlatform.ResumeLayout(false);
            this.Gp_ProcessPlatform.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TxtB_ProcessNumber;
        private System.Windows.Forms.TextBox TxtB_ProcessObject;
        private System.Windows.Forms.GroupBox Gp_ProcessModality;
        private System.Windows.Forms.RadioButton Rb_ElectronicAuction;
        private System.Windows.Forms.RadioButton Rb_InPersonAuction;
        private System.Windows.Forms.GroupBox Gp_ProcessNumber;
        private System.Windows.Forms.GroupBox Gp_ProcessObject;
        private System.Windows.Forms.GroupBox Gp_ProcessJudgmentsType;
        private System.Windows.Forms.RadioButton Rb_BiggestDiscount;
        private System.Windows.Forms.RadioButton Rb_LowestPrice;
        private System.Windows.Forms.GroupBox Gp_ProcessCity;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox Gp_ProcessState;
        private System.Windows.Forms.ComboBox Cbx_ProcessState;
        private System.Windows.Forms.GroupBox Gp_ProcessDateTime;
        private System.Windows.Forms.DateTimePicker Dtp_ProcessDateTime;
        private System.Windows.Forms.GroupBox Gp_ProcessPlatform;
        private System.Windows.Forms.RadioButton Rb_Bll;
        private System.Windows.Forms.RadioButton Rb_Pcp;
        private System.Windows.Forms.RadioButton Rb_Licitanet;
    }
}