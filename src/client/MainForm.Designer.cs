/******************************************************************************
** Copyright (c) 2006-2016 Unified Automation GmbH All rights reserved.
**
** Software License Agreement ("SLA") Version 2.5
**
** Unless explicitly acquired and licensed from Licensor under another
** license, the contents of this file are subject to the Software License
** Agreement ("SLA") Version 2.5, or subsequent versions
** as allowed by the SLA, and You may not copy or use this file in either
** source code or executable form, except in compliance with the terms and
** conditions of the SLA.
**
** All software distributed under the SLA is provided strictly on an
** "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED,
** AND LICENSOR HEREBY DISCLAIMS ALL SUCH WARRANTIES, INCLUDING WITHOUT
** LIMITATION, ANY WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
** PURPOSE, QUIET ENJOYMENT, OR NON-INFRINGEMENT. See the SLA for specific
** language governing rights and limitations under the SLA.
**
** Project: .NET based OPC UA Client Server SDK
**
** Description: OPC Unified Architecture Software Development Kit.
**
** The complete license agreement can be found here:
** http://unifiedautomation.com/License/SLA/2.5/
******************************************************************************/

namespace UnifiedAutomation.Sample
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtNamespaceUri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.txtMonitored1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMonitored2 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblConnectionState = new System.Windows.Forms.Label();
            this.UseDnsNameAndPortFromDiscoveryUrl = new System.Windows.Forms.CheckBox();
            this.btnWriteAsync = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIdentifier2 = new System.Windows.Forms.TextBox();
            this.txtIdentifier1 = new System.Windows.Forms.TextBox();
            this.txtWrite1 = new System.Windows.Forms.TextBox();
            this.txtWrite2 = new System.Windows.Forms.TextBox();
            this.btnReadAsync = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtRead2 = new System.Windows.Forms.TextBox();
            this.txtRead1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerUrl.Location = new System.Drawing.Point(114, 4);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(199, 20);
            this.txtServerUrl.TabIndex = 0;
            this.txtServerUrl.Text = "opc.tcp://localhost:48030";
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnConnect.Location = new System.Drawing.Point(3, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtNamespaceUri
            // 
            this.txtNamespaceUri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespaceUri.Location = new System.Drawing.Point(114, 34);
            this.txtNamespaceUri.Name = "txtNamespaceUri";
            this.txtNamespaceUri.Size = new System.Drawing.Size(199, 20);
            this.txtNamespaceUri.TabIndex = 3;
            this.txtNamespaceUri.Text = "http://yourorganisation.org/DCS-lab/";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(319, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "OPC UA Server URL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Used Namespce URI";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMonitor
            // 
            this.btnMonitor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMonitor.Enabled = false;
            this.btnMonitor.Location = new System.Drawing.Point(561, 90);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(182, 23);
            this.btnMonitor.TabIndex = 6;
            this.btnMonitor.Text = "Monitor";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // txtMonitored1
            // 
            this.txtMonitored1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonitored1.Location = new System.Drawing.Point(561, 33);
            this.txtMonitored1.Name = "txtMonitored1";
            this.txtMonitored1.Size = new System.Drawing.Size(182, 20);
            this.txtMonitored1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Monitored Value";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMonitored2
            // 
            this.txtMonitored2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonitored2.Location = new System.Drawing.Point(561, 62);
            this.txtMonitored2.Name = "txtMonitored2";
            this.txtMonitored2.Size = new System.Drawing.Size(182, 20);
            this.txtMonitored2.TabIndex = 19;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtServerUrl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNamespaceUri, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblConnectionState, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.UseDnsNameAndPortFromDiscoveryUrl, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(746, 59);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(433, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Connection State:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConnectionState
            // 
            this.lblConnectionState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionState.Location = new System.Drawing.Point(545, 8);
            this.lblConnectionState.Name = "lblConnectionState";
            this.lblConnectionState.Size = new System.Drawing.Size(228, 13);
            this.lblConnectionState.TabIndex = 7;
            this.lblConnectionState.Text = "disconnected";
            this.lblConnectionState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UseDnsNameAndPortFromDiscoveryUrl
            // 
            this.UseDnsNameAndPortFromDiscoveryUrl.AutoSize = true;
            this.UseDnsNameAndPortFromDiscoveryUrl.Checked = true;
            this.UseDnsNameAndPortFromDiscoveryUrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseDnsNameAndPortFromDiscoveryUrl.Location = new System.Drawing.Point(3, 32);
            this.UseDnsNameAndPortFromDiscoveryUrl.Name = "UseDnsNameAndPortFromDiscoveryUrl";
            this.UseDnsNameAndPortFromDiscoveryUrl.Size = new System.Drawing.Size(105, 17);
            this.UseDnsNameAndPortFromDiscoveryUrl.TabIndex = 8;
            this.UseDnsNameAndPortFromDiscoveryUrl.Text = "UseDiscoveryUrl";
            this.UseDnsNameAndPortFromDiscoveryUrl.UseVisualStyleBackColor = true;
            // 
            // btnWriteAsync
            // 
            this.btnWriteAsync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnWriteAsync.Enabled = false;
            this.btnWriteAsync.Location = new System.Drawing.Point(375, 119);
            this.btnWriteAsync.Name = "btnWriteAsync";
            this.btnWriteAsync.Size = new System.Drawing.Size(180, 23);
            this.btnWriteAsync.TabIndex = 29;
            this.btnWriteAsync.Text = "Write Asynchronous";
            this.btnWriteAsync.UseVisualStyleBackColor = true;
            this.btnWriteAsync.Click += new System.EventHandler(this.btnWriteAsync_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnWrite.Enabled = false;
            this.btnWrite.Location = new System.Drawing.Point(375, 90);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(180, 23);
            this.btnWrite.TabIndex = 14;
            this.btnWrite.Text = "Write Synchronous";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Variable Identifier";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(375, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Write Value";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtIdentifier2
            // 
            this.txtIdentifier2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentifier2.Location = new System.Drawing.Point(3, 62);
            this.txtIdentifier2.Name = "txtIdentifier2";
            this.txtIdentifier2.Size = new System.Drawing.Size(180, 20);
            this.txtIdentifier2.TabIndex = 18;
            this.txtIdentifier2.Text = "Demo.Static.Scalar.Double";
            // 
            // txtIdentifier1
            // 
            this.txtIdentifier1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentifier1.Location = new System.Drawing.Point(3, 33);
            this.txtIdentifier1.Name = "txtIdentifier1";
            this.txtIdentifier1.Size = new System.Drawing.Size(180, 20);
            this.txtIdentifier1.TabIndex = 7;
            this.txtIdentifier1.Text = "Demo.Dynamic.Scalar.Int32";
            // 
            // txtWrite1
            // 
            this.txtWrite1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWrite1.Location = new System.Drawing.Point(375, 33);
            this.txtWrite1.Name = "txtWrite1";
            this.txtWrite1.Size = new System.Drawing.Size(180, 20);
            this.txtWrite1.TabIndex = 15;
            // 
            // txtWrite2
            // 
            this.txtWrite2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWrite2.Location = new System.Drawing.Point(375, 62);
            this.txtWrite2.Name = "txtWrite2";
            this.txtWrite2.Size = new System.Drawing.Size(180, 20);
            this.txtWrite2.TabIndex = 23;
            // 
            // btnReadAsync
            // 
            this.btnReadAsync.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReadAsync.Enabled = false;
            this.btnReadAsync.Location = new System.Drawing.Point(189, 119);
            this.btnReadAsync.Name = "btnReadAsync";
            this.btnReadAsync.Size = new System.Drawing.Size(180, 23);
            this.btnReadAsync.TabIndex = 24;
            this.btnReadAsync.Text = "Read Asynchronous";
            this.btnReadAsync.UseVisualStyleBackColor = true;
            this.btnReadAsync.Click += new System.EventHandler(this.btnReadAsync_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRead.Enabled = false;
            this.btnRead.Location = new System.Drawing.Point(189, 90);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(180, 23);
            this.btnRead.TabIndex = 11;
            this.btnRead.Text = "Read Synchronous";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtRead2
            // 
            this.txtRead2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRead2.Location = new System.Drawing.Point(189, 62);
            this.txtRead2.Name = "txtRead2";
            this.txtRead2.Size = new System.Drawing.Size(180, 20);
            this.txtRead2.TabIndex = 12;
            // 
            // txtRead1
            // 
            this.txtRead1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRead1.Location = new System.Drawing.Point(189, 33);
            this.txtRead1.Name = "txtRead1";
            this.txtRead1.Size = new System.Drawing.Size(180, 20);
            this.txtRead1.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(189, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Read Value";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMonitor, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtMonitored2, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtMonitored1, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnWriteAsync, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtWrite2, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnWrite, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtWrite1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtIdentifier1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtRead2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtIdentifier2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtRead1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnRead, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnReadAsync, 1, 4);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 77);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(746, 146);
            this.tableLayoutPanel2.TabIndex = 30;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 236);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Unified Automation Sample Client Basic";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtNamespaceUri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.TextBox txtMonitored1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMonitored2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtIdentifier1;
        private System.Windows.Forms.TextBox txtIdentifier2;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWrite1;
        private System.Windows.Forms.TextBox txtWrite2;
        private System.Windows.Forms.Button btnWriteAsync;
        private System.Windows.Forms.Button btnReadAsync;
        private System.Windows.Forms.TextBox txtRead2;
        private System.Windows.Forms.TextBox txtRead1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblConnectionState;
        private System.Windows.Forms.CheckBox UseDnsNameAndPortFromDiscoveryUrl;
    }
}
