﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace UnifiedAutomation.Sample
{
    public class AssemblyStationViewModel : BaseViewModel
    {
        private bool _stInput;
        private bool _stOutput;

        private byte _currentCycleTime = 0;
        private byte _totalCycleTime = 30;
        private byte _timeoutSec = 60;

        private bool _empty = true;
        private bool _run;
        private bool _blocked;
        private bool _alarm;
        private bool _excluded;
        private bool _timeout;

        public ObjectNode Parent;
        public System.Threading.Timer Timer;

        public AssemblyStationViewModel(ObjectNode parent)
        {
            Parent = parent;
        }

        public void Start()
        {
            Timer = new System.Threading.Timer((callback) => Tick(), null, 1000, 1000);
        }

        public bool StInput
        {
            get { return _stInput; }

            set
            {
                if (value == _stInput) return;
                _stInput = value;
                RaisePropertyChanged("StInput");
            }
        }

        public bool StOutput
        {
            get { return _stOutput; }

            set
            {
                if (value == _stOutput) return;
                _stOutput = value;
                RaisePropertyChanged("StOutput");
            }
        }

        public byte CurrentCycleTime
        {
            get { return _currentCycleTime; }

            set
            {
                if (value == _currentCycleTime) return;
                _currentCycleTime = value;
                RaisePropertyChanged("CurrentCycleTime");
            }
        }

        public byte TotalCycleTime
        {
            get { return _totalCycleTime; }

            set
            {
                if (value == _totalCycleTime) return;
                _totalCycleTime = value;
                RaisePropertyChanged("TotalCycleTime");
            }
        }

        public byte TimeoutSec
        {
            get { return _timeoutSec; }

            set
            {
                if (value == _timeoutSec) return;
                _timeoutSec = value;
                RaisePropertyChanged("TimeoutSec");
            }
        }

        public bool Empty
        {
            get { return _empty; }

            set
            {
                if (value == _empty) return;
                _empty = value;
                RaisePropertyChanged("Empty");
            }
        }

        public bool Run
        {
            get { return _run; }

            set
            {
                if (value == _run) return;
                _run = value;
                RaisePropertyChanged("Run");
            }
        }

        public bool Blocked
        {
            get { return _blocked; }

            set
            {
                if (value == _blocked) return;
                _blocked = value;
                RaisePropertyChanged("Blocked");
            }
        }

        public bool Alarm
        {
            get { return _alarm; }

            set
            {
                if (value == _alarm) return;
                _alarm = value;
                RaisePropertyChanged("Alarm");
            }
        }

        public bool Excluded
        {
            get { return _excluded; }

            set
            {
                if (value == _excluded) return;
                _excluded = value;
                RaisePropertyChanged("Excluded");
            }
        }

        public bool Timeout
        {
            get { return _timeout; }

            set
            {
                if (value == _timeout) return;
                _timeout = value;
                RaisePropertyChanged("Timeout");
            }
        }

        public void Tick()
        {
            if (Alarm || Excluded || Timeout)
                return;

            if (Empty)
            {
                if (StInput)
                {
                    Empty = false;
                    Run = true;
                }
            }
            else if (Run)
            {
                if (!StInput)
                    return;

                ++CurrentCycleTime;
                if (CurrentCycleTime >= TimeoutSec)
                {
                    Run = false;
                    Timeout = true;
                }
                else if (CurrentCycleTime >= TotalCycleTime)
                {
                    StInput = false;
                    Run = false;
                    if (!StOutput)
                    {
                        CurrentCycleTime = 0;
                        StOutput = true;
                        Empty = true;
                    }
                    else
                    {
                        Blocked = true;
                    }
                }
            }
            else if (Blocked)
            {
                if (!StOutput)
                {
                    Blocked = false;
                    Run = true;
                }
            }
        }
    }
}
