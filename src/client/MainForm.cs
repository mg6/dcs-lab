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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;


namespace UnifiedAutomation.Sample
{
    public partial class MainForm : Form
    {
        #region Connect and Disconnect Server
        /// <summary>
        /// Connect to the UA server and read the namespace table.
        /// The connect is based on the Server URL entered in the Form
        /// The read of the namespace table is used to detect the namespace index
        /// of the namespace URI entered in the Form and used for the variables to read
        /// </summary>
        private void connect()
        {
            if (m_session == null)
            {
                // Create a session
                m_session = new Session(m_application);

                // attach to events
                m_session.ConnectionStatusUpdate += new ServerConnectionStatusUpdateEventHandler(Session_ServerConnectionStatusUpdate);
            }

            m_session.UseDnsNameAndPortFromDiscoveryUrl = UseDnsNameAndPortFromDiscoveryUrl.Checked;

            // Step 1 ----------------------------------------------------
            // Connect to the server with no security
            m_session.Connect(txtServerUrl.Text, SecuritySelection.None);

            // Step 2 ----------------------------------------------------
            // Search for the namespace URI entered in the user interface
            // The namespace table is automatically read from the server and provided as property of the session
            // Index in the namespace table is used as namespace index for the NodeIds in the example
            ushort i;
            for (i = 0; i < m_session.NamespaceUris.Count; i++)
            {
                if (m_session.NamespaceUris[i] == txtNamespaceUri.Text)
                {
                    m_NameSpaceIndex = i;
                }
            }
            // Check if the namespace was found
            if (m_NameSpaceIndex == 0)
            {
                throw new Exception("Namespace " + txtNamespaceUri.Text + " not found in server namespace table");
            }
        }

        /// <summary>
        /// Disconnect from the UA server.
        /// </summary>
        private void disconnect()
        {
            // Disconnect from Server
            m_session.Disconnect();
            m_subscription = null;
        }

        /// <summary>
        /// receive updates about session state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Session_ServerConnectionStatusUpdate(Session sender, ServerConnectionStatusUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ServerConnectionStatusUpdateEventHandler(Session_ServerConnectionStatusUpdate), sender, e);
                return;
            }

            // check that the current session matches the session that raised the event.
            if (!Object.ReferenceEquals(m_session, sender))
            {
                return;
            }

            lock (this)
            {
                bool allowEditing = true;

                switch (e.Status)
                {
                    case ServerConnectionStatus.Disconnected:
                        m_bConnected = false;
                        allowEditing = false;
                        // update status label
                        lblConnectionState.Text = "Disconnected";
                        // update buttons
                        //btnConnect.Text = "Connect";
                        //btnMonitor.Text = "Monitor";
                        break;
                    case ServerConnectionStatus.Connected:
                        m_bConnected = true;
                        allowEditing = true;
                        // update status label
                        lblConnectionState.Text = "Connected";
                        // update buttons
                        btnConnect.Text = "Disconnect";
                        startMonitoring();
                        break;
                    case ServerConnectionStatus.ConnectionWarningWatchdogTimeout:
                        // update status label
                        lblConnectionState.Text = "ConnectionWarningWatchdogTimeout";
                        break;
                    case ServerConnectionStatus.ConnectionErrorClientReconnect:
                        // update status label
                        lblConnectionState.Text = "ConnectionErrorClientReconnect";
                        break;
                    case ServerConnectionStatus.ServerShutdownInProgress:
                        // update status label
                        lblConnectionState.Text = "ServerShutdownInProgress";
                        break;
                    case ServerConnectionStatus.ServerShutdown:
                        // update status label
                        lblConnectionState.Text = "ServerShutdown";
                        break;
                    case ServerConnectionStatus.SessionAutomaticallyRecreated:
                        // update status label
                        lblConnectionState.Text = "SessionAutomaticallyRecreated";
                        break;
                    case ServerConnectionStatus.Connecting:
                        // update status label
                        lblConnectionState.Text = "Connecting";
                        break;
                    case ServerConnectionStatus.LicenseExpired:
                        // update status label
                        lblConnectionState.Text = "LicenseExpired";
                        break;
                }

                // Toggle Textboxes
                txtServerUrl.Enabled = !m_bConnected;
                txtNamespaceUri.Enabled = !m_bConnected;

                // Toggle action buttons
                //btnMonitor.Enabled = allowEditing;
                //btnRead.Enabled = allowEditing;
                //btnReadAsync.Enabled = allowEditing;
                //btnWrite.Enabled = allowEditing;
                //btnWriteAsync.Enabled = allowEditing;
            }
        }
        #endregion

        #region Read and Write Variable Values
        /// <summary>
        /// Reads the values of the two variables entered in the From.
        /// The NodeIds used for the Read are constructed from the identifier entered
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void read()
        {
            // Step 1 --------------------------------------------------
            // Prepare nodes to read
            // Add the two variable NodeIds to the list of nodes to read
            // NodeId is constructed from
            // - the identifier text in the text box
            // - the namespace index collected during the server connect
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            //nodesToRead.Add(new ReadValueId()
            //{
            //    NodeId = new NodeId(txtIdentifier1.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value
            //});
            //nodesToRead.Add(new ReadValueId()
            //{
            //    NodeId = new NodeId(txtIdentifier2.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value
            //});


            // Step 2 --------------------------------------------------
            // Read the values from the server
            List<DataValue> results = m_session.Read(nodesToRead);


            // Step 3 --------------------------------------------------
            // Update GUI with results
            // Print result for first variable - check first the result code
            if (StatusCode.IsGood(results[0].StatusCode))
            {
                // The node succeeded - print the value as string
                //txtRead1.Text = results[0].WrappedValue.ToString();
                //txtRead1.BackColor = Color.White;
            }
            else
            {
                // The node failed - print the symbolic name of the status code
                //txtRead1.Text = results[0].StatusCode.ToString();
                //txtRead1.BackColor = Color.Red;
            }
            // Print result for second variable - check first the result code
            if (StatusCode.IsGood(results[1].StatusCode))
            {
                // The node succeeded - print the value as string
                //txtRead2.Text = results[1].WrappedValue.ToString();
                //txtRead2.BackColor = Color.White;
            }
            else
            {
                // The node failed - print the symbolic name of the status code
                //txtRead2.Text = results[1].StatusCode.ToString();
                //txtRead2.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// Write 2 values to the variables entered in the From.
        /// The NodeId used for the Write is constructed from the identifier entered
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void write()
        {
            // Step 1 --------------------------------------------------
            // Get values to write and convert them to the right data type
            if (m_TypeItem1 == BuiltInType.Null || m_TypeItem2 == BuiltInType.Null)
            {
                // Read variable data types from server
                readDataTypes();
            }

            // Get values from GUI and convert them to the right data type
            DataValue val1 = new DataValue();
            //val1.Value = TypeUtils.Cast(txtWrite1.Text, m_TypeItem1);
            DataValue val2 = new DataValue();
            //val2.Value = TypeUtils.Cast(txtWrite2.Text, m_TypeItem2);


            // Step 2 --------------------------------------------------
            // Prepare nodes to write including the values to write
            List<WriteValue> nodesToWrite = new List<WriteValue>();
            //nodesToWrite.Add(new WriteValue()
            //{
            //    NodeId = new NodeId(txtIdentifier1.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value,
            //    Value = val1
            //});
            //nodesToWrite.Add(new WriteValue()
            //{
            //    NodeId = new NodeId(txtIdentifier2.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value,
            //    Value = val2
            //});


            // Step 3 --------------------------------------------------
            // Write values to server
            List<StatusCode> results = m_session.Write(nodesToWrite);


            // Step 4 --------------------------------------------------
            // Update GUI with results
            // check results
            if (StatusCode.IsGood(results[0]))
            {
                // write succeeded - reset background color
                //txtWrite1.BackColor = Color.White;
            }
            else
            {
                // write failed - print the symbolic name of the status code
                //txtWrite1.Text = results[0].ToString();
                //txtWrite1.BackColor = Color.Red;
            }

            if (StatusCode.IsGood(results[1]))
            {
                // write succeeded - reset background color
                //txtWrite2.BackColor = Color.White;
            }
            else
            {
                // write failed - print the symbolic name of the status code
                //txtWrite2.Text = results[1].ToString();
                //txtWrite2.BackColor = Color.Red;
            }
        }
        #endregion

        #region Monitoring of Variable Values
        /// <summary>
        /// Start monitoring by creating a subscription and monitored items
        /// </summary>
        private void startMonitoring()
        {
            // Step 1 --------------------------------------------------
            // Create and initialize subscription
            m_subscription = new Subscription(m_session);
            m_subscription.PublishingEnabled = true;
            m_subscription.PublishingInterval = 100;
            // Data change events will be received through Subscription_DataChanged
            m_subscription.DataChanged += new DataChangedEventHandler(Subscription_DataChanged);

            // Create subscription on server
            m_subscription.Create();


            // Step 2 --------------------------------------------------
            // Prepare variables to monitor as data monitored item
            List<MonitoredItem> monitoredItems = new List<MonitoredItem>();
            // The corresponding result text box gets assigned as user data
            // Default is monitoring Value attributes
            //monitoredItems.Add(new DataMonitoredItem(new NodeId(txtIdentifier1.Text, m_NameSpaceIndex)) { UserData = txtMonitored1 });
            //monitoredItems.Add(new DataMonitoredItem(new NodeId(txtIdentifier2.Text, m_NameSpaceIndex)) { UserData = txtMonitored2 });
            foreach (var accessor in allowedVars.Values)
            {
                var name = accessor.Label;
                monitoredItems.Add(new DataMonitoredItem(new NodeId($"{prefix}.{name}", m_NameSpaceIndex)) { UserData = accessor });
            }


            // Step 3 --------------------------------------------------
            // Create monitored items on server
            List<StatusCode> results = m_subscription.CreateMonitoredItems(monitoredItems);


            // Step 4 --------------------------------------------------
            // Display any errors.
            for (int ii = 0; ii < results.Count; ii++)
            {
                TextBox textBox = monitoredItems[ii].UserData as TextBox;

                if (textBox != null)
                {
                    if (StatusCode.IsBad(results[ii]))
                    {
                        textBox.Text = results[ii].ToString();
                        textBox.BackColor = Color.Red;
                    }
                    else
                    {
                        textBox.Text = String.Empty;
                        textBox.BackColor = Color.White;
                    }
                }
            }
        }

        /// <summary>
        /// Stop monitoring by deleting the subscription
        /// </summary>
        private void stopMonitoring()
        {
            // Delete subscription from server
            // Monitored items are deleted together with the subscription
            m_subscription.Delete();
            m_subscription = null;
        }

        /// <summary>
        /// Receive datachange notifications.
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="e"></param>
        private void Subscription_DataChanged(Subscription subscription, DataChangedEventArgs e)
        {
            // Need to make sure this method is called on the UI thread because it updates UI controls.
            if (InvokeRequired)
            {
                BeginInvoke(new DataChangedEventHandler(Subscription_DataChanged), subscription, e);
                return;
            }

            var asControl = elementHost1.Child as AssemblyStationControl;
            var asViewModel = asControl.DataContext as AssemblyStationViewModel;

            try
            {
                // Check that the subscription has not changed.
                if (!Object.ReferenceEquals(m_subscription, subscription))
                {
                    return;
                }

                foreach (DataChange change in e.DataChanges)
                {
                    var accessor = change.MonitoredItem.UserData as Accessor;
                    if (accessor.Label == "CYCLE_TIME")
                        accessor.Setter(asViewModel, change.Value.GetValue<byte>(0));
                    else
                        accessor.Setter(asViewModel, change.Value.GetValue<bool>(false));

                    // Get text box for displaying value from user data
                    //TextBox textBox = change.MonitoredItem.UserData as TextBox;

                    //if (textBox != null)
                    //{
                    //    // Print result for variable - check first the result code
                    //    if (StatusCode.IsGood(change.Value.StatusCode))
                    //    {
                    //        // The node succeeded - print the value as string
                    //        textBox.Text = change.Value.WrappedValue.ToString();
                    //        textBox.BackColor = Color.White;
                    //    }
                    //    else
                    //    {
                    //        // The node failed - print the symbolic name of the status code
                    //        textBox.Text = change.Value.StatusCode.ToString();
                    //        textBox.BackColor = Color.Red;
                    //    }
                    //}
                }
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("Error in DataChanged callback", exception);
            }
        }
        #endregion

        #region Async Read Variable Values with callback
        /// <summary>
        /// Reads the values of the two variables entered in the From.
        /// The NodeIds used for the Read are constructed from the identifier entered
        /// in the Form and the namespace index detected in the connect method
        /// using the async read method
        /// </summary>
        private void readAsync()
        {
            // Step 1 --------------------------------------------------
            // Prepare nodes to read
            // Add the two variable NodeIds to the list of nodes to read
            // NodeId is constructed from
            // - the identifier text in the text box
            // - the namespace index collected during the server connect
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            //nodesToRead.Add(new ReadValueId()
            //{
            //    NodeId = new NodeId(txtIdentifier1.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value
            //});
            //nodesToRead.Add(new ReadValueId()
            //{
            //    NodeId = new NodeId(txtIdentifier2.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value
            //});

            // use list of objects as user data - add the text fields
            List<object> lstObjects = new List<object>();
            //lstObjects.Add(txtRead1);
            //lstObjects.Add(txtRead2);


            // Step 2 --------------------------------------------------
            // Read the values from the server
            m_session.BeginRead(nodesToRead, 0, TimestampsToReturn.Both, null, OnReadComplete, lstObjects);

            // Read results will be received through OnReadComplete
        }

        /// <summary>
        /// Receive results from async read
        /// </summary>
        /// <param name="result"></param>
        private void OnReadComplete(IAsyncResult result)
        {
            // Need to make sure this method is called on the UI thread because it updates UI controls.
            if (this.InvokeRequired)
            {
                // Asynchronous execution of the AsyncCallback delegate.
                this.BeginInvoke(new AsyncCallback(OnReadComplete), result);
                return;
            }

            List<DataValue> results = null;

            try
            {
                results = m_session.EndRead(result);

                List<object> lstObjects = (List<object>)result.AsyncState;

                if (lstObjects.Count == results.Count)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        object obj = lstObjects[i];

                        if (obj.GetType() == typeof(TextBox))
                        {
                            // Get the according item
                            TextBox txtBox = (TextBox)obj;

                            if (StatusCode.IsGood(results[i].StatusCode))
                            {
                                // Reading the node succeeded - print the value as string
                                txtBox.Text = results[i].Value.ToString();
                                txtBox.BackColor = Color.White;
                            }
                            else
                            {
                                // Reading the node failed - print error code
                                txtBox.Text = results[i].StatusCode.ToString();
                                txtBox.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            // error
                        }
                    }
                }
                else
                {
                    // error
                }
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("OnReadComplete failed", exception);
            }
        }
        #endregion

        #region Async Write Variable Values with callback
        /// <summary>
        /// Write 2 values to the variables entered in the From.
        /// The NodeId used for the Write is constructed from the identifier entered
        /// in the Form and the namespace index detected in the connect method
        /// using the async write method
        /// </summary>
        private void writeAsync()
        {
            // Step 1 --------------------------------------------------
            // Get values to write and convert them to the right data type
            if (m_TypeItem1 == BuiltInType.Null || m_TypeItem2 == BuiltInType.Null)
            {
                readDataTypes();
            }

            // Get values from GUI and convert them to the right data type
            DataValue val1 = new DataValue();
            //val1.Value = TypeUtils.Cast(txtWrite1.Text, m_TypeItem1);
            DataValue val2 = new DataValue();
            //val2.Value = TypeUtils.Cast(txtWrite2.Text, m_TypeItem2);


            // Step 2 --------------------------------------------------
            // Prepare nodes to write including the values to write
            List<WriteValue> nodesToWrite = new List<WriteValue>();
            //nodesToWrite.Add(new WriteValue()
            //{
            //    NodeId = new NodeId(txtIdentifier1.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value,
            //    Value = val1
            //});
            //nodesToWrite.Add(new WriteValue()
            //{
            //    NodeId = new NodeId(txtIdentifier2.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.Value,
            //    Value = val2
            //});

            // use list of objects as user data - add the text fields
            List<object> lstObjects = new List<object>();
            //lstObjects.Add(txtRead2);
            //lstObjects.Add(txtRead1);


            // Step 3 --------------------------------------------------
            // Write values to server
            m_session.BeginWrite(nodesToWrite, OnWriteComplete, lstObjects);

            // Write results will be received through OnWriteComplete
        }

        /// <summary>
        /// Receive results from async write
        /// </summary>
        /// <param name="result"></param>
        private void OnWriteComplete(IAsyncResult result)
        {
            // Need to make sure this method is called on the UI thread because it updates UI controls.
            if (this.InvokeRequired)
            {
                // Asynchronous execution of the AsyncCallback delegate.
                this.BeginInvoke(new AsyncCallback(OnWriteComplete), result);
                return;
            }

            List<StatusCode> results = null;

            try
            {
                results = m_session.EndWrite(result);

                List<object> lstObjects = (List<object>)result.AsyncState;

                if (lstObjects.Count == results.Count)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        object obj = lstObjects[i];

                        if (obj.GetType() == typeof(TextBox))
                        {
                            // Get the according item
                            TextBox txtBox = (TextBox)obj;

                            if (StatusCode.IsNotGood(results[i]))
                            {
                                // Writing the node failed - print error code
                                txtBox.Text = results[i].ToString();
                                txtBox.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            // error
                        }
                    }
                }
                else
                {
                    // error
                }
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("OnWriteComplete failed", exception);
            }
        }
        #endregion

        #region Read data types of variables from server
        /// <summary>
        /// Read the type informatin for both variables configured.
        /// </summary>
        private void readDataTypes()
        {
            // Add the two variable NodeIds to the list of nodes to read
            // NodeId is constructed from
            // - the identifier text in the text box
            // - the namespace index collected during the server connect
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            //nodesToRead.Add(new ReadValueId()
            //{
            //    NodeId = new NodeId(txtIdentifier1.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.DataType
            //});
            //nodesToRead.Add(new ReadValueId()
            //{
            //    NodeId = new NodeId(txtIdentifier2.Text, m_NameSpaceIndex),
            //    AttributeId = Attributes.DataType
            //});

            // Read the datatypes
            List<DataValue> results = null;

            try
            {
                results = m_session.Read(nodesToRead, 0, TimestampsToReturn.Neither, null);

                // check the result code
                if (StatusCode.IsGood(results[0].StatusCode))
                {
                    // The node succeeded - save buildInType for later use
                    m_TypeItem1 = TypeUtils.GetBuiltInType((NodeId)results[0].Value);
                }
                else
                {
                    //throw new Exception("Read datatype failed for item " + txtIdentifier1.Text);
                }

                // check the result code
                if (StatusCode.IsGood(results[1].StatusCode))
                {
                    // The node succeeded - save buildInType for later use
                    m_TypeItem2 = TypeUtils.GetBuiltInType((NodeId)results[1].Value);
                }
                else
                {
                    //throw new Exception("Read datatype failed for item " + txtIdentifier1.Text);
                }
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("Read datatype failed", exception);
                throw exception;
            }
        }
        #endregion

        #region Construction
        public MainForm(ApplicationInstance applicationInstance)
        {
            m_application = applicationInstance;
            InitializeComponent();
        }
        #endregion

        #region Fields
        private ApplicationInstance m_application = null;
        private Session m_session = null;
        private bool m_bConnected = false;
        private Subscription m_subscription = null;
        private UInt16 m_NameSpaceIndex = 0;
        private BuiltInType m_TypeItem1;
        private BuiltInType m_TypeItem2;
        #endregion

        #region User actions
        /// <summary>
        /// Handle event for connect button
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (m_bConnected)
            {
                try
                {
                    disconnect();
                }
                catch (Exception exception)
                {
                    ExceptionDlg.Show(null, exception);
                }
            }
            else
            {
                try
                {
                    connect();
                }
                catch (Exception exception)
                {
                    ExceptionDlg.Show("Connect failed", exception);
                }
            }
        }

        /// <summary>
        /// Handle event for Read button
        /// </summary>
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                read();
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show(null, exception);
            }
        }

        /// <summary>
        /// Handle event for ReadAsync button
        /// </summary>
        private void btnReadAsync_Click(object sender, EventArgs e)
        {
            try
            {
                readAsync();
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("Read failed", exception);
            }
        }

        /// <summary>
        /// Handle event for Write button
        /// </summary>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                write();
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("Write failed", exception);
            }
        }

        /// <summary>
        /// Handle event for WriteAsync button
        /// </summary>
        private void btnWriteAsync_Click(object sender, EventArgs e)
        {
            try
            {
                writeAsync();
            }
            catch (Exception exception)
            {
                ExceptionDlg.Show("Write failed", exception);
            }
        }

        /// <summary>
        /// Starts the monitoring of the values of the two variables entered in the From.
        /// The NodeIds used for the monitoring are constructed from the identifier entered
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void btnMonitor_Click(object sender, EventArgs e)
        {
            if (m_subscription == null)
            {
                // create the subscription
                try
                {
                    startMonitoring();

                    //btnMonitor.Text = "Stop";
                }
                catch (Exception exception)
                {
                    ExceptionDlg.Show("Create subscription failed", exception);

                    m_subscription = null;
                    //btnMonitor.Text = "Monitor";
                }
            }
            else
            {
                try
                {
                    stopMonitoring();

                    //btnMonitor.Text = "Monitor";
                    //txtMonitored1.Text = "";
                    //txtMonitored2.Text = "";
                }
                catch (Exception exception)
                {
                    ExceptionDlg.Show("Stopping  monitoring failed", exception);
                }
            }
        }
        #endregion

        private System.Threading.Timer timer;

        public class Accessor
        {
            public String Label;
            public Action<AssemblyStationViewModel, object> Setter;
            public Func<AssemblyStationViewModel, object> Getter;
        }

        private string prefix = "UA2_1_2";

        private Dictionary<String, Accessor> allowedVars = new Dictionary<String, Accessor>()
        {
            {
                "StInput",
                new Accessor() {
                    Label = "ST_INPUT",
                    Getter = (vm) => vm.StInput,
                    Setter = (vm, val) => vm.StInput = (bool)val
                }
            },
            {
                "StOutput",
                new Accessor() {
                    Label = "ST_OUTPUT",
                    Getter = (vm) => vm.StOutput,
                    Setter = (vm, val) => vm.StOutput = (bool)val
                }
            },
            {
                "CurrentCycleTime",
                new Accessor() {
                    Label = "CYCLE_TIME",
                    Getter = (vm) => vm.CurrentCycleTime,
                    Setter = (vm, val) => vm.CurrentCycleTime= (byte)val
                }
            },
            {
                "Empty",
                new Accessor() {
                    Label = "EMPTY",
                    Getter = (vm) => vm.Empty,
                    Setter = (vm, val) => vm.Empty = (bool)val
                }
            },
            {
                "Run",
                new Accessor() {
                    Label = "RUN",
                    Getter = (vm) => vm.Run,
                    Setter = (vm, val) => vm.Run = (bool)val
                }
            },
            {
                "Blocked",
                new Accessor() {
                    Label = "BLOCKED",
                    Getter = (vm) => vm.Blocked,
                    Setter = (vm, val) => vm.Blocked = (bool)val
                }
            },
            {
                "Alarm",
                 new Accessor() {
                     Label = "ALARM",
                     Getter = (vm) => vm.Alarm,
                     Setter = (vm, val) => vm.Alarm = (bool)val
                 }
            },
            {
                "Excluded",
                new Accessor() {
                    Label = "EXCLUDED",
                    Getter = (vm) => vm.Excluded,
                    Setter = (vm, val) => vm.Excluded = (bool)val
                }
            },
            {
                "Timeout",
                new Accessor() {
                    Label = "TIMEOUT",
                    Getter = (vm) => vm.Timeout,
                    Setter = (vm, val) => vm.Timeout = (bool)val
                }

            },
        };

        private void MainForm_Load(object sender, EventArgs e)
        {
            var asControl = elementHost1.Child as AssemblyStationControl;
            var asViewModel = asControl.DataContext as AssemblyStationViewModel;

            asViewModel.PropertyChanged += (object _sender, PropertyChangedEventArgs _e) =>
            {
                if (allowedVars.TryGetValue(_e.PropertyName, out var accessor))
                {
                    var val = accessor.Getter(asViewModel);
                    if (val is bool)
                        writeSync($"{prefix}.{accessor.Label}", (bool)val);
                    else
                        writeSync($"{prefix}.{accessor.Label}", (byte)val);
                }
            };

            timer = new System.Threading.Timer((callback) =>
            {
                BeginInvoke(new Action(() =>
                {
                    asViewModel.Tick();
                }));
            }, null, 1000, 1000);
        }

        public void writeSync(string id, bool val)
        {
            List<StatusCode> results = m_session.Write(new List<WriteValue>()
            {
                new WriteValue()
                {
                    NodeId = new NodeId(id, m_NameSpaceIndex),
                    AttributeId = Attributes.Value,
                    Value = new DataValue { Value = TypeUtils.Cast(val, BuiltInType.Boolean) },
                }
            });
        }

        public void writeSync(string id, byte val)
        {
            List<StatusCode> results = m_session.Write(new List<WriteValue>()
            {
                new WriteValue()
                {
                    NodeId = new NodeId(id, m_NameSpaceIndex),
                    AttributeId = Attributes.Value,
                    Value = new DataValue { Value = TypeUtils.Cast(val, BuiltInType.Byte) },
                }
            });
        }
    }
}
