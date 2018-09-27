namespace MockAHolyServer
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
            this.bsDevices = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetDevices = new MockAHolyServer.DataSet();
            this.btnFind = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnInvite = new System.Windows.Forms.Button();
            this.btnConnection = new System.Windows.Forms.Button();
            this.btnSendNumPlayers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bsDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // bsDevices
            // 
            this.bsDevices.DataMember = "Devices";
            this.bsDevices.DataSource = this.dataSetDevices;
            // 
            // dataSetDevices
            // 
            this.dataSetDevices.DataSetName = "DataSet";
            this.dataSetDevices.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(202, 12);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(339, 23);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "Find devices";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(184, 225);
            this.listBox1.TabIndex = 3;
            // 
            // btnInvite
            // 
            this.btnInvite.Location = new System.Drawing.Point(202, 41);
            this.btnInvite.Name = "btnInvite";
            this.btnInvite.Size = new System.Drawing.Size(339, 23);
            this.btnInvite.TabIndex = 4;
            this.btnInvite.Text = "Invite";
            this.btnInvite.UseVisualStyleBackColor = true;
            this.btnInvite.Click += new System.EventHandler(this.btnInvite_Click);
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(202, 111);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(339, 23);
            this.btnConnection.TabIndex = 5;
            this.btnConnection.Text = "Start connection to selected device";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // btnSendNumPlayers
            // 
            this.btnSendNumPlayers.Location = new System.Drawing.Point(202, 214);
            this.btnSendNumPlayers.Name = "btnSendNumPlayers";
            this.btnSendNumPlayers.Size = new System.Drawing.Size(339, 23);
            this.btnSendNumPlayers.TabIndex = 6;
            this.btnSendNumPlayers.Text = "Send num palyer and start";
            this.btnSendNumPlayers.UseVisualStyleBackColor = true;
            this.btnSendNumPlayers.Click += new System.EventHandler(this.btnSendNumPlayers_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 246);
            this.Controls.Add(this.btnSendNumPlayers);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this.btnInvite);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnFind);
            this.Name = "Form1";
            this.Text = "Mock-A-Holy Bluetooth pass-through to Unity";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetDevices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bsDevices;
        private DataSet dataSetDevices;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnInvite;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.Button btnSendNumPlayers;
    }
}

