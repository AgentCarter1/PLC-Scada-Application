using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7;
using static Sharp7.S7Client;
namespace PLC
{
    public partial class Form1 : Form
    {
        static S7Client plc1 = new S7Client();
        static S7Client plc2 = new S7Client();
        static S7Client plc3 = new S7Client();
        static S7CpuInfo plc1CpuInfo = new S7CpuInfo();
        static S7CpuInfo plc2CpuInfo = new S7CpuInfo();
        static S7CpuInfo plc3CpuInfo = new S7CpuInfo();
        static S7CpInfo plc1CpiInfo = new S7CpInfo();
        static S7CpInfo plc2CpiInfo = new S7CpInfo();
        static S7CpInfo plc3CpiInfo = new S7CpInfo();
        public int result1 = plc1.ConnectTo("192.168.41.10", 0, 1);
        public int result = plc2.ConnectTo("192.168.42.10", 0, 1);
        public int result3 = plc3.ConnectTo("192.168.43.10", 0, 1);
        
        int time = 3;
        public Form1()
        {
            InitializeComponent();
        }
        private void getPlc1Data(TextBox txt1,TextBox txt2,TextBox txt3,ProgressBar pgbar,Label lbl1,Label lbl2)
        {
            BasicData.getData(plc1, txt1, txt2, txt3, pgbar,
                       lbl1, lbl2, result1, 8, 6, 10, 2, 0, treatmentwaterevac);
            
        }
        private void getPlc2Data(TextBox txt1, TextBox txt2, TextBox txt3, ProgressBar pgbar, Label lbl1, Label lbl2)
        {
            BasicData.getData(plc2, txt1, txt2, txt3,
                        pgbar, lbl1, lbl2, result, 16, 8, 12, 4, 0, treatmentwaterevac);
        }
        private void getPlc3Data(TextBox txt1, TextBox txt2, TextBox txt3, ProgressBar pgbar, Label lbl1, Label lbl2,TextBox txt4)
        {
            BasicData.getData(plc3, txt1, txt2, txt3,
                    pgbar, lbl1, lbl2, result3,
                    8, 8, 12, 4, 2, txt4);
        }
        private void setProgressBar(ProgressBar pgbar, int min, int max)
        {
            pgbar.Maximum = max;
            pgbar.Minimum = min;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
           
            
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                getPlc2Data(totalvolume, currentlevel, currentvolume,
                        progressBar1, label14, label13);
                time = 3;
            }
        }

        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == promotion1)
            {
                if (result1 == 0)
                {
                    
                    promotion1timer.Start();
                    getPlc1Data(totalvolume1, currentlevel1, currentvolum1, progresspromotion2,
                       label15, label16);
                    setProgressBar(progresspromotion2, 0, 100);
                    pumpflowmeter1.Text = "0";
                }
                else
                {
                    MessageBox.Show(plc1.ErrorText(result1), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == promotion2)
            {
                if (result == 0)
                {
                    
                    promotion2timer.Start();
                    getPlc2Data(totalvolume, currentlevel, currentvolume,
                        progressBar1, label14, label13);
                    
                    setProgressBar(progressBar1, 0, 100);
                    pumpvolume.Text = "0";
                }
                else
                {
                    MessageBox.Show(plc2.ErrorText(result), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == treatment)
            {

                if (result3 == 0)
                {
                    treatmenttimer.Start();
                    getPlc3Data(treatmentTotalText, treatmentCurrentlvl, treatmentCurrentText,
                    progressBar3, label26, label29, treatmentwaterevac);
                    setProgressBar(progressBar3, 0, 100);
                    treatmentFlowmeterText.Text = "0";
                }
                else
                {
                    MessageBox.Show(plc3.ErrorText(result3), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == alldata)
            {
                if (result1 == 0)
                {
                    
                    allPro1.Start();
                    getPlc1Data(textBox13, textBox10, textBox12, progressBar2,
                       label43, label71);
                    setProgressBar(progresspromotion2, 0, 100);
                    pumpflowmeter1.Text = "0";
                }
                else
                {
                    MessageBox.Show(plc1.ErrorText(result1), "Error", MessageBoxButtons.OK);
                }
                if (result == 0)
                {
                    
                    allPro2.Start();
                    getPlc2Data(textBox4, textBox1, textBox3,
                        progressBar5, label81, label44);

                    setProgressBar(progressBar1, 0, 100);
                    pumpvolume.Text = "0";
                }
                else
                {
                    MessageBox.Show(plc2.ErrorText(result), "Error", MessageBoxButtons.OK);
                }
                if (result3 == 0)
                {
                    alltreatment.Start();
                    getPlc3Data(textBox9, textBox6, textBox8,
                    progressBar4, label70, label60, textBox5);
                    setProgressBar(progressBar3, 0, 100);
                    treatmentFlowmeterText.Text = "0";
                }
                else
                {
                    MessageBox.Show(plc3.ErrorText(result3), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == promotion1info)
            {
                if(result1 == 0)
                {
                    plc1CpuInfo = BasicData.getPlcCpuInfo(plc1);
                    plc1CpiInfo = BasicData.getPlcCpiInfo(plc1);
                    BasicData.fillTextbox(plc1, textBox49, textBox48, textBox43, textBox47, textBox46, textBox42, textBox45, textBox44, textBox41,
                        textBox40, textBox39, textBox38, textBox37, textBox36, textBox35,
                        textBox34, textBox33, textBox32, plc1CpuInfo, plc1CpiInfo);
                }
                else
                {
                    MessageBox.Show(plc1.ErrorText(result1), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == promotion2info)  
            {
                if (result == 0)
                {
                    plc2CpuInfo = BasicData.getPlcCpuInfo(plc2);
                    plc2CpiInfo = BasicData.getPlcCpiInfo(plc2);
                    BasicData.fillTextbox(plc2, textBox67, textBox66, textBox61, textBox65, textBox64, textBox60,
                        textBox63, textBox62, textBox59, textBox58, textBox57, textBox56, textBox55, textBox54, textBox53, textBox52,
                        textBox51, textBox50, plc2CpuInfo, plc2CpiInfo);
                }
                else
                {
                    MessageBox.Show(plc2.ErrorText(result), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == treatmentinfo)
            {
                if(result3 == 0)
                {
                    plc3CpuInfo = BasicData.getPlcCpuInfo(plc3);
                    plc3CpiInfo = BasicData.getPlcCpiInfo(plc3);
                    BasicData.fillTextbox(plc3, textBox14, textBox15, textBox20, textBox16, textBox17, textBox21, textBox18, textBox19, textBox22,
                        textBox23, textBox24, textBox25, textBox26, textBox27, textBox28,
                        textBox29, textBox30, textBox31, plc3CpuInfo, plc3CpiInfo);
                }
                else
                {
                    MessageBox.Show(plc3.ErrorText(result3), "Error", MessageBoxButtons.OK);
                }
            }
            else if (tabControl1.SelectedTab == allinfo)
            {
                MessageBox.Show("allinfo");
            }
            
        }
        private void promotion1timer_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                time = 3;
                getPlc1Data(totalvolume1, currentlevel1, currentvolum1, progresspromotion2,
                       label15, label16);
            }
        }

        private void treatmenttimer_Tick(object sender, EventArgs e)
        {
            time--;
            if(time == 0)
            {
                time = 3;
                getPlc3Data(treatmentTotalText, treatmentCurrentlvl, treatmentCurrentText,
                    progressBar3, label26, label29, treatmentwaterevac);
            }
        }

        private void allPro1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                time = 3;
                getPlc1Data(textBox13, textBox10, textBox12, progressBar2,
                       label43, label71);
            }
        }

        private void allPro2_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                time = 3;

                getPlc2Data(textBox4, textBox1, textBox3,
                    progressBar5, label81, label44);
            }
        }

        private void alltreatment_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                time = 3;
                getPlc3Data(textBox9, textBox6, textBox8,
                    progressBar4, label70, label60, textBox5);
            }
            
        }

        
    }
}
