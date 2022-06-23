
namespace PI1_СutOpening
{
    partial class Window_AR
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
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbFamilyForWall = new System.Windows.Forms.ComboBox();
            this.cmbFamilyForFloor = new System.Windows.Forms.ComboBox();
            this.lblFamilyForWall = new System.Windows.Forms.Label();
            this.lblFamilyForFloor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(74, 122);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbFamilyForWall
            // 
            this.cmbFamilyForWall.FormattingEnabled = true;
            this.cmbFamilyForWall.Location = new System.Drawing.Point(14, 37);
            this.cmbFamilyForWall.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFamilyForWall.Name = "cmbFamilyForWall";
            this.cmbFamilyForWall.Size = new System.Drawing.Size(200, 21);
            this.cmbFamilyForWall.TabIndex = 1;
            this.cmbFamilyForWall.SelectedIndexChanged += new System.EventHandler(this.cmbFamilyForWall_SelectedIndexChanged);
            // 
            // cmbFamilyForFloor
            // 
            this.cmbFamilyForFloor.FormattingEnabled = true;
            this.cmbFamilyForFloor.Location = new System.Drawing.Point(14, 91);
            this.cmbFamilyForFloor.Margin = new System.Windows.Forms.Padding(5);
            this.cmbFamilyForFloor.Name = "cmbFamilyForFloor";
            this.cmbFamilyForFloor.Size = new System.Drawing.Size(200, 21);
            this.cmbFamilyForFloor.TabIndex = 2;
            this.cmbFamilyForFloor.SelectedIndexChanged += new System.EventHandler(this.cmbFamilyForFloor_SelectedIndexChanged);
            // 
            // lblFamilyForWall
            // 
            this.lblFamilyForWall.AutoSize = true;
            this.lblFamilyForWall.Location = new System.Drawing.Point(14, 14);
            this.lblFamilyForWall.Margin = new System.Windows.Forms.Padding(5);
            this.lblFamilyForWall.Name = "lblFamilyForWall";
            this.lblFamilyForWall.Size = new System.Drawing.Size(173, 13);
            this.lblFamilyForWall.TabIndex = 3;
            this.lblFamilyForWall.Text = "Семейство отверстия для стены";
            // 
            // lblFamilyForFloor
            // 
            this.lblFamilyForFloor.AutoSize = true;
            this.lblFamilyForFloor.Location = new System.Drawing.Point(14, 68);
            this.lblFamilyForFloor.Margin = new System.Windows.Forms.Padding(5);
            this.lblFamilyForFloor.Name = "lblFamilyForFloor";
            this.lblFamilyForFloor.Size = new System.Drawing.Size(166, 13);
            this.lblFamilyForFloor.TabIndex = 4;
            this.lblFamilyForFloor.Text = "Семейство отверстия для пола";
            // 
            // Window_AR
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 155);
            this.Controls.Add(this.lblFamilyForFloor);
            this.Controls.Add(this.lblFamilyForWall);
            this.Controls.Add(this.cmbFamilyForFloor);
            this.Controls.Add(this.cmbFamilyForWall);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Window_AR";
            this.ShowIcon = false;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Window_AR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cmbFamilyForWall;
        private System.Windows.Forms.ComboBox cmbFamilyForFloor;
        private System.Windows.Forms.Label lblFamilyForWall;
        private System.Windows.Forms.Label lblFamilyForFloor;
    }
}