using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;
using System.Windows.Forms;
using static Sharp7.S7Client;
namespace PLC
{
    public static class BasicData
    {
        public static void getData(S7Client plc,TextBox totalvolume,TextBox currentlevel,TextBox currentvolume,
            ProgressBar progressBar1, Label labelProportion,Label localLabel, int result,int secondDb,int totalVolumeOff,
            int currentVolumeOff,int currentLevelOff,int treatment,TextBox treatmentTextbox)
        {
            if (result == 0)
            {

                byte[] db1Buffer = new byte[18];
                result = plc.DBRead(1, 0, 18, db1Buffer);
                if (result != 0)
                {
                    MessageBox.Show(plc.ErrorText(result), "Error", MessageBoxButtons.OK);
                }

                float totalVolume = S7.GetRealAt(db1Buffer, totalVolumeOff);
                totalvolume.Text = totalVolume.ToString();

                float currentLevel = S7.GetRealAt(db1Buffer, currentLevelOff);
                currentlevel.Text = currentLevel.ToString();
                progressBar1.Value = Convert.ToInt32(currentLevel);
                labelProportion.Text = "%" + progressBar1.Value.ToString();

                float currentVolume = S7.GetRealAt(db1Buffer, currentVolumeOff);
                currentvolume.Text = currentVolume.ToString();
               
                //for take data other table
                result = plc.DBRead(secondDb, 0, 18, db1Buffer);
                if (result != 0)
                {
                    MessageBox.Show(plc.ErrorText(result), "Error", MessageBoxButtons.OK);
                }
                bool local = S7.GetBitAt(db1Buffer, 0, 0);
                localLabel.Text = local.ToString();

                if(treatment != 0)
                {
                    int currentLvlToSend = S7.GetIntAt(db1Buffer, treatment);
                    treatmentTextbox.Text = currentLvlToSend.ToString();
                }
            }
            else
            {
                MessageBox.Show(plc.ErrorText(result), "Error", MessageBoxButtons.OK);
            }
        }


        public static S7CpuInfo getPlcCpuInfo(S7Client plc)
        {
            S7CpuInfo cpuInfo = new S7CpuInfo();
            plc.GetCpuInfo(ref cpuInfo);
            return cpuInfo;
        }
        public static S7CpInfo getPlcCpiInfo(S7Client plc)
        {
            S7CpInfo cpiInfo = new S7CpInfo();
            plc.GetCpInfo(ref cpiInfo);
            return cpiInfo;
        }
        public static void fillTextbox(S7Client plc,TextBox plcport, TextBox connectedtimeout, TextBox pduSizeReuested,
            TextBox exectiotime, TextBox pdusizenegotiated,
            TextBox hashcode, TextBox recvtimeout,
            TextBox sendtimeout, TextBox lasterror, TextBox modulename,
            TextBox moduletypename, TextBox serialnumber, TextBox copyright,
            TextBox asname, TextBox maxpdulenght, TextBox maxconnections,
            TextBox maxmpirate, TextBox maxbusrate,  S7CpuInfo cpuInfo,  S7CpInfo cpiInfo)
        {
            //--PLC DATA
            plcport.Text = plc.PLCPort.ToString();
            connectedtimeout.Text = plc.ConnTimeout.ToString();
            pduSizeReuested.Text = plc.PduSizeRequested.ToString();
            exectiotime.Text = plc.ExecutionTime.ToString();
            pdusizenegotiated.Text = plc.PduSizeNegotiated.ToString();
            hashcode.Text = plc.GetHashCode().ToString();
            recvtimeout.Text = plc.RecvTimeout.ToString();
            sendtimeout.Text = plc.SendTimeout.ToString();
            lasterror.Text = plc.LastError().ToString();
            //--CPU DATA
            if (cpuInfo.ASName != null)
            {
                modulename.Text = cpuInfo.ModuleName.ToString();
                moduletypename.Text = cpuInfo.ModuleTypeName.ToString();
                serialnumber.Text = cpuInfo.SerialNumber.ToString();
                copyright.Text = cpuInfo.Copyright.ToString();
                asname.Text = cpuInfo.ASName.ToString();
            }
            
            //--CPI DATA
            if(cpiInfo.MaxBusRate != null)
            {
                maxpdulenght.Text = cpiInfo.MaxPduLength.ToString();
                maxconnections.Text = cpiInfo.MaxConnections.ToString();
                maxmpirate.Text = cpiInfo.MaxMpiRate.ToString();
                maxbusrate.Text = cpiInfo.MaxBusRate.ToString();
            }
            
            

        }
    }
}
