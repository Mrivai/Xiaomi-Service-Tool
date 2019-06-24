using System;

namespace RN3_PRO_TOOLKIT
{
    internal class Files
    {
        /// <summary>
        /// Gets a value of recovery.img
        /// to fix imei and baseband
        /// flash via fastboot atau edl
        /// </summary>
        public static string RootFolder
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\";
            }
        }
        public static string Su
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\Su\\su.zip";
            }
        }

        public static string TemRecovery
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\Temp_Recovery\\recovery.img";
            }
        }
        /// <summary>
        /// Gets a value of recovery.img
        /// to fix imei and baseband
        /// flash via fastboot atau edl
        /// </summary>
        public static string Recovery
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\Recovery\\recovery.img";
            }
        }
        /// <summary>
        /// Gets a value of rawprogram0.xml
        /// to fix imei and baseband
        /// flash via fastboot atau edl
        /// </summary>
        public static string Imei
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\imei\\rawprogram0.xml";
            }
        }
        /// <summary>
        /// Gets a value of NON-HLOS.bin path
        /// to fix lost signal
        /// flash via fastboot
        /// </summary>
        public static string Modem
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\modem\\NON-HLOS.bin";
            }
        }

        /// <summary>
        /// Gets a value of persist.img
        /// to fix sensor
        /// flash via fastboot atau edl
        /// </summary>
        public static string Sensor
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\sensor\\persist.img";
            }
        }
        public static string Backup
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Backup\\";
            }
        }
        public static string Ubl
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "File\\ubl\\rawprogram0.xml";
            }
        }
    }
    internal class Tool
    {
        public static string ImeiChanger
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Tool\\QUALCOMM.exe";
            }
        }
    }
}
