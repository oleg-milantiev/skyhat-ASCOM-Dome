﻿//tabs=4
// --------------------------------------------------------------------------------
// ASCOM Dome driver for SkyHat
//
// Description:	ASCOM драйвер мотокрышки SkyHat
//
// Implements:	ASCOM Dome interface version: 2.0
// Author:		(mo) Oleg Milantiev <oleg@milantiev.com>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 18-11-2022	mo	2.0.0   Все настройки вынес в Windows App
// 29-10-2020	mo	1.8.0   Добавил стабильности
// 08-09-2020	mo	1.7.0   Версия с парой моторов
// 17-04-2020	mo	1.2.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------


#define Dome

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;

namespace ASCOM.SkyHat
{
    /// <summary>
    /// ASCOM Dome Driver for SkyHat.
    /// </summary>
    [Guid("d011f976-a1c5-4ece-8a12-011e741c3f2e")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Dome : IDomeV2
    {
        internal const int DEBUG = 3;
        // 0 - none
        // 1 - info
        // 2 - verbose
        // 3 - debug

        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal const string driverID = "ASCOM.SkyHat.Dome";

        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        internal const string driverDescription = "MO SkyHat Dome Driver";

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        /// <summary>
        /// COM-port
        /// </summary>
        public Serial serial = new Serial();

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        public bool connectedState;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal static TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyHat"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Dome()
        {
            tl = new TraceLogger("", "SkyHat");
            tl.Enabled = true;
            Properties.Settings.Default.Reload();

            if (DEBUG >= 3)
                LogMessage("Dome", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object

            if (DEBUG >= 3)
                LogMessage("Dome", "Completed initialisation");
        }


        //
        // PUBLIC COM INTERFACE IDomeV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            if (DEBUG >= 1)
                LogMessage("SetupDialog", "Start");

            using (SetupDialogForm F = new SetupDialogForm(this))
            {
                var result = F.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.Save();

                    SerialDisconnect();
                }
            }

        }

        public ArrayList SupportedActions
        {
            get
            {
                LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
            // DO NOT have both these sections!  One or the other
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
            // DO NOT have both these sections!  One or the other
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        private bool serialSend(byte[] package)
        {
            if (!serial.Connected)
                return false;

            try
            {
                serial.ClearBuffers();
                
                if (DEBUG >= 3)
                    LogMessage("serialSend", "Start");
            }
            catch (NotConnectedException Ex)     // objSerial.ClearBuffers();
            {
                LogMessage("serialSend: NotConnectedException", Ex.ToString());
                throw new ASCOM.NotConnectedException("Serial port connection error", Ex);
            }
            catch (ASCOM.DriverException Ex)
            {
                LogMessage("serialSend: ASCOM.DriverException when clearing serial port buffers", "ERROR: Possible serial port disconnect.\n" + Ex);
                throw new ASCOM.DriverException("Cannot connect to controller", Ex);
            }
            catch (Exception Ex)            // objSerial.ClearBuffers();
            {
                LogMessage("serialSend: Exception", Ex.ToString());
                throw new ASCOM.DriverException("Serial port connection error", Ex);
            }

            try
            {
                serial.TransmitBinary(package);
            }
            catch (UnauthorizedAccessException Ex)
            {
                LogMessage("serialSend: NotConnectedException", Ex.ToString());
                throw new ASCOM.NotConnectedException("Serial port connection error", Ex);
            }
            catch (ASCOM.DriverException Ex)
            {
                LogMessage("serialSend: ASCOM.DriverException when trying to transmit data", "ERROR: Possible serial port disconnect.\n" + Ex);
                throw new ASCOM.DriverException("Cannot connect to controller", Ex);
            }
            catch (Exception Ex)          // objSerial.Transmit(command);
            {
                LogMessage("serialSend: Exception", Ex.ToString());
                throw new ASCOM.DriverException("Serial port connection error", Ex);
            }

            LogMessage("serialSend", "Sent package to serial:"+ Encoding.ASCII.GetString(package));
            
            return true;
        }

        private bool SerialCommand_Close()
        {
            return serialSend(new byte[] { (byte)'c', (byte)'c', (byte)'a' });
        }

        private bool SerialCommand_Open()
        {
            return serialSend(new byte[] { (byte)'c', (byte)'o', (byte)'a' });
        }
        
        private bool SerialCommand_Abort()
        {
            return serialSend(new byte[] { (byte)'c', (byte)'a' });
        }

        /// <summary>
        /// Отсылает команду GET на устройство. Данные помещает в this.*
        /// </summary>
        public bool SerialCommand_GetEEPROM()
        {
            if (!serialSend(new byte[] { (byte)'c', (byte)'e' }))
            {
                return false;
            }

            byte[] serialBuf = new byte[14];

            serialBuf = serial.ReceiveCountedBinary(14);

            if (DEBUG >= 2)
                LogMessage("serialCommandGetEEPROM: Receive serial package", "Length:" + serialBuf.Length.ToString());

            //- byte start = 0xEE
            //- byte, current: Текущий ток в ADU датчика
            //- byte, timeout: Остаток (в секундах) до наступления ошибки по таймауту
            //- byte, moveLeftTo: Куда движется левый мотор ('c', 'o' или 's')
            //- byte, moveRightTo: Куда движется правый мотор ('c', 'o' или 's')
            //- byte, statusLeft: Статус левой крышки, см. ниже
            //- byte, statusRight: Статус правой крышки, см. ниже
            //- byte, light: Статус лампочки 1/0
            //- byte, speedLeft: скорость левой крышки
            //- byte, speedRight: скорость левой крышки
            //- byte stop = 0x00

            part = (char) serialBuf[1];
            // ignore package tail

            return true;
        }

        private char part;


        /// <summary>
        /// Отсылает команду GET на устройство. Данные помещает в this.*
        /// </summary>
        public bool SerialCommand_Get()
        {
            if (!serialSend(new byte[] { (byte)'c', (byte)'g' }))
            {
                return false;
            }

            byte[] serialBuf = new byte[11];

            serialBuf = serial.ReceiveCountedBinary(11);

            if (DEBUG >= 2)
                LogMessage("serialCommandGet: Receive serial package", "Length:" + serialBuf.Length.ToString());

            //- byte start = 0xEE
            //- byte, current: Текущий ток в ADU датчика
            //- byte, timeout: Остаток (в секундах) до наступления ошибки по таймауту
            //- byte, moveLeftTo: Куда движется левый мотор ('c', 'o' или 's')
            //- byte, moveRightTo: Куда движется правый мотор ('c', 'o' или 's')
            //- byte, statusLeft: Статус левой крышки, см. ниже
            //- byte, statusRight: Статус правой крышки, см. ниже
            //- byte, light: Статус лампочки 1/0
            //- byte, speedLeft: скорость левой крышки
            //- byte, speedRight: скорость левой крышки
            //- byte stop = 0x00

            current = serialBuf[1];
            timeout = serialBuf[2];
            moveLeftTo = serialBuf[3];
            moveRightTo = serialBuf[4];
            statusLeft = serialBuf[5];
            statusRight = serialBuf[6];
            statusLight = serialBuf[7];
            speedLeft = serialBuf[8];
            speedRight = serialBuf[9];

            if (DEBUG >= 3)
            {
                LogMessage("SerialCommand_Get: current", current.ToString());
                LogMessage("SerialCommand_Get: timeout", timeout.ToString());
                LogMessage("SerialCommand_Get: moveLeftTo", moveLeftTo.ToString());
                LogMessage("SerialCommand_Get: moveRightTo", moveRightTo.ToString());
                LogMessage("SerialCommand_Get: statusLeft", statusLeft.ToString());
                LogMessage("SerialCommand_Get: statusRight", statusRight.ToString());
                LogMessage("SerialCommand_Get: statusLight", statusLight.ToString());
                LogMessage("SerialCommand_Get: speedLeft", speedLeft.ToString());
                LogMessage("SerialCommand_Get: speedRight", speedRight.ToString());
            }

            return true;
        }

        byte current, timeout, moveLeftTo, moveRightTo, speedLeft, speedRight;

        public bool SerialConnect()
        {
            LogMessage("Connected", "Connecting to port " + Properties.Settings.Default.ComPortString);
            serial.PortName = Properties.Settings.Default.ComPortString;
            serial.Speed = SerialSpeed.ps115200;

            if (string.IsNullOrEmpty(serial.PortName))
            {
                LogMessage("Connected", "Cannot connect to COM port: Null");
               
                return false;
            }

            try
            {
                serial.DTREnable = true;
                serial.RTSEnable = true;

                serial.ReceiveTimeout = 5;
                serial.ReceiveTimeoutMs = 5000;

                serial.Connected = true;
                Thread.Sleep(2000);

                LogMessage("Connected: ", "serial.Connected is success");

                return true;
            }
            catch (UnauthorizedAccessException Ex)
            {
                LogMessage("Connected NotConnectedException", Ex.ToString());
                serial.DTREnable = false;
                serial.RTSEnable = false;
                
                return false;
            }
            catch (Exception Ex)
            {
                LogMessage("Connected", "Cannot connect to COM port: Missing?");
                serial.Connected = false;
                serial.DTREnable = false;
                serial.RTSEnable = false;
                
                return false;
            }
        }

        public void SerialDisconnect()
        {
            LogMessage("SerialDisconnect", "Disconnecting from port " + Properties.Settings.Default.ComPortString);

            serial.Connected = false;
            connectedState = false;
            serial.DTREnable = false;
            serial.RTSEnable = false;
            
            serial.Dispose();
            serial = new Serial();
        }
        
        public bool Connected
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("Connected", "Get {0}", IsConnected);

                return IsConnected;
            }
            set
            {
                if (value == IsConnected)
                    return;

                LogMessage("Connected Set", value.ToString());

                if (value)
                {
                    LogMessage("START", "Connect Started");

                    SerialConnect();

                    if (serial.Connected == true)
                    {
                        LogMessage("Connected:", "objSerial.Connected = true");
                        connectedState = true;

                        // wait for time specified in DelayOnConnect
                        //System.Threading.Thread.Sleep(1000);

                        LogMessage("Connected", Convert.ToString(serial.Connected));

                        if (!SerialCommand_GetEEPROM())
                        {
                            SerialDisconnect();

                            return;
                        }

                        if (!SerialCommand_Get())
                        {
                            SerialDisconnect();

                            return;
                        }
                    }
                }
                else
                {
                    SerialDisconnect();
                }
            }
        }

        public string Description
        {
            get
            {
                LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = "SkyHat ASCOM Driver. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);

                LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            get
            {
                LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        public string Name
        {
            get
            {
                string name = "MO SkyHat ASCOM Driver";
                LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region IDome Implementation

        byte statusLeft, statusRight, statusLight;

        public void AbortSlew()
        {
            LogMessage("AbortSlew", "Completed");
            SerialCommand_Abort();
        }

        public double Altitude
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("Altitude Get", "Not implemented");

                throw new ASCOM.PropertyNotImplementedException("Altitude", false);
            }
        }

        public bool AtHome
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("AtHome Get", "Not implemented");

                throw new ASCOM.PropertyNotImplementedException("AtHome", false);
            }
        }

        public bool AtPark
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("AtPark Get", "Not implemented");

                throw new ASCOM.PropertyNotImplementedException("AtPark", false);
            }
        }

        public double Azimuth
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("Azimuth Get", "Not implemented");

                throw new ASCOM.PropertyNotImplementedException("Azimuth", false);
            }
        }

        public bool CanFindHome
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanFindHome Get", false.ToString());

                return false;
            }
        }

        public bool CanPark
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanPark Get", false.ToString());

                return false;
            }
        }

        public bool CanSetAltitude
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanSetAltitude Get", false.ToString());

                return false;
            }
        }

        public bool CanSetAzimuth
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanSetAzimuth Get", false.ToString());

                return false;
            }
        }

        public bool CanSetPark
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanSetPark Get", false.ToString());

                return false;
            }
        }

        public bool CanSetShutter
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanSetShutter Get", true.ToString());

                return true;
            }
        }

        public bool CanSlave
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanSlave Get", false.ToString());

                return false;
            }
        }

        public bool CanSyncAzimuth
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("CanSyncAzimuth Get", false.ToString());

                return false;
            }
        }

        public void CloseShutter()
        {
            SerialCommand_Close();
            LogMessage("CloseShutter", "Shutter has been closed");
        }

        public void FindHome()
        {
            LogMessage("FindHome", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("FindHome");
        }

        public void OpenShutter()
        {
            SerialCommand_Open();
            LogMessage("OpenShutter", "Shutter has been opened");
        }

        public void Park()
        {
            LogMessage("Park", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Park");
        }

        public void SetPark()
        {
            LogMessage("SetPark", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SetPark");
        }

        public ShutterState ShutterStatus
        {
            get
            {
                SerialCommand_Get();

                switch (part)
                {
                    default:
                        if (DEBUG >= 3)
                            LogMessage("ShutterStatus", "unknown part: "+ part);

                        if (DEBUG >= 1)
                            LogMessage("ShutterStatus", ShutterState.shutterError.ToString());

                        return ShutterState.shutterError;

                    case 'l':
                        switch (moveLeftTo)
                        {
                            case (byte)'s':
                                switch (statusLeft)
                                {
                                    case (byte)'c':
                                        if (DEBUG >= 1)
                                            LogMessage("ShutterStatus", ShutterState.shutterClosed.ToString());

                                        return ShutterState.shutterClosed;

                                    case (byte)'o':
                                        if (DEBUG >= 1)
                                            LogMessage("ShutterStatus", ShutterState.shutterOpen.ToString());

                                        return ShutterState.shutterOpen;

                                    // case (byte)'g': // не может быть GAP при открытии только одной створки
                                    // case (byte)'t': // ошибка таймаута
                                    // case (byte)'u': // начальный непонятный статус
                                    default:
                                        if (DEBUG >= 1)
                                            LogMessage("ShutterStatus", ShutterState.shutterError.ToString());

                                        return ShutterState.shutterError;
                                }

                            case (byte)'o':
                                if (DEBUG >= 1)
                                    LogMessage("ShutterStatus", ShutterState.shutterOpening.ToString());

                                return ShutterState.shutterOpening;

                            case (byte)'c':
                                if (DEBUG >= 1)
                                    LogMessage("ShutterStatus", ShutterState.shutterClosing.ToString());

                                return ShutterState.shutterClosing;
                        }
                        break;

                    case 'r':
                        switch (moveRightTo)
                        {
                            case (byte)'s':
                                switch (statusRight)
                                {
                                    case (byte)'c':
                                        if (DEBUG >= 1)
                                            LogMessage("ShutterStatus", ShutterState.shutterClosed.ToString());

                                        return ShutterState.shutterClosed;

                                    case (byte)'o':
                                        if (DEBUG >= 1)
                                            LogMessage("ShutterStatus", ShutterState.shutterOpen.ToString());

                                        return ShutterState.shutterOpen;

                                    // case (byte)'g': // не может быть GAP при открытии только одной створки
                                    // case (byte)'t': // ошибка таймаута
                                    // case (byte)'u': // начальный непонятный статус
                                    default:
                                        if (DEBUG >= 1)
                                            LogMessage("ShutterStatus", ShutterState.shutterError.ToString());

                                        return ShutterState.shutterError;
                                }

                            case (byte)'o':
                                if (DEBUG >= 1)
                                    LogMessage("ShutterStatus", ShutterState.shutterOpening.ToString());

                                return ShutterState.shutterOpening;

                            case (byte)'c':
                                if (DEBUG >= 1)
                                    LogMessage("ShutterStatus", ShutterState.shutterClosing.ToString());

                                return ShutterState.shutterClosing;
                        }

                        break;

                    case 'b':
                        // TODO сделал left и right створки. Не проверял both

                        if ((statusLeft == 't') || (statusRight == 't'))
                        {
                            if (DEBUG >= 1)
                                LogMessage("ShutterStatus", ShutterState.shutterError.ToString());

                            return ShutterState.shutterError;
                        }

                        if ((moveLeftTo == 's') && (moveRightTo == 's'))
                        {
                            if ((statusLeft == 'o') && (statusRight == 'o'))
                            {
                                if (DEBUG >= 1)
                                    LogMessage("ShutterStatus", ShutterState.shutterOpen.ToString());

                                return ShutterState.shutterOpen;
                            }
                            if ((statusLeft == 'c') && (statusRight == 'c'))
                            {
                                if (DEBUG >= 1)
                                    LogMessage("ShutterStatus", ShutterState.shutterClosed.ToString());

                                return ShutterState.shutterClosed;
                            }

                            if (DEBUG >= 1)
                                LogMessage("ShutterStatus", ShutterState.shutterError.ToString());

                            return ShutterState.shutterError;
                        }

                        if ((moveLeftTo == 'o') || (moveRightTo == 'o'))
                        {
                            if (DEBUG >= 1)
                                LogMessage("ShutterStatus", ShutterState.shutterOpening.ToString());

                            return ShutterState.shutterOpening;
                        }
                        if ((moveLeftTo == 'c') || (moveRightTo == 'c'))
                        {
                            if (DEBUG >= 1)
                                LogMessage("ShutterStatus", ShutterState.shutterClosing.ToString());

                            return ShutterState.shutterClosing;
                        }
                        break;
                }

                if (DEBUG >= 1)
                    LogMessage("ShutterStatus", ShutterState.shutterError.ToString());

                return ShutterState.shutterError;
            }
        }

        public bool Slaved
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("Slaved Get", false.ToString());

                return false;
            }
            set
            {
                if (DEBUG >= 1)
                    LogMessage("Slaved Set", "not implemented");
                
                throw new ASCOM.PropertyNotImplementedException("Slaved", true);
            }
        }

        public void SlewToAltitude(double Altitude)
        {
            if (DEBUG >= 2)
                LogMessage("SlewToAltitude", "Not implemented");

            throw new ASCOM.MethodNotImplementedException("SlewToAltitude");
        }

        public void SlewToAzimuth(double Azimuth)
        {
            if (DEBUG >= 2)
                LogMessage("SlewToAzimuth", "Not implemented");

            throw new ASCOM.MethodNotImplementedException("SlewToAzimuth");
        }

        public bool Slewing
        {
            get
            {
                if (DEBUG >= 3)
                    LogMessage("Slewing Get", false.ToString());

                return false;
            }
        }

        public void SyncToAzimuth(double Azimuth)
        {
            if (DEBUG >= 2)
                LogMessage("SyncToAzimuth", "Not implemented");

            throw new ASCOM.MethodNotImplementedException("SyncToAzimuth");
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Dome";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }
        #endregion
    }
}
