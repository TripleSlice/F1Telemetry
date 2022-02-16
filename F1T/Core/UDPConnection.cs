using F1T.Structs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace F1T.Core
{

    /// <summary>
    /// Provides UDP information about the game
    /// </summary>
    /// https://github.com/thomz12/F12020-Telemetry
    /// https://forums.codemasters.com/topic/80231-f1-2021-udp-specification/ (Need an account to get UDP Specs)
    public class UDPConnection
    {
        // == UDP Client ===
        private UdpClient Client;

        // Delegates
        public delegate void MotionDataReceiveDelegate(PacketMotionData packet);
        public delegate void CarTelemetryDataReceiveDelegate(PacketCarTelemetryData packet);
        public delegate void CarStatusDataReceiveDelegate(PacketCarStatusData packet);
        public delegate void CarDamageDataReceiveDelegate(PacketCarDamageData packet);
        public delegate void LapDataReceiveDelegate(PacketLapData packet);
        public delegate void ParticipantDataReceiveDelegate(PacketParticipantsData packet);
        // =====
        // EventData is special
        public delegate void EventDataReceiveDelegate(PacketEventData packet);
        // Sub-types
        public delegate void EventDataReceiveSessionStartedDelegate();
        public delegate void EventDataReceiveSessionEndedDelegate();
        public delegate void EventDataReceiveDriverFastestLapDelegate(EventFastestLap data);
        public delegate void EventDataReceiveDriverRetireDelegate(EventRetirement data);
        public delegate void EventDataReceiveRaceControlEnableDRSDelegate();
        public delegate void EventDataReceiveRaceControlDisableDRSDelegate();
        public delegate void EventDataReceiveTeamatePitDelegate(EventTeamMateInPits data);
        public delegate void EventDataReceiveChequeredFlagDelegate();
        public delegate void EventDataReceiveRaceWinnerDelegate(EventRaceWinner data);
        public delegate void EventDataReceivePenaltyIssuedDelegate(EventPenalty data);
        public delegate void EventDataReceiveSpeedTrapFastestDelegate(EventSpeedTrap data);
        public delegate void EventDataReceiveStartLightsDelegate(EventStartLights data);
        public delegate void EventDataReceiveLightsOutDelegate();
        public delegate void EventDataReceiveDriveThroughServedDelegate(EventDriveThroughPentaltyServed data);
        public delegate void EventDataReceiveStopGoServedDelegate(EventStopGoPenaltyServed data);
        public delegate void EventDataReceiveFlashbackActivatedDelegate(EventFlashback data);
        public delegate void EventDataReceiveButtonStatusChangedDelegate(EventButtons data);
        // =====

        // Packet events
        public event MotionDataReceiveDelegate OnMotionDataReceive;
        public event CarTelemetryDataReceiveDelegate OnCarTelemetryDataReceive;
        public event CarStatusDataReceiveDelegate OnCarStatusDataReceive;
        public event CarDamageDataReceiveDelegate OnCarDamageDataReceive;
        public event LapDataReceiveDelegate OnLapDataReceive;
        public event ParticipantDataReceiveDelegate OnPaticipantDataReceive;
        // =====
        // EventData is special
        public event EventDataReceiveDelegate OnEventDataReceive;
        // Sub-types
        public event EventDataReceiveSessionStartedDelegate OnEventDataReceiveSessionStarted;
        public event EventDataReceiveSessionEndedDelegate OnEventDataReceiveSessionEnded;
        public event EventDataReceiveDriverFastestLapDelegate OnEventDataReceiveDriverFastestLap;
        public event EventDataReceiveDriverRetireDelegate OnEventDataReceiveDriverRetire;
        public event EventDataReceiveRaceControlEnableDRSDelegate OnEventDataReceiveRaceControlEnableDRS;
        public event EventDataReceiveRaceControlDisableDRSDelegate OnEventDataReceiveRaceControlDisableDRS;
        public event EventDataReceiveTeamatePitDelegate OnEventDataReceiveTeamatePit;
        public event EventDataReceiveChequeredFlagDelegate OnEventDataReceiveChequeredFlag;
        public event EventDataReceiveRaceWinnerDelegate OnEventDataReceiveRaceWinner;
        public event EventDataReceivePenaltyIssuedDelegate OnEventDataReceivePenaltyIssued;
        public event EventDataReceiveSpeedTrapFastestDelegate OnEventDataReceiveSpeedTrapFastest;
        public event EventDataReceiveStartLightsDelegate OnEventDataReceiveStartLights;
        public event EventDataReceiveLightsOutDelegate OnEventDataReceiveLightsOut;
        public event EventDataReceiveDriveThroughServedDelegate OnEventDataReceiveDriveThroughServed;
        public event EventDataReceiveStopGoServedDelegate OnEventDataReceiveStopGoServed;
        public event EventDataReceiveFlashbackActivatedDelegate OnEventDataReceiveFlashbackActivated;
        public event EventDataReceiveButtonStatusChangedDelegate OnEventDataReceiveButtonStatusChanged;
        // =====



        // === Singleton Instance with Thread Saftey ===
        private static UDPConnection _instance = null;
        private static object _singletonLock = new object();
        public static UDPConnection GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new UDPConnection(); }
                return _instance;
            }
        }
        // === End of Singleton


        private UDPConnection()
        {
            // TODO Gracefully exit with
            // message if UDP port is in use...
            //Client uses as receive udp client
            Client = new UdpClient(20777);

            try { Client.BeginReceive(new AsyncCallback(recv), null); }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        // === Ingestion point of data from the game ===
        // Cast the packet to its type
        // Invoke any delegate functions
        // These will then gen pushed to the events
        private void recv(IAsyncResult res)
        {
            // https://stackoverflow.com/questions/7266101/receive-messages-continuously-using-udpclient
            // https://stackoverflow.com/questions/60352529/byte-array-to-struct-udp-packet
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);

            GCHandle handle = GCHandle.Alloc(received, GCHandleType.Pinned);
            PacketHeader packetHeader = (PacketHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketHeader));

            switch (packetHeader.m_packetId)
            {
                case PacketType.CarTelemetry:
                    PacketCarTelemetryData telemetryData = (PacketCarTelemetryData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketCarTelemetryData));
                    OnCarTelemetryDataReceive?.Invoke(telemetryData);
                    break;

                case PacketType.Motion:
                    PacketMotionData motionData = (PacketMotionData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketMotionData));
                    OnMotionDataReceive?.Invoke(motionData);
                    break;

                case PacketType.CarDamage:
                    PacketCarDamageData damageData = (PacketCarDamageData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketCarDamageData));
                    OnCarDamageDataReceive?.Invoke(damageData);
                    break;

                case PacketType.CarStatus:
                    PacketCarStatusData statusData = (PacketCarStatusData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketCarStatusData));
                    OnCarStatusDataReceive?.Invoke(statusData);
                    break;

                case PacketType.LapData:
                    PacketLapData lapData = (PacketLapData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketLapData));
                    OnLapDataReceive?.Invoke(lapData);
                    break;

                case PacketType.Participants:
                    PacketParticipantsData participantData = (PacketParticipantsData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketParticipantsData));
                    OnPaticipantDataReceive?.Invoke(participantData);
                    break;

                case PacketType.Event:
                    PacketEventData eventData = (PacketEventData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketEventData));
                    OnEventDataReceive?.Invoke(eventData);

                    var eventCode = new string(eventData.m_eventStringCode);
                    switch (eventCode)
                    {
                        case EEventDataDetails.SessionStarted:
                            OnEventDataReceiveSessionStarted?.Invoke();
                            break;

                        case EEventDataDetails.SessionEnded:
                            OnEventDataReceiveSessionEnded?.Invoke();
                            break;

                        case EEventDataDetails.DriverFastestLap:
                            OnEventDataReceiveDriverFastestLap?.Invoke(eventData.m_eventDetails.fastestLap);
                            break;

                        case EEventDataDetails.DriverRetire:
                            OnEventDataReceiveDriverRetire?.Invoke(eventData.m_eventDetails.retirement);
                            break;

                        case EEventDataDetails.RaceControlEnableDRS:
                            OnEventDataReceiveRaceControlEnableDRS?.Invoke();
                            break;

                        case EEventDataDetails.RaceControlDisableDRS:
                            OnEventDataReceiveRaceControlDisableDRS?.Invoke();
                            break;

                        case EEventDataDetails.TeamatePit:
                            OnEventDataReceiveTeamatePit?.Invoke(eventData.m_eventDetails.teamMateInPits);
                            break;

                        case EEventDataDetails.ChequeredFlag:
                            OnEventDataReceiveChequeredFlag?.Invoke();
                            break;

                        case EEventDataDetails.RaceWinner:
                            OnEventDataReceiveRaceWinner?.Invoke(eventData.m_eventDetails.raceWinner);
                            break;

                        case EEventDataDetails.PenaltyIssued:
                            OnEventDataReceivePenaltyIssued?.Invoke(eventData.m_eventDetails.penalty);
                            break;

                        case EEventDataDetails.SpeedTrapFastest:
                            OnEventDataReceiveSpeedTrapFastest?.Invoke(eventData.m_eventDetails.speedTrap);
                            break;

                        case EEventDataDetails.StartLights:
                            OnEventDataReceiveStartLights?.Invoke(eventData.m_eventDetails.startLights);
                            break;

                        case EEventDataDetails.LightsOut:
                            OnEventDataReceiveLightsOut?.Invoke();
                            break;

                        case EEventDataDetails.DriveThroughServed:
                            OnEventDataReceiveDriveThroughServed?.Invoke(eventData.m_eventDetails.driveThroughPentaltyServed);
                            break;

                        case EEventDataDetails.StopGoServed:
                            OnEventDataReceiveStopGoServed?.Invoke(eventData.m_eventDetails.stopGoPenaltyServed);
                            break;

                        case EEventDataDetails.FlashbackActivated:
                            OnEventDataReceiveFlashbackActivated?.Invoke(eventData.m_eventDetails.flashback);
                            break;

                        case EEventDataDetails.ButtonStatusChanged:
                            OnEventDataReceiveButtonStatusChanged?.Invoke(eventData.m_eventDetails.buttons);
                            break;
                    }

                    break;
            }
            Client.BeginReceive(new AsyncCallback(recv), null);
        }
    }
}
