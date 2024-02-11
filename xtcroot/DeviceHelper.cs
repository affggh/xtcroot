using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xtcroot
{
    class DeviceHelper
    {
        private string AdbExecutable;
        private string FastbootExecutable;
        private string LsusbExecutable;
        private string QSaharaServerExecutable;
        private string Fh_LoaderExecutable;
        
        private DeviceHelper() {
            AdbExecutable = "./adb.exe";
            FastbootExecutable = "./fastboot.exe";
            LsusbExecutable = "./lsusb.exe";
        }


    }
}
