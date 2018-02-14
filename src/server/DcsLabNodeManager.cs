/******************************************************************************
**
** <auto-generated>
**     This code was generated by a tool: UaModeler
**     Runtime Version: 1.5.2, using .NET Server 2.5.0 template (version 3)
**
**     This is a template file that was generated for your convenience.
**     This file will not be overwritten when generating code again.
**     ADD YOUR IMPLEMTATION HERE!
** </auto-generated>
**
** Copyright (c) 2006-2018 Unified Automation GmbH All rights reserved.
**
** Software License Agreement ("SLA") Version 2.6
**
** Unless explicitly acquired and licensed from Licensor under another
** license, the contents of this file are subject to the Software License
** Agreement ("SLA") Version 2.6, or subsequent versions
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
** Project: .NET OPC UA SDK information model for namespace http://yourorganisation.org/DCS-lab/
**
** Description: OPC Unified Architecture Software Development Kit.
**
** The complete license agreement can be found here:
** http://unifiedautomation.com/License/SLA/2.6/
**
** Created: 13.02.2018
**
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaServer;
using UnifiedAutomation.Sample;

namespace Polsl.DcsLab
{
    internal partial class DcsLabNodeManager : BaseNodeManager
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DcsLabNodeManager(ServerManager server) : base(server)
        {
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// An overrideable version of the Dispose.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TBD
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Called when the node manager is started.
        /// </summary>
        public override void Startup()
        {
            try
            {
                Console.WriteLine("Starting DcsLabNodeManager.");

                DefaultNamespaceIndex = AddNamespaceUri("http://yourorganisation.org/DCS-lab/");

                Console.WriteLine("Loading the DcsLab Model.");
                ImportUaNodeset(Assembly.GetEntryAssembly(), "dcs-lab.xml");

                var controllers = LookupObject(ObjectIds.Controllers);

                var station1 = CreateAssemblyStation("AS1_1_2",  controllers);
                var station2 = CreateAssemblyStation("AS21_1_2", controllers);
                var station3 = CreateAssemblyStation("AS2_1_2",  controllers);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to start DcsLabNodeManager " + e.Message);
            }
        }

        /// <summary>
        /// Reads the attributes.
        /// </summary>
        protected override void Read(
            RequestContext context,
            TransactionHandle transaction,
            IList<NodeAttributeOperationHandle> operationHandles,
            IList<ReadValueId> settings)
        {
            for (int ii = 0; ii < operationHandles.Count; ii++)
            {
                DataValue dv = null;

                // the data passed to CreateVariable is returned as the UserData in the handle.
                if (operationHandles[ii].NodeHandle.UserData is Tuple<Func<object>, Action<object>> accessors)
                {
                    var getter = accessors.Item1;

                    dv = new DataValue(new Variant(getter(), null), DateTime.UtcNow);

                    // apply any index range or encoding.
                    if (!string.IsNullOrEmpty(settings[ii].IndexRange) || !QualifiedName.IsNull(settings[ii].DataEncoding))
                    {
                        dv = ApplyIndexRangeAndEncoding(
                            operationHandles[ii].NodeHandle,
                            dv,
                            settings[ii].IndexRange,
                            settings[ii].DataEncoding);
                    }
                }

                // set an error if not found.
                if (dv == null)
                {
                    dv = new DataValue(new StatusCode(StatusCodes.BadNodeIdUnknown));
                }

                // return the data to the caller.
                ((ReadCompleteEventHandler)transaction.Callback)(
                    operationHandles[ii],
                    transaction.CallbackData,
                    dv,
                    false);
            }
        }

        /// <summary>
        /// Write the attributes
        /// </summary>
        protected override void Write(
            RequestContext context,
            TransactionHandle transaction,
            IList<NodeAttributeOperationHandle> operationHandles,
            IList<WriteValue> settings)
        {
            for (int ii = 0; ii < operationHandles.Count; ii++)
            {
                StatusCode error = StatusCodes.Good;

                // the data passed to CreateVariable is returned as the UserData in the handle.

                if (operationHandles[ii].NodeHandle.UserData is Tuple<Func<object>, Action<object>> accessors)
                {
                    if (!string.IsNullOrEmpty(settings[ii].IndexRange))
                    {
                        error = StatusCodes.BadIndexRangeInvalid;
                    }

                    var setter = accessors.Item2;

                    setter(settings[ii].Value.Value);
                }
                else
                {
                    error = StatusCodes.BadNodeIdUnknown;
                }

                // return the data to the caller.
                ((WriteCompleteEventHandler)transaction.Callback)(
                    operationHandles[ii],
                    transaction.CallbackData,
                    error,
                    false);
            }
        }

        /// <summary>
        /// Called when the node manager is stopped.
        /// </summary>
        public override void Shutdown()
        {
            try
            {
                Console.WriteLine("Stopping DcsLabNodeManager.");

                // TBD
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to stop DcsLabNodeManager " + e.Message);
            }
        }
        #endregion

        public ObjectNode LookupObject(ExpandedNodeId nodeId)
        {
            return (ObjectNode)FindInMemoryNode(new NodeId((uint)nodeId.Identifier, DefaultNamespaceIndex));
        }

        public AssemblyStationViewModel CreateAssemblyStation(string name, ObjectNode parent)
        {
            var settings = new CreateObjectSettings()
            {
                ParentNodeId = parent.NodeId,
                ReferenceTypeId = UnifiedAutomation.UaBase.ReferenceTypeIds.Organizes,
                RequestedNodeId = new NodeId(name, DefaultNamespaceIndex),
                BrowseName = new QualifiedName(name, DefaultNamespaceIndex),
                TypeDefinitionId = new NodeId(Polsl.DcsLab.ObjectTypes.AssemblyStationType, DefaultNamespaceIndex),
            };

            var node = CreateObject(Server.DefaultRequestContext, settings);

            var vm = new AssemblyStationViewModel(node);
            var getters = getGetters(vm);
            var setters = getSetters(vm);

            foreach (var variable in variables) {
                SetVariableConfiguration(
                    node.NodeId, new QualifiedName(variable, DefaultNamespaceIndex),
                    NodeHandleType.ExternalPolled,
                    new Tuple<Func<object>, Action<object>>(getters[variable], setters[variable]));
            }
            return vm;
        }

        private readonly string[] variables = new string[]
        {
            "ST_INPUT",
            "ST_OUTPUT",
            "CYCLE_TIME",
            "ALARM",
            "BLOCKED",
            "EMPTY",
            "EXCLUDED",
            "RUN",
            "TIMEOUT",
        };

        private Dictionary<string, Action<object>> getSetters(AssemblyStationViewModel station)
        {
            return new Dictionary<string, Action<object>>(){
                {"ST_INPUT", v => station.StInput = (bool)v},
                {"ST_OUTPUT", v => station.StOutput = (bool)v},
                {"CYCLE_TIME", v => station.CurrentCycleTime = (byte)v},
                {"ALARM", v => station.Alarm = (bool)v},
                {"BLOCKED", v => station.Blocked = (bool)v},
                {"EMPTY", v => station.Empty = (bool)v},
                {"EXCLUDED", v => station.Excluded = (bool)v},
                {"RUN", v => station.Run = (bool)v},
                {"TIMEOUT", v => station.Timeout = (bool)v},
            };
        }

        private Dictionary<string, Func<object>> getGetters(AssemblyStationViewModel station)
        {
            return new Dictionary<string, Func<object>>(){
                {"ST_INPUT", () => station.StInput},
                {"ST_OUTPUT", () => station.StOutput},
                {"CYCLE_TIME", () => station.CurrentCycleTime},
                {"ALARM", () => station.Alarm},
                {"BLOCKED", () => station.Blocked},
                {"EMPTY", () => station.Empty},
                {"EXCLUDED", () => station.Excluded},
                {"RUN", () => station.Run},
                {"TIMEOUT", () => station.Timeout},
            };
        }

        #region Private Methods
        #endregion

        #region Private Fields
        #endregion
    }
}

