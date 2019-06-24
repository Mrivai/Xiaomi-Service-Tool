using MetroFramework;
using MetroFramework.Forms;
using QC.QMSLPhone;
using System;
using System.Threading;
using System.Windows.Forms;

namespace RN3_PRO_TOOLKIT
{
    public partial class ImeiChanger : MetroForm
    {
        private Phone phone = new Phone();
        private MACADD_Location macLoc;
        private int _comPortIn;
        private string imei1;
        private string imei2;
        private string bta;
        private string sn;
        private string mac;
        private string meid;
        private string fileQcn;

        private void write()
        {
            if (BackupCbx.Checked && RestrCbx.Checked)
            {

            }
            else if (CbSN.Checked)
            {
                WriteSN(SN.Text);
            }
            else if (CbImei1.Checked)
            {
                if(Imei1.Text != imei1 || Imei1.Text.Length == imei1.Length )
                {
                    WriteImei(Imei1.Text, 1);
                }
            }
            else if (CbImei2.Checked)
            {
                if (Imei2.Text != imei2 || Imei2.Text.Length == imei2.Length)
                {
                    WriteImei(Imei2.Text, 0);
                }
            }
            else if (CbMeid.Checked)
            {
                if (Meid.Text != meid || Meid.Text.Length == meid.Length)
                {
                    WriteMeid(Meid.Text.ToUpper());
                }
            }
            else if (CbMac.Checked)
            {
                if (Mac.Text != mac || Mac.Text.Length == mac.Length)
                {
                    WriteMeid(Mac.Text.ToUpper());
                }
            }
            else if (CbBt.Checked)
            {
                if (Blu.Text != bta || Blu.Text.Length == bta.Length)
                {
                    WriteBta(Blu.Text.ToUpper());
                }
            }
            phone.DisconnectServer();
            reset();
            //InitializeComponent();
            Read();
        }
        
        private uint TimeOut { get; set; }
        private uint ComPort { get; set; }
        private int ReturnDelay { get; set; }

        public ImeiChanger()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            write();
        }
        private void Backup()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select Folder For saved Backup file";
            folderBrowserDialog.ShowNewFolderButton = true;
            var ErrorMsg = "";
            var SPC = "000000";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                fileQcn = @folderBrowserDialog.SelectedPath + "\\" + "Backup.QCN";
                var result = phone.CreateQCNFile(fileQcn, SPC, out ErrorMsg);
                if (!result)
                {
                    MetroMessageBox.Show(this, ErrorMsg, "Error Happen While Backup Qcn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void WriteSN(string SN)
        {
            if(SN !=sn)
            phone.WriteSN(SN);
            phone.EFS_SyncWithWait(2000);
        }
        private void WriteImei(string newImei, int sim)
        {
            if (newImei.Length == 15)
            {
                newImei = newImei.Remove(newImei.Length - 1);
            }
            try
            {
                phone.WriteIMEI_DualSIM(newImei, sim);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private void WriteMeid(string MD)
        {
            try
            {
                phone.WriteMEIDNumber(MD);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private void WriteMac(string NewMac)
        {
            byte[] macBytes = new byte[6];
            byte[] mac = new byte[6];
            try
            {
                macBytes = ConvertStringToByteArray(NewMac, NewMac.Length);
                macLoc = MACADD_Location.NV4678;
                if (macLoc == MACADD_Location.AP_PERSIST)
                {
                    int j = 11;
                    for (int i = 0; i < mac.Length; i++)
                    {
                        mac[i] = (byte)(macBytes[j] << 4);
                        mac[i] += (byte)(macBytes[j - 1]);

                        j -= 2;
                    }

                    phone.FTM_WLAN_GEN6_SET_MAC_ADDRESS(mac);
                }
                else if (macLoc == MACADD_Location.NV4678)
                {
                    int j = 0;
                    for (int i = 0; i < mac.Length; i++)
                    {
                        mac[i] = (byte)(macBytes[j + 1] << 4);
                        mac[i] += (byte)macBytes[j];

                        j += 2;
                    }

                    phone.NVWrite(nv_items_enum_type.NV_WLAN_MAC_ADDRESS_I, mac, 128);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private void WriteBta(string NewBta)
        {
            byte[] btaBytes = new byte[6];
            byte[] bta = new byte[6];
            try
            {
                btaBytes = ConvertStringToByteArray(NewBta, NewBta.Length);
                int j = 0;
                for (int i = 0; i < bta.Length; i++)
                {
                    bta[i] = (byte)(btaBytes[j + 1] << 4);
                    bta[i] += (byte)btaBytes[j];
                    j += 2;
                }
                phone.NVWrite(nv_items_enum_type.NV_BD_ADDR_I, bta, 128);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private void Updates()
        {
            SN.Text = sn;
            Imei1.Text = imei1;
            Imei2.Text = imei2;
            Meid.Text = meid;
            Mac.Text = mac;
            Blu.Text = bta;
        }
        private void reset()
        {
            SN.Text = "";
            Imei1.Text = "";
            Imei2.Text = "";
            Meid.Text = "";
            Mac.Text = "";
            Blu.Text = "";
        }
        private void Read()
        {
            ConnectToPhoneAutoDetect();
            if (phone.IsPhoneConnected())
            {
                readSN();
                readMeid();
                readEmei();
                readMac();
                readBTa();
                Updates();
            }
        }
        private void readSN()
        {
            phone.ReadSN(out sn);
        }
        private void readMeid()
        {
            Meid_Info meidInfo;
            phone.ReadMEID(out meidInfo);
            meid = meidInfo.RR + meidInfo.MAC + meidInfo.SNR;
        }
        private void readEmei()
        {
            Imei_Info imeiInfo2 = new Imei_Info();
            Imei_Info imeiInfo1 = new Imei_Info();
            try
            {
                phone.ReadIMEI_DualSIM(out imeiInfo2, 0);
                phone.ReadIMEI_DualSIM(out imeiInfo1, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            imei1 = imeiInfo1.imei;
            imei2 = imeiInfo2.imei;
        }
        private void readMac()
        {
            byte[] macBytes = new byte[6];
            int macLen = 6;
            bool _result = true;
            try
            {
                macLoc = MACADD_Location.NV4678;
                if (macLoc == MACADD_Location.AP_PERSIST)
                {
                    macBytes = phone.FTM_WLAN_GEN6_GET_MAC_ADDRESS();
                }

                // Read WLAN MAC address in NV 4678
                else if (macLoc == MACADD_Location.NV4678)
                {
                    phone.NVRead(nv_items_enum_type.NV_WLAN_MAC_ADDRESS_I, macBytes, 128);
                }
            }
            catch (Exception ex)
            {
                _result = false;
                throw new Exception(ex.ToString());
            }
            if (_result)
            {
                ConvertByteArrayToHexString(macBytes, macLen, out mac);
            }
        }
        private void readBTa()
        {
            bool _result = true;
            byte[] bdaBytes = new byte[6];
            byte[] bdaSwappedBytes = new byte[6];
            string[] bdaString = new string[6];
            int bdaLen = 6;
            try
            {
                phone.NVRead(nv_items_enum_type.NV_BD_ADDR_I, bdaBytes, 128);
            }
            catch (Exception ex)
            {
                _result = false;
                throw new Exception(ex.ToString());
            }
            if (_result)
            {
                for (int i = 0, j = bdaLen - 1; i <= bdaLen - 1; i++, j--)
                    bdaSwappedBytes[i] = bdaBytes[j];
                ConvertByteArrayToHexString(bdaSwappedBytes, bdaLen, out bta);
                bta = bta.ToUpper();
            }
        }

        private void ConnectToPhoneAutoDetect()
        {
            phone.SetLibraryMode(LibraryModeEnum.QPhoneMS);
            phone.connectToServerAutoDetect((uint)0xffff, TimeOut);
            ComPort = phone.GetPhoneComPort();
            if (ComPort != 0)
            {
                _comPortIn = Convert.ToInt32(ComPort.ToString());
                Thread.Sleep(ReturnDelay);
            }
        }
        private enum MACADD_Location
        {
            AP_PERSIST,
            NV4678
        };
        private byte[] ConvertStringToByteArray(string InStr, int numBytes)
        {
            //NV Item value to be sent to Phone
            byte[] nvVal = new byte[numBytes];
            int[] nvArray = new int[numBytes];
            string[] stringArray = new string[numBytes];

            //            char[] chArray = InStr.ToCharArray();
            for (int i = 0; i < InStr.Length; i++)
            {
                stringArray[i] = InStr[i].ToString();
                nvArray[i] = int.Parse(stringArray[i], System.Globalization.NumberStyles.HexNumber);
            }

            int k = InStr.Length - 1;
            for (int i = 0; i < InStr.Length; i++, k--)
            {
                nvVal[i] = (byte)nvArray[k];
            }

            return nvVal;

        }
        private void ConvertByteArrayToHexString(byte[] ByteArrayIn, int StrLength, out string StringOut)
        {
            string[] outString = new string[StrLength];
            string[] tempString = new string[2];
            byte temp, temp1, temp2;

            StringOut = "";

            for (int index = 0; index < StrLength; index++)
            {
                //catch any instances where value isn;t written
                if (ByteArrayIn != null && ByteArrayIn.Length > index)
                {
                    //must break out each byte and convert so can handle cases like 0x05 
                    // where string convert will truncate the 0
                    temp = Convert.ToByte(ByteArrayIn[index]);
                    temp1 = Convert.ToByte(temp & 0x000F);
                    temp2 = Convert.ToByte((temp & 0x00F0) >> 4);

                    tempString[0] = Convert.ToString(temp1, 16);
                    tempString[1] = Convert.ToString(temp2, 16);

                    //StringOut += outString[index];
                    StringOut += tempString[1].ToUpper() + tempString[0].ToUpper();

                }
            }
        }

        private void ImeiChanger_Load(object sender, EventArgs e)
        {
            Read();
        }
    }
}
