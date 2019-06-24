using MetroFramework;
using MetroFramework.Forms;
using Mrivai;
using Mrivai.Pelitabangsa;
using Mrivai.Pelitabangsa.Modul;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace RN3_PRO_TOOLKIT
{
    public partial class MainMenu : MetroForm
    {
        XiaomiController xiaomi;
        Flash flash;
        Device devices;
        SpeechSynthesizer kenzo = new SpeechSynthesizer();
        private Point pos;
        private Point dest;
        private Point coordinates_1;
        private Point coordinates_2;
        private string resX = "1080";
        private string resY = "1920";
        private bool enable_touch;
        private bool fast_motion;
        private bool complete_flag;
        MinicapStream minicap = MinicapStream.Instance;
        protected bool _validasi = false;
        private string filename;
        private Thread splash;
        private List<Apps> apklist;

        public MainMenu()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MetroMessageBox.Show(this, "You can only have one instance of the toolkit running at a time!", "Why would you ever need more than one open I mean seriously wtf.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            splash = new Thread(new ThreadStart(SplashThread));
            splash.Start();
            InitializeComponent();
            xiaomi = XiaomiController.Instance;
            kenzo.SetOutputToDefaultAudioDevice();
            kenzo.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
        }

        private void SplashThread()
        {
            Application.Run(new Splash());
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            splash.Abort();
            timer2.Interval = 500;
            timer2.Enabled = true;
            kenzo.SpeakAsync("system online and ready");
            xiaomi.log("system online and ready", null, null);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            check();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            check();
        }
        //check connected device using timer
        private void check()
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }
        //check connected device with specific state
        private bool autodetect(DeviceState Tstate)
        {
            bool res = false;
            timer2.Enabled = true;
            if (devices.State == Tstate)
            {
                res = true;
            }
            else if (devices.State != Tstate)
            {
                timer2.Enabled = true;
                res = false;
            }
            return res;
        }

        private void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            devices = xiaomi.GetConnectedDevice();
            if (xiaomi.HasConnectedDevices) { e.Result = "OK"; }
            else if (!xiaomi.HasConnectedDevices) { e.Result = "Fail"; }
        }

        private void worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if ((string)e.Result == "OK")
            {
                timer2.Enabled = false;
                kenzo.SpeakAsync("Device connected successfully");
                
                Status.Text = devices.State.ToString();
                if (devices.State == DeviceState.ONLINE)
                {
                    timer1.Enabled = true;
                    enable_touch = true;
                    fast_motion = false;
                    getApplist();
                    update();
                    //Disp();
                }
                else if (devices.State == DeviceState.EDL || devices.State == DeviceState.FASTBOOT)
                {
                    FlashResult.Visible = true;
                    FlashPort.Text = devices.SerialNumber;
                    FlashResult.Text = "READY";
                    FlashResult.Visible = true;
                    FlashPort.Visible = true;
                    Stt.Visible = true;
                }
                else if (devices.State == DeviceState.UNAUTHORIZED)
                {
                    kenzo.SpeakAsync("Sir, I nedd your Authorization, Please Accept");
                }
                else if (devices.State != DeviceState.EDL)
                {
                    xiaomi.log("Device Connected :", devices.SerialNumber, null);
                }
            }
            else if ((string)e.Result == "Fail")
            {
                Status.Text = "OFFLINE";
            }
        }

        //flashing button
        private void FlashBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.EDL || devices.State == DeviceState.FASTBOOT)
            {
                var type = "";
                FlashSpiner.Visible = true;
                progress.Visible = true;
                ProgressText.Visible = true;
                FlashTime.Visible = true;
                Stt.Visible = true;

                if (CbFlashall.Checked)
                {
                    type = FlashType.CleanAll;
                }
                else if (CbKeepData.Checked)
                {
                    type = FlashType.SaveUserData;
                }
                else if (CbFlashLock.Checked)
                {
                    type = FlashType.CleanAllAndLock;
                }
                button24.Enabled = false;
                FlashBtn.Enabled = false;
                Flashtimer.Enabled = true;
                try
                {
                    flash = new Flash(devices);
                    bool isdone = flash.IsDone;

                    if (!isdone)
                    {
                        flash.StartTime = DateTime.Now;
                        flash.Status = "Flashing";
                        flash.Progress = 0.0f;
                        flash.IsUpdate = true;
                        flash.SwPath = RomPath.Text;
                        flash.Flashtype = type;
                        new Thread(new ThreadStart(flash.StartFlash))
                        {
                            IsBackground = true
                        }.Start();
                    }
                }
                catch (Exception ex)
                {
                    xiaomi.log("Start Flashing", ex.Message, ex.StackTrace);
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device In EDL Mode or Fastboot Mode and try again");
            }
        }
        //update flashing status using timer
        private void Flashtimer_Tick(object sender, EventArgs e)
        {
            FlashResult.Text = flash.Result;
            FlashResult.Invalidate();
            status(flash.Status);
            ProgressText.Text = (flash.Progress * 100.0).ToString() + "%";
            if (progress.Value == (int)(flash.Progress * 100.0))
            {
                if ((int)(flash.Progress * 100.0) < 100)
                    flash.Progress += 3f / 1000f;
                progress.Value = (int)(flash.Progress * 100.0);
            }
            else
            {
                progress.Value = (int)(flash.Progress * 100.0);
            }

            ProgressText.Text = progress.Value.ToString();

            if (flash.StartTime > DateTime.MinValue)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(flash.StartTime);
                string time = timeSpan.TotalSeconds.ToString();
                time = time.Substring(0, time.Length - 6);
                FlashTime.Text = string.Format("{0}s", time);
            }
            if (flash.Error)
            {
                Flashtimer.Enabled = false;
                flash.IsUpdate = false;
                button24.Enabled = true;
                FlashBtn.Enabled = true;
                kenzo.SpeakAsync("Sir, Error Detected, " + flash.Status.Replace("_", " ") + " Please try again");
                FlashPort.Visible = false;
                FlashSpiner.Visible = false;
                button24.Enabled = true;
                FlashBtn.Enabled = true;
                progress.Visible = false;
                ProgressText.Visible = false;
                FlashTime.Visible = false;
                FlashResult.Visible = false;
                status(null);
                Stt.Visible = false;
                check();
                return;
            }
            if (flash.Result == "success")
            {
                flash.IsUpdate = false;
                FlashResult.BackColor = Color.LightGreen;
                Flashtimer.Enabled = false;
                kenzo.SpeakAsync("Flashing Device Succesfuly");
                FlashPort.Visible = false;
                FlashSpiner.Visible = false;
                button24.Enabled = true;
                FlashBtn.Enabled = true;
                progress.Visible = false;
                ProgressText.Visible = false;
                FlashTime.Visible = false;
                FlashResult.Visible = false;
                Stt.Visible = false;
                check();
            }
        }
        //select firmware path
        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    RomPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        //chek bootloader info
        private void BLInfo_Click(object sender, EventArgs e)
        {
            unlockPnlchanged(BLInfo);
            if (!xiaomi.HasConnectedDevices)
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            else if (devices.State == DeviceState.FASTBOOT)
                if (!devices.IsUnlocked)
                    kenzo.SpeakAsync("sir , Device Locked Down");
                else
                    kenzo.SpeakAsync("Sir , Device already Unlocked");
            else
                kenzo.SpeakAsync("Sir , Make sure devices in Fastboot Mode before using this feature");
        }

        //reboot
        private void rebootBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            else if (devices.State == DeviceState.FASTBOOT || devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
                devices.Reboot();
            else if (devices.State == DeviceState.EDL)
                kenzo.SpeakAsync("Sir , please press and hold power button till the device booting up");
            timer2.Enabled = true;
        }

        //reboot to fastboot
        private void reboot2FastbootBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            else if (devices.State == DeviceState.ONLINE)
                devices.RebootBootloader();
            timer2.Enabled = true;
        }
        //reboot recovery
        private void reboot2recoveryBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.SIDELOAD)
                devices.RebootRecovery();
            else if (devices.State == DeviceState.FASTBOOT || devices.State == DeviceState.EDL)
                kenzo.SpeakAsync("Sir , Please Press and Hold Power button and Volume UP button together till devices boot into recovery");
            timer2.Enabled = true;
        }
        //reboot temp recovery
        private void TempRecoveryBtn_Click(object sender, EventArgs e)
        {
            var img = Files.TemRecovery;

            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.FASTBOOT)
            {
                if (devices.IsUnlocked)
                {
                    devices.RebootTempRecovery(img);
                    timer2.Enabled = true;
                }
                else
                {
                    kenzo.SpeakAsync("Sir , Make sure device bootloader unlocked before using this feature");
                }
            }
            else if (devices.State != DeviceState.FASTBOOT)
            {
                kenzo.SpeakAsync("Sir , Make sure device is on bootloader mode before using this feature and try again");
            }
        }
        //reboot edl
        private void reboot2Edl_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            else if (devices.State == DeviceState.FASTBOOT || devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
                devices.RebootEdl();
            else if (devices.State == DeviceState.EDL)
                kenzo.SpeakAsync("Sir , device already in E.D.L mode");
            timer2.Enabled = true;
        }
        //remove screenlock 
        private void screnlockBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE)
            {
                if (!devices.HasRoot)
                {
                    kenzo.SpeakAsync("Sir make sure device hasbeen root before use this and try again");
                }
                else
                {
                    devices.RemoveScreenLock();
                    kenzo.SpeakAsync("Sir Screenlock hasbeen remove");
                }
            }
            else if (devices.State == DeviceState.SIDELOAD)
            {
                devices.RemoveScreenLock();
                kenzo.SpeakAsync("Sir Screenlock hasbeen remove");
            }
            else
            {
                kenzo.SpeakAsync("Sir, make sure device is online mode, or, sideload mode and try again");
            }
        }
        //fastboot oem unlock
        private void UnlockBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.FASTBOOT)
            {
                devices.OemUnlock();
                kenzo.SpeakAsync("Sir , device bootloader hasbeen un locked");
            }
            else
            {
                kenzo.SpeakAsync("Sir, make sure your device is on fastboot mode and try again");
            }
        }
        //fastboot oem relock
        private void relockBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.FASTBOOT)
            {
                devices.Oemlock();
                kenzo.SpeakAsync("Sir , device bootloader hasbeen locked");
            }
            else
            {
                kenzo.SpeakAsync("Sir, make sure your device is on fastboot mode and try again");
            }
        }
        //adb restore backup
        private void AdbRestoreBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE)
            {
                var p = RestPath.Text;
                devices.AdbRestore(p);
                kenzo.SpeakAsync("Sir device Has been Restore");
            }
            else
            {
                kenzo.SpeakAsync("Sir, make sure your device is online and try again");
            }
        }
        //adb backup
        private void AdbBckupBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE)
            {
                var name = BckupPath.Text;
                bool apk = false;
                bool shared = false;
                bool system = false;

                if (BckupPath.Text == null)
                {
                    kenzo.SpeakAsync("Sir , please insert the destination path and try again");
                }
                else
                {
                    if (CbApk.Checked)
                    {
                        apk = true;
                    }
                    if (CbShared.Checked)
                    {
                        shared = true;
                    }
                    if (CbSystem.Checked)
                    {
                        system = true;
                    }
                    devices.AdbBackup(apk, shared, system, name);
                    kenzo.SpeakAsync("Sir Device Has been Backup");
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir, make sure your device is online and try again");
            }
        }
        //EDL Backup
        private void EdlBckup_Click(object sender, EventArgs e)
        {
            var path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select Folder For saved Backup file";
            folderBrowserDialog.ShowNewFolderButton = true;
            List<string> backup = new List<string>();
            //folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Backup\\";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                foreach (object chekeditem in PartListBox.CheckedItems)
                {
                    backup.Add(chekeditem.ToString());
                }
                devices.EdlBackupList(backup, path);
            }

            kenzo.SpeakAsync("Sir, Selected partition hasbeen Backup");
        }
        //
        private void AppUninBtn_Click(object sender, EventArgs e)
        {
            foreach (object chekeditem in AppsList.CheckedItems)
            {
                foreach (Apps item in apklist)
                {
                    if (chekeditem.ToString() == item.name)
                    {
                        devices.UninstallApk(item.packagename);
                    }
                }
            }
        }
        //unofficial unlock method
        private void UnOfficialUnlockBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.EDL)
            {
                devices.UblUnofficial();
            }
            else if (devices.State != DeviceState.EDL)
            {
                kenzo.SpeakAsync("Sir , Make sure device on EDL mode and try again ");
            }
        }

        private void DiagmodeBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
            {
                if (!devices.IsDiag)
                {
                    kenzo.SpeakAsync("activate diagnostic mode");
                    devices.EnableDiagMode();
                    kenzo.SpeakAsync("diagnostic mode has been activated");
                }
                else if (devices.IsDiag)
                {
                    kenzo.SpeakAsync("deactivate diagnostic mode");
                    devices.DisableDiagMode();
                    kenzo.SpeakAsync("Sir , Diagnotic still active till you reboot device, Please reboot device to take a change");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
            {
                if (devices.IsDiag)
                {
                    kenzo.SpeakAsync("diagnostic mode has been activated");
                }
                else
                {
                    kenzo.SpeakAsync("Sir, Diagnostic mode is not active");
                }
            }
        }

        private void FlashImgBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.FASTBOOT)
            {
                var filter1 = "*.img";

                if (Regex.Match(filename, filter1, RegexOptions.IgnoreCase | RegexOptions.Singleline).Success)
                {
                    devices.FastbootFlashImg(@filename);
                    kenzo.SpeakAsync("Flashing Img File done");
                }
                else
                {
                    kenzo.SpeakAsync("Sir , Make sure filename and extension is correct");
                }
            }
            else if (devices.State == DeviceState.EDL)
            {
                var filter = ".img";

                if (Regex.Match(filename, filter).Success)
                {
                    devices.EdlFlasher(@filename);
                    kenzo.SpeakAsync("Flashing Img File done");
                }
                else
                {
                    kenzo.SpeakAsync("Sir , Make sure filename and extension is correct");
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , this Feature only support on fastboot mode and edl mode");
            }
        }
        // push file
        private void pushBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
            {
                if (filename != null)
                {
                    devices.PushFile(@filename, PushTo.Text);
                }
                else
                {
                    kenzo.SpeakAsync("Sir , Make sure filename and extension is correct");
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device on online mode");
            }
        }
        //pull file
        private void PullBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
            {
                if (filename != null)
                {
                    devices.PullFile(PullFrom.Text, @PullFrom.Text);
                }
                else
                {
                    kenzo.SpeakAsync("Sir , Make sure filename and extension is correct");
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device on online mode");
            }
        }
        //sideload
        private void SdlBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.SIDELOAD)
            {
                if (filename != null || filename.Contains(".zip"))
                {
                    devices.Sideload(@filename);
                }
            }
            else if (devices.State == DeviceState.ONLINE)
            {
                if (filename != null || filename.Contains(".zip"))
                {
                    devices.Commander("adb reboot sideload " + filename);
                }
            }
            else if (devices.State == DeviceState.RECOVERY)
            {
                MetroMessageBox.Show(this, "To Activate Sideload Mode Follow My Instruction \n chosse Sideload in recovery menu and try again", "Sir, Please Activate Sideload Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device on correct mode");
            }
        }
        //flash update zip
        private void FlashZipUp_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.SIDELOAD)
            {
                if (filename != null || filename.Contains(".zip"))
                {
                    devices.Commander("adb sideload_miui OTAPACKAGE " + @filename);
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device in Xioami Recovery mode");
            }
        }

        private void apkinsBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE)
            {
                if (filename.Contains(".apk"))
                {
                    devices.InstallApk(filename);
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device in Xioami Recovery mode");
            }
        }
        //open imei changer
        private void ImeicgrBtn_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again ");
            }
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
            {
                if (devices.IsDiag)
                {
                    ImeiChanger imei = new ImeiChanger();
                    imei.Show();
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device on online mode or recovery mode ");
            }
        }
        //open device manager
        private void DevmanBtn_Click(object sender, EventArgs e)
        {
            var proc = new Process
            {
                StartInfo = { FileName = Environment.SystemDirectory + "\\mmc.exe", Arguments = "devmgmt.msc" }
            };
            proc.Start();
        }
        //open task manager
        private void button1_Click(object sender, EventArgs e)
        {
            var proc = new Process
            {
                StartInfo = { FileName = Environment.SystemDirectory + "\\taskmgr.exe" }
            };
            proc.Start();
        }
        //install Driver
        private void InstalDriveBtn_Click(object sender, EventArgs e)
        {
            string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            MiDriver miDriver = new MiDriver();
            miDriver.CopyFiles(applicationBase);
            miDriver.InstallAllDriver(applicationBase, true);
        }
        //official unlock mmethod
        private void officialUnlockBtn_Click(object sender, EventArgs e)
        {
            Process.Start("http://en.miui.com/unlock/");
            kenzo.SpeakAsync("Sir, wait for a second, your will redirect to xiaomi website to unlock your device follow instruction on the site");
        }
        //button back
        private void tblBack_Click(object sender, EventArgs e)
        {
            devices.PressButton(KeyEventCode.BACK);
            if (!fast_motion) { update(); }
        }
        //button home
        private void tblMenu_Click(object sender, EventArgs e)
        {
            devices.PressButton(KeyEventCode.HOME);
            if (!fast_motion) { update(); }
        }
        //button menu
        private void tblRight_Click(object sender, EventArgs e)
        {
            devices.PressButton(KeyEventCode.MENU);
            if (!fast_motion) { update(); }
        }
        //button power
        private void PwrBtn_Click(object sender, EventArgs e)
        {
            devices.PressButton(KeyEventCode.POWER);
            if (!fast_motion) { update(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checked
            {
                int num = (int)Math.Round((Width - ClientSize.Width) / 2.0);
                pos.X = Cursor.Position.X - Location.X - 686 - num;
                pos.Y = Cursor.Position.Y - Location.Y - 59 - (Height - ClientSize.Height - (num - 56));

                if (pos.X > 0 & pos.X < DevicePanel.Width & pos.Y > 0 & pos.Y < DevicePanel.Height)
                {
                    dest.X = (int)Math.Round(unchecked(pos.X / (double)DevicePanel.Width * Convert.ToDouble(resX)));
                    dest.Y = (int)Math.Round(unchecked(pos.Y / (double)DevicePanel.Height * Convert.ToDouble(resY)));
                }
            }
        }

        private void DevicePanel_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (enable_touch)
            {
                complete_flag = false;
                coordinates_1.X = 0;
                coordinates_1.Y = 0;
                coordinates_2.X = 0;
                coordinates_2.Y = 0;
            }
        }

        private void DevicePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (enable_touch)
            {
                coordinates_1.X = dest.X;
                coordinates_1.Y = dest.Y;
                complete_flag = true;
            }
        }

        private void DevicePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (enable_touch)
            {
                if (complete_flag)
                {
                    coordinates_2.X = dest.X;
                    coordinates_2.Y = dest.Y;
                    if (coordinates_1.X == coordinates_2.X & coordinates_1.Y == coordinates_2.Y)
                    {
                        devices.Tap(coordinates_1.X.ToString(), coordinates_1.Y.ToString());
                        if (!fast_motion) { update(); }
                    }
                    else
                    {
                        devices.Swipe(coordinates_1.X.ToString(), coordinates_1.Y.ToString(), coordinates_2.X.ToString(), coordinates_2.Y.ToString());
                        if (!fast_motion) { update(); }
                    }
                }
            }
        }

        private void update()
        {
            if (fast_motion)
            {
                bool isstarted = devices.IsMiniCapStart();
                xiaomi.log("isstarted", isstarted.ToString(), null);
                if (!isstarted)
                {
                    Thread minicaptask = new Thread(devices.StartMinicapServer);
                    minicaptask.Start();
                    //devices.StartMinicapServer();
                    minicap = new MinicapStream();
                    DoubleBuffered = true;
                    minicap.Update += new MinicapEventHandler(UpdatePictureBox);
                    Thread thread = new Thread(minicap.ReadImageStream);
                    thread.Start();
                }
            }
            else // if (!fast_motion)
            {
                Disp();
            }
        }

        private void UpdatePictureBox()
        {
            DevicePanel.Invalidate();

            byte[] buff = new byte[0];
            minicap.ImageByteQueue.TryDequeue(out buff);
            MemoryStream stream = new MemoryStream(buff);
            DevicePanel.BackgroundImage = Image.FromStream(stream);
        }

        private async void Disp()
        {
            if (devices.State == DeviceState.ONLINE)
            {
                bool ss;
                await Task.Delay(500);
                ss = await Task.Run(() => devices.GetScreen()).ConfigureAwait(false);
                await Task.Delay(250);
                //ss = devices.GetScreen();
                if (ss)
                {
                    string file = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ss.raw";
                    BinaryReader binReader = new BinaryReader(File.Open(file, FileMode.Open));
                    FileInfo fileInfo = new FileInfo(file);
                    byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                    binReader.Close();

                    // Init bitmap
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(bytes);
                    bitmap.EndInit();

                    File.Delete(file);
                    DevicePanel.BackgroundImage = bitob(bitmap);
                }
            }
        }

        private Bitmap bitob(BitmapImage bi)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BmpBitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bi));
                enc.Save(ms);
                Bitmap b = new Bitmap(ms);
                return new Bitmap(b);
            }
        }

        private Image UpDisp(string p)
        {
            Bitmap bm;
            Image imgs;

            using (imgs = Image.FromFile(p))
            {
                bm = new Bitmap(imgs);
            }
            return bm;
        }

        private void Files_DragEnter(object sender, DragEventArgs e)
        {
            string namafile;
            _validasi = GetFilename(out namafile, e);
            if (_validasi)
            {
                filename = namafile;
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;

            filename = string.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = e.Data.GetData(DataFormats.FileDrop) as Array;

                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is string))
                    {
                        filename = ((string[])data)[0];
                        return true;
                    }
                }
            }
            return ret;
        }

        ///
        /// Navigation Menu 
        ///

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            minicap.StopMinicap();
            xiaomi.Dispose();
        }

        private void button_back(object sender, EventArgs e)
        {
            if (MenuUtama.Visible == false)
            {
                MenuUtama.Visible = true;
                MenuAbout.Visible = false;
                MenuInstal.Visible = false;
                MenuWipe.Visible = false;
                MenuBoot.Visible = false;
                MenuFlash.Visible = false;
                MenuUnlock.Visible = false;
                MenuBackup.Visible = false;
                MenuAdvanced.Visible = false;
            }
        }

        private void Minstl_Click(object sender, EventArgs e)
        {
            imgzipBtn.Text = "Flash IMG";
            pushpullBtn.Text = "Push";
            PushPnl.Visible = false;
            PullPnl.Visible = false;
            FlashImgPnl.Visible = false;
            FlashZipPnl.Visible = false;
            SideloadPnl.Visible = false;
            MenuInstal.Visible = true;
            MenuUtama.Visible = false;
        }

        private void MWipe_Click(object sender, EventArgs e)
        {
            MenuWipe.Visible = true;
            MenuUtama.Visible = false;
        }

        private void Mboot_Click(object sender, EventArgs e)
        {
            MenuBoot.Visible = true;
            recoveryPnl.Visible = false;
            MenuUtama.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MenuFlash.Visible = true;
            MenuUtama.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MenuUnlock.Visible = true;
            UnlockPnl.Visible = false;
            OptionPnl.Visible = false;
            relockPnl.Visible = false;
            screenlockPnl.Visible = false;
            MenuUtama.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button3.Text = "ADB Backup";
            AdbBckupPnl.Visible = false;
            AdbRestPnl.Visible = false;
            EdlBckupPnl.Visible = false;
            MenuBackup.Visible = true;
            MenuUtama.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MenuAbout.Visible = true;
            MenuUtama.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MenuAdvanced.Visible = true;
            MenuUtama.Visible = false;
        }

        private void recoverymenuBtn_Click(object sender, EventArgs e)
        {
            if (recoveryPnl.Visible == false)
                recoveryPnl.Visible = true;
            else
                recoveryPnl.Visible = false;
        }

        private void BLUnlock_Click(object sender, EventArgs e)
        {
            unlockPnlchanged(BLUnlock);
            if (OptionPnl.Visible == false)
            {
                OptionPnl.Visible = true;
            }
            else
            {
                OptionPnl.Visible = false;
            }
        }

        private void BLrelock_Click(object sender, EventArgs e)
        {
            unlockPnlchanged(BLrelock);
            if (relockPnl.Visible == true)
            {
                relockPnl.Visible = false;
                UnlockPnl.Visible = false;
            }
            else if (UnlockPnl.Visible == false)
            {
                UnlockPnl.Visible = true;
            }
            else if (UnlockPnl.Visible == true)
            {
                relockPnl.Visible = true;
                UnlockPnl.Visible = false;
            }
        }

        private void ScrUnlock_Click(object sender, EventArgs e)
        {
            unlockPnlchanged(ScrUnlock);
            if (screenlockPnl.Visible == false)
            {
                screenlockPnl.Visible = true;
            }
            else
            {
                screenlockPnl.Visible = false;
            }
        }

        private void unlockPnlchanged(Button btn)
        {
            if (btn == BLUnlock)
            {
                relockPnl.Visible = false;
                UnlockPnl.Visible = false;
                screenlockPnl.Visible = false;
            }
            else if (btn == BLrelock)
            {
                OptionPnl.Visible = false;
                screenlockPnl.Visible = false;
            }
            else if (btn == ScrUnlock)
            {
                OptionPnl.Visible = false;
                relockPnl.Visible = false;
                UnlockPnl.Visible = false;
            }
            else if (btn == BLInfo)
            {
                OptionPnl.Visible = false;
                relockPnl.Visible = false;
                UnlockPnl.Visible = false;
                screenlockPnl.Visible = false;
            }
        }

        private void installPnlchanged(Button btn)
        {
            if (btn == imgzipBtn)
            {
                PushPnl.Visible = false;
                PullPnl.Visible = false;
                SideloadPnl.Visible = false;
                ApkInsPnl.Visible = false;
                ApkUninPnl.Visible = false;
            }
            else if (btn == pushpullBtn)
            {
                FlashZipPnl.Visible = false;
                FlashImgPnl.Visible = false;
                SideloadPnl.Visible = false;
                ApkInsPnl.Visible = false;
                ApkUninPnl.Visible = false;
            }
            else if (btn == SideloadBtn)
            {
                FlashZipPnl.Visible = false;
                FlashImgPnl.Visible = false;
                PushPnl.Visible = false;
                PullPnl.Visible = false;
                ApkInsPnl.Visible = false;
                ApkUninPnl.Visible = false;
            }
            else if (btn == ApkBtnMenu)
            {
                PushPnl.Visible = false;
                PullPnl.Visible = false;
                FlashZipPnl.Visible = false;
                FlashImgPnl.Visible = false;
                SideloadPnl.Visible = false;
            }
        }

        private void imgzipBtn_Click(object sender, EventArgs e)
        {
            installPnlchanged(imgzipBtn);
            if (FlashZipPnl.Visible == true)
            {
                FlashZipPnl.Visible = false;
                FlashImgPnl.Visible = false;
            }
            else if (FlashImgPnl.Visible == false)
            {
                FlashImgPnl.Visible = true;
                imgzipBtn.Text = "Flash Zip";
            }
            else if (FlashZipPnl.Visible == false)
            {
                FlashImgPnl.Visible = false;
                FlashZipPnl.Visible = true;
                imgzipBtn.Text = "Flash IMG";
            }
        }

        private void pushpullBtn_Click(object sender, EventArgs e)
        {
            installPnlchanged(pushpullBtn);
            if (PullPnl.Visible == true)
            {
                PushPnl.Visible = false;
                PullPnl.Visible = false;
            }
            else if (PushPnl.Visible == false)
            {
                PushPnl.Visible = true;
                pushpullBtn.Text = "Pull";
            }
            else if (PullPnl.Visible == false)
            {
                PushPnl.Visible = false;
                PullPnl.Visible = true;
                pushpullBtn.Text = "Push";
            }
        }

        private void ApkBtnMenu_Click(object sender, EventArgs e)
        {
            installPnlchanged(ApkBtnMenu);
            if (ApkUninPnl.Visible == true)
            {
                ApkInsPnl.Visible = false;
                ApkUninPnl.Visible = false;
            }
            else if (ApkInsPnl.Visible == false)
            {
                ApkInsPnl.Visible = true;
            }
            else if (ApkUninPnl.Visible == false)
            {
                ApkInsPnl.Visible = false;
                ApkUninPnl.Visible = true;
            }
        }

        private void getApplist()
        {
            if (!xiaomi.HasConnectedDevices)
            {
                return;
            }
            else if (devices.State == DeviceState.ONLINE || devices.State == DeviceState.RECOVERY)
            {
                AppsList.Items.Clear();
                apklist = Apps.getlist();
                foreach (Apps item in apklist)
                {
                    AppsList.Items.Add(item.name);
                }
            }
            else
            {
                kenzo.SpeakAsync("Sir , Make sure device on online mode or recovery mode ");
            }
        }

        private void SideloadBtn_Click(object sender, EventArgs e)
        {
            installPnlchanged(SideloadBtn);
            if (SideloadPnl.Visible == false)
                SideloadPnl.Visible = true;
            else
                SideloadPnl.Visible = false;
        }

        private void BckupPnlchanged(Button btn)
        {
            if (btn == button6)
            {
                AdbBckupPnl.Visible = false;
                AdbRestPnl.Visible = false;
            }
            else if (btn == button3)
            {
                EdlBckupPnl.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!xiaomi.HasConnectedDevices)
            {
                kenzo.SpeakAsync("Sir , Make sure device connected and try again");
            }
            else if (devices.State == DeviceState.EDL)
            {
                List<Gpt> part = new List<Gpt>();
                part = Miuidl.partition;
                foreach (Gpt item in part)
                {
                    PartListBox.Items.Add(item.name);
                }
                PartListBox.Visible = true;
                BckupPnlchanged(button6);
                if (EdlBckupPnl.Visible == true)
                {
                    EdlBckupPnl.Visible = false;
                }
                else if (EdlBckupPnl.Visible == false)
                {
                    EdlBckupPnl.Visible = true;
                }
            }
            else if (devices.State != DeviceState.EDL)
            {
                kenzo.SpeakAsync("Sir , This Feature Only available in EDL Mode, Make Sure Device in Edl mode and try again");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BckupPnlchanged(button3);
            if (AdbRestPnl.Visible == true)
            {
                AdbBckupPnl.Visible = false;
                AdbRestPnl.Visible = false;
            }
            else if (AdbBckupPnl.Visible == false)
            {
                AdbBckupPnl.Visible = true;
                button3.Text = "ADB Restore";
            }
            else if (AdbRestPnl.Visible == false)
            {
                AdbBckupPnl.Visible = false;
                AdbRestPnl.Visible = true;
                button3.Text = "ADB Backup";
            }
        }

        private void FlashZipPnl_DragDrop(object sender, DragEventArgs e)
        {
            status(filename);
        }

        private void SideloadPnl_DragDrop(object sender, DragEventArgs e)
        {
            status(filename);
        }

        private void PushPnl_DragDrop(object sender, DragEventArgs e)
        {
            status(filename);
        }

        private void FlashImgPnl_DragDrop(object sender, DragEventArgs e)
        {
            status(filename);
        }

        private void ApkInsPnl_DragDrop(object sender, DragEventArgs e)
        {
            status(filename);
        }

        private void status(string stt)
        {
            Stt.Text = stt;
            //Stt.Invalidate();
        }

        private void notif(string title, string message)
        {
            MetroMessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //var folder = "C:\\Users\\mrivai89\\Documents\\github\\";
            //devices.MakeRaw(folder);
        }
        
    }
}