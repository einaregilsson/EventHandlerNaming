namespace EinarEgilsson.EventHandlerNaming
{
    partial class OptionsForm
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
            this.lblPattern = new System.Windows.Forms.Label();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.cboSiteNameTransform = new System.Windows.Forms.ComboBox();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.cboEventNameTransform = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkOmitSiteNameForOwnEvents = new System.Windows.Forms.CheckBox();
            this.chkUseDelegateInference = new System.Windows.Forms.CheckBox();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.txtRemovePrefixes = new System.Windows.Forms.TextBox();
            this.lblRemovePrefixes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(58, 27);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(44, 13);
            this.lblPattern.TabIndex = 0;
            this.lblPattern.Text = "Pattern:";
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(46, 53);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(56, 13);
            this.lblSiteName.TabIndex = 2;
            this.lblSiteName.Text = "SiteName:";
            // 
            // cboSiteNameTransform
            // 
            this.cboSiteNameTransform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSiteNameTransform.FormattingEnabled = true;
            this.cboSiteNameTransform.Location = new System.Drawing.Point(108, 50);
            this.cboSiteNameTransform.Name = "cboSiteNameTransform";
            this.cboSiteNameTransform.Size = new System.Drawing.Size(293, 21);
            this.cboSiteNameTransform.TabIndex = 3;
            // 
            // txtPattern
            // 
            this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPattern.Location = new System.Drawing.Point(108, 24);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(293, 20);
            this.txtPattern.TabIndex = 1;
            this.txtPattern.Text = "On$(SiteName)$(EventName)";
            // 
            // cboEventNameTransform
            // 
            this.cboEventNameTransform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEventNameTransform.FormattingEnabled = true;
            this.cboEventNameTransform.Location = new System.Drawing.Point(108, 77);
            this.cboEventNameTransform.Name = "cboEventNameTransform";
            this.cboEventNameTransform.Size = new System.Drawing.Size(293, 21);
            this.cboEventNameTransform.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "EventName:";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(256, 215);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnOKClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(337, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkOmitSiteNameForOwnEvents
            // 
            this.chkOmitSiteNameForOwnEvents.AutoSize = true;
            this.chkOmitSiteNameForOwnEvents.Checked = true;
            this.chkOmitSiteNameForOwnEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOmitSiteNameForOwnEvents.Location = new System.Drawing.Point(108, 141);
            this.chkOmitSiteNameForOwnEvents.Name = "chkOmitSiteNameForOwnEvents";
            this.chkOmitSiteNameForOwnEvents.Size = new System.Drawing.Size(181, 17);
            this.chkOmitSiteNameForOwnEvents.TabIndex = 8;
            this.chkOmitSiteNameForOwnEvents.Text = "Omit $(SiteName) for own events";
            this.chkOmitSiteNameForOwnEvents.UseVisualStyleBackColor = true;
            // 
            // chkUseDelegateInference
            // 
            this.chkUseDelegateInference.AutoSize = true;
            this.chkUseDelegateInference.Location = new System.Drawing.Point(108, 165);
            this.chkUseDelegateInference.Name = "chkUseDelegateInference";
            this.chkUseDelegateInference.Size = new System.Drawing.Size(136, 17);
            this.chkUseDelegateInference.TabIndex = 9;
            this.chkUseDelegateInference.Text = "Use delegate inference";
            this.chkUseDelegateInference.UseVisualStyleBackColor = true;
            // 
            // lnkHelp
            // 
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Location = new System.Drawing.Point(105, 185);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(121, 13);
            this.lnkHelp.TabIndex = 10;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Help, how do I use this?";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // txtRemovePrefixes
            // 
            this.txtRemovePrefixes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemovePrefixes.Location = new System.Drawing.Point(108, 104);
            this.txtRemovePrefixes.Name = "txtRemovePrefixes";
            this.txtRemovePrefixes.Size = new System.Drawing.Size(293, 20);
            this.txtRemovePrefixes.TabIndex = 7;
            this.txtRemovePrefixes.Text = "txt;lbl;btn;cbo;grp;chk;prg;rdo;grd;lst;edt";
            // 
            // lblRemovePrefixes
            // 
            this.lblRemovePrefixes.AutoSize = true;
            this.lblRemovePrefixes.Location = new System.Drawing.Point(13, 107);
            this.lblRemovePrefixes.Name = "lblRemovePrefixes";
            this.lblRemovePrefixes.Size = new System.Drawing.Size(89, 13);
            this.lblRemovePrefixes.TabIndex = 6;
            this.lblRemovePrefixes.Text = "Remove prefixes:";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 250);
            this.ControlBox = false;
            this.Controls.Add(this.lblRemovePrefixes);
            this.Controls.Add(this.txtRemovePrefixes);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.chkUseDelegateInference);
            this.Controls.Add(this.chkOmitSiteNameForOwnEvents);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboEventNameTransform);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.cboSiteNameTransform);
            this.Controls.Add(this.lblSiteName);
            this.Controls.Add(this.lblPattern);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsForm";
            this.Text = "EventHandler Naming Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.Label lblSiteName;
        private System.Windows.Forms.ComboBox cboSiteNameTransform;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.ComboBox cboEventNameTransform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkOmitSiteNameForOwnEvents;
        private System.Windows.Forms.CheckBox chkUseDelegateInference;
        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.TextBox txtRemovePrefixes;
        private System.Windows.Forms.Label lblRemovePrefixes;
    }
}