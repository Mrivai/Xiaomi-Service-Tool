# Unofficial Xiaomi Service Tool
### xiaomi redmi note 3 pro toolkit

Xiaomi Service Tool (xiaomi redmi note 3 pro toolkit) is an application created to solve existing software problems on a Qualcomm chipset based xiaomi smartphone (case study smartphone xiaomi redmi note 3 pro). So far to overcome the problem of software need a tool Box Flasher which is usually used by technicians service mobile phone, then how about end users who want to repair their own mobile phone without having to buy the tool that the price is not cheap. Therefore needs to be made an application that has several functions similar to the tool to facilitate users in improving the existing software on the user's smartphone.


## Features
- Auto detect connected device.
- Screencast.
- Gesture Control.
- Flash Menu used for flashing user device.
- Unlock
	- Unlock Bootloader
		- Official, Unlock Bootloader based Xiaomi Official method.
		- Un-Official, Instal Unlock Bootloader (emmc_appsboot.mbn) over EDL mode.
	- Unlock Relock
		- Fasboot Oem unlock, execute Fasboot Oem unlock command
		- Fasboot Oem Lock, execute Fasboot Oem Lock command
	- Unlock Screenlock, remove device screenlock pin, pattern, password over recovery mode (twrp).
	- Check Bootloader Status, summary for check device bootloader status.
- Instal
	- Flash Img, install device .img file over fastboot or EDL mode.
	- Flash Zip, install device update file or patch file (.zip) over recovery mode.
	- Sideload, used for sideload device update file or patch file (.zip) over recovery mode.
	- Push, push file from pc into device over adb mode.
	- Pull, pull file from device into pc over adb mode.
	- Instal Apk over adb mode.
	- Remove Apps over adb mode.
- Wipe
	- Advand wipe.
	- format device.
- Backup
	- Adb Backup.
	- EDL Backup, used for backup device system over EDL m0
- Advanced
	- Diag Mode. used for enable diag mode.
	- Check Diag Mode.
	- Imei Changer, used for changing device imei.
- Reboot
	- Device.
	- Bootloader.
	- recovery.
	- EDL.

## Build

Clone repo :point_right: open solution in Visual Studio :point_right: add reference :point_right: build :facepunch:

## Usage

Please install device driver and enable device usb debug before using this app.

Note: This application is under development stage, Im not resposible for any problem comes by using this app

## Author 

- [Mrivai](https://github.com/Mrivai) 

## Screenshots
<p align="center">
<img src="./screenshots/splash_screen.png">
<img src="./screenshots/main_menu.png">
<img src="./screenshots/flash.png">
<img src="./screenshots/unlock.png">
<img src="./screenshots/unlock1.png">
<img src="./screenshots/unlock2.png">
<img src="./screenshots/unlock3.png">
<img src="./screenshots/unlock4.png">
<img src="./screenshots/install.png">
<img src="./screenshots/install1.png">
<img src="./screenshots/install2.png">
<img src="./screenshots/install3.png">
<img src="./screenshots/install4.png">
<img src="./screenshots/install5.png">
<img src="./screenshots/install6.png">
<img src="./screenshots/wipe.png">
<img src="./screenshots/backup.png">
<img src="./screenshots/adv.png">
<img src="./screenshots/reboot.png">
<img src="./screenshots/imei.png">
<img src="./screenshots/about.png">
</p>