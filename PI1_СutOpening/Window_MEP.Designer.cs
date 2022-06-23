
namespace PI1_СutOpening
{
    partial class Window_MEP
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
            this.lblFamilyForWall = new System.Windows.Forms.Label();
            this.lblFamilyForFloor = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lblLikedFile = new System.Windows.Forms.Label();
            this.cmbFamilyForWall = new System.Windows.Forms.ComboBox();
            this.cmbFamilyForFloor = new System.Windows.Forms.ComboBox();
            this.txtbOffset = new System.Windows.Forms.TextBox();
            this.cmbLinkedFile = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFamilyForWall
            // 
            this.lblFamilyForWall.AutoSize = true;
            this.lblFamilyForWall.Location = new System.Drawing.Point(14, 14);
            this.lblFamilyForWall.Margin = new System.Windows.Forms.Padding(5);
            this.lblFamilyForWall.Name = "lblFamilyForWall";
            this.lblFamilyForWall.Size = new System.Drawing.Size(169, 13);
            this.lblFamilyForWall.TabIndex = 0;
            this.lblFamilyForWall.Text = "Семейство заглушки для стены";
            // 
            // lblFamilyForFloor
            // 
            this.lblFamilyForFloor.AutoSize = true;
            this.lblFamilyForFloor.Location = new System.Drawing.Point(14, 68);
            this.lblFamilyForFloor.Margin = new System.Windows.Forms.Padding(5);
            this.lblFamilyForFloor.Name = "lblFamilyForFloor";
            this.lblFamilyForFloor.Size = new System.Drawing.Size(162, 13);
            this.lblFamilyForFloor.TabIndex = 1;
            this.lblFamilyForFloor.Text = "Семейство заглушки для пола";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(14, 122);
            this.lblOffset.Margin = new System.Windows.Forms.Padding(5);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(122, 13);
            this.lblOffset.TabIndex = 2;
            this.lblOffset.Text = "Величина зазора в мм";
            // 
            // lblLikedFile
            // 
            this.lblLikedFile.AutoSize = true;
            this.lblLikedFile.Location = new System.Drawing.Point(14, 175);
            this.lblLikedFile.Margin = new System.Windows.Forms.Padding(5);
            this.lblLikedFile.Name = "lblLikedFile";
            this.lblLikedFile.Size = new System.Drawing.Size(177, 13);
            this.lblLikedFile.TabIndex = 3;
            this.lblLikedFile.Text = "Файл ограждающих конструкций";
            // 
            // cmbFamilyForWall
            // 
            this.cmbFamilyForWall.FormattingEnabled = true;
            this.cmbFamilyForWall.Location = new System.Drawing.Point(14, 37);
            this.cmbFamilyForWall.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFamilyForWall.Name = "cmbFamilyForWall";
            this.cmbFamilyForWall.Size = new System.Drawing.Size(200, 21);
            this.cmbFamilyForWall.TabIndex = 4;
            this.cmbFamilyForWall.SelectedIndexChanged += new System.EventHandler(this.cmbFamilyForWall_SelectedIndexChanged);
            // 
            // cmbFamilyForFloor
            // 
            this.cmbFamilyForFloor.FormattingEnabled = true;
            this.cmbFamilyForFloor.Location = new System.Drawing.Point(14, 91);
            this.cmbFamilyForFloor.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFamilyForFloor.Name = "cmbFamilyForFloor";
            this.cmbFamilyForFloor.Size = new System.Drawing.Size(200, 21);
            this.cmbFamilyForFloor.TabIndex = 5;
            this.cmbFamilyForFloor.SelectedIndexChanged += new System.EventHandler(this.cmbFamilyForFloor_SelectedIndexChanged);
            // 
            // txtbOffset
            // 
            this.txtbOffset.Location = new System.Drawing.Point(14, 145);
            this.txtbOffset.Margin = new System.Windows.Forms.Padding(5);
            this.txtbOffset.Name = "txtbOffset";
            this.txtbOffset.Size = new System.Drawing.Size(200, 20);
            this.txtbOffset.TabIndex = 6;
            this.txtbOffset.TextChanged += new System.EventHandler(this.txtbOffset_TextChanged);
            // 
            // cmbLinkedFile
            // 
            this.cmbLinkedFile.FormattingEnabled = true;
            this.cmbLinkedFile.Location = new System.Drawing.Point(14, 198);
            this.cmbLinkedFile.Margin = new System.Windows.Forms.Padding(5);
            this.cmbLinkedFile.Name = "cmbLinkedFile";
            this.cmbLinkedFile.Size = new System.Drawing.Size(200, 21);
            this.cmbLinkedFile.TabIndex = 7;
            this.cmbLinkedFile.SelectedIndexChanged += new System.EventHandler(this.cmbLinkedFile_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(75, 229);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Window_MEP
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 266);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbLinkedFile);
            this.Controls.Add(this.txtbOffset);
            this.Controls.Add(this.cmbFamilyForFloor);
            this.Controls.Add(this.cmbFamilyForWall);
            this.Controls.Add(this.lblLikedFile);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.lblFamilyForFloor);
            this.Controls.Add(this.lblFamilyForWall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Window_MEP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Window_MEP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFamilyForWall;
        private System.Windows.Forms.Label lblFamilyForFloor;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lblLikedFile;
        private System.Windows.Forms.ComboBox cmbFamilyForWall;
        private System.Windows.Forms.ComboBox cmbFamilyForFloor;
        private System.Windows.Forms.TextBox txtbOffset;
        private System.Windows.Forms.ComboBox cmbLinkedFile;
        private System.Windows.Forms.Button btnOK;
    }
}