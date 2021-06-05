using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace Мониторинг_портов
{
    public partial class Form1 : Form
    {
        bool tcpFlag = false;
        bool udpFlag = false;
        String resTcp = " ";
        String resUdp = " ";
        DateTime Start; // Время запуска
        DateTime Stoped; //Время окончания
        TimeSpan Elapsed = new TimeSpan(); // Разница
        DateTime now = DateTime.Now; //Дата

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)  //TCP
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

            tcpFlag = false;
            resTcp = null;

            int count = 0;
            Start = DateTime.Now; // Старт (Записываем время)

            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

            PortInfo portInfo = new PortInfo();
            int m = tcpEndPoints.Length;
            int k = tcpConnections.Length;
            List<PortInfo> p = new List<PortInfo>();

            if (comboBox1.SelectedIndex == 0)  //Все
            {
                for (int i = 0; i < m; i++)  //Слушаются на локальном компьютере
                {
                    portInfo.PortNumber = tcpEndPoints[i].Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpEndPoints[i].Address, tcpEndPoints[i].Port);
                    portInfo.Remote = String.Format("{0}:{1}", properties.HostName, "0");  //Environment.MachineName
                    portInfo.State = String.Format("{0}", "Listening");

                    p.Add(portInfo);

                    textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                    textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                    textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                    textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                    resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                }

                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                    textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                    textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                    textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                    resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                }

                textBox6.Text = Convert.ToString(m + k);
            }
            if (comboBox1.SelectedIndex == 1)  //CloseWait
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if(portInfo.State == "CloseWait")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }
            if (comboBox1.SelectedIndex == 2)  //Established
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if (portInfo.State == "Established")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }
            if (comboBox1.SelectedIndex == 3)  //FinWait1
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if (portInfo.State == "FinWait1")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }
            if (comboBox1.SelectedIndex == 4)  //FinWait2
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if (portInfo.State == "FinWait2")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }
            if (comboBox1.SelectedIndex == 5)  //LastAck
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if (portInfo.State == "LastAck")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }
            if (comboBox1.SelectedIndex == 6)  //Listening
            {
                for (int i = 0; i < m; i++)  //Слушаются на локальном компьютере
                {
                    portInfo.PortNumber = tcpEndPoints[i].Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpEndPoints[i].Address, tcpEndPoints[i].Port);
                    portInfo.Remote = String.Format("{0}:{1}", properties.HostName, "0");  //Environment.MachineName
                    portInfo.State = String.Format("{0}", "Listening");

                    p.Add(portInfo);

                    textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                    textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                    textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                    textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                    resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                }

                if (m == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(m);
            }
            if (comboBox1.SelectedIndex == 7)  //SynSent
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if (portInfo.State == "SynSent")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }
            if (comboBox1.SelectedIndex == 8)  //TimeWait
            {
                for (int i = 0; i < k; i++)  //Активные подключения на локальном компьютере
                {
                    portInfo.PortNumber = tcpConnections[i].LocalEndPoint.Port;
                    portInfo.Local = String.Format("{0}:{1}", tcpConnections[i].LocalEndPoint.Address, tcpConnections[i].LocalEndPoint.Port);
                    portInfo.Remote = String.Format("{0}:{1}", tcpConnections[i].RemoteEndPoint.Address, tcpConnections[i].RemoteEndPoint.Port);
                    portInfo.State = String.Format("{0}", tcpConnections[i].State);

                    p.Add(portInfo);

                    if (portInfo.State == "TimeWait")
                    {
                        count++;

                        textBox1.Text += Convert.ToString(p[i].PortNumber) + Environment.NewLine;
                        textBox2.Text += Convert.ToString(p[i].Local) + Environment.NewLine;
                        textBox3.Text += Convert.ToString(p[i].Remote) + Environment.NewLine;
                        textBox4.Text += Convert.ToString(p[i].State) + Environment.NewLine;

                        resTcp += Convert.ToString(p[i].PortNumber).PadRight(10) + Convert.ToString(p[i].Local).PadRight(30) + Convert.ToString(p[i].Remote).PadRight(30) + Convert.ToString(p[i].State).PadRight(20) + Environment.NewLine;
                    }
                }

                if(count == 0)
                {
                    MessageBox.Show("Порты с данным статусом не найдены!");
                }

                textBox6.Text = Convert.ToString(count);
            }


            tcpFlag = true;

            textBox7.Text = Convert.ToString(now.ToString("d"));

            Stoped = DateTime.Now; // Стоп (Записываем время)
            Elapsed = Stoped.Subtract(Start); // Вычитаем из Stoped (когда код выполнился) время Start (когда код запустили на выполнение)
            textBox5.Text = String.Format("{0}, {1}", Elapsed.Milliseconds, "мс"); // Время в миллисекундах

            resTcp += Environment.NewLine + String.Format("{0}: {1}", "Время работы", textBox5.Text);
            resTcp += Environment.NewLine + String.Format("{0}: {1}", "Количество просканированных портов", textBox6.Text);
            resTcp += Environment.NewLine + String.Format("{0}: {1}", "Дата", textBox7.Text);
        }

        private void button3_Click(object sender, EventArgs e)  //UDP
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

            udpFlag = false;
            resUdp = null;

            Start = DateTime.Now; // Старт (Записываем время)

            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] udpEndPoints = properties.GetActiveUdpListeners();
            
            PortInfo portInf = new PortInfo();
            int k = udpEndPoints.Length;
            List<PortInfo> pr = new List<PortInfo>();

            for (int i = 0; i < k; i++)  //Слушаются на локальном компьютере
            {
                portInf.PortNumber = udpEndPoints[i].Port;
                portInf.Local = String.Format("{0}:{1}", udpEndPoints[i].Address, udpEndPoints[i].Port);
                portInf.Remote = String.Format("{0}", "*:*"); 

                portInf.State = String.Format("{0}", " ");

                pr.Add(portInf);

                textBox1.Text += Convert.ToString(pr[i].PortNumber) + Environment.NewLine;
                textBox2.Text += Convert.ToString(pr[i].Local) + Environment.NewLine;
                textBox3.Text += Convert.ToString(pr[i].Remote) + Environment.NewLine;
                textBox4.Text += Convert.ToString(pr[i].State) + Environment.NewLine;

                resUdp += Convert.ToString(pr[i].PortNumber).PadRight(10) + Convert.ToString(pr[i].Local).PadRight(40) + Convert.ToString(pr[i].Remote).PadRight(20) + Convert.ToString(pr[i].State).PadRight(20) + Environment.NewLine;
            }

            udpFlag = true;

            textBox6.Text = Convert.ToString(k);
            textBox7.Text = Convert.ToString(now.ToString("d"));

            Stoped = DateTime.Now; // Стоп (Записываем время)
            Elapsed = Stoped.Subtract(Start); // Вычитаем из Stoped (когда код выполнился) время Start (когда код запустили на выполнение)
            textBox5.Text = String.Format("{0}, {1}", Elapsed.Milliseconds, "мс"); // Время в миллисекундах

            resUdp += Environment.NewLine + String.Format("{0}: {1}", "Время работы", textBox5.Text);
            resUdp += Environment.NewLine + String.Format("{0}: {1}", "Количество просканированных портов", textBox6.Text);
            resUdp += Environment.NewLine + String.Format("{0}: {1}", "Дата", textBox7.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  //Фильтр
        {
           
        }

        private void button1_Click(object sender, EventArgs e)  //Выгрузка результата
        {
            SaveFileDialog sf = new SaveFileDialog();

           if (sf.ShowDialog() == DialogResult.Cancel) { return; }
                string filename = sf.FileName;
                sf.DefaultExt = ".txt";
                sf.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if(tcpFlag == true)
            {
                System.IO.File.WriteAllText(filename, resTcp);
            }
            else
            {
                if (udpFlag == true)
                {
                    System.IO.File.WriteAllText(filename, resUdp);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }


    public class PortInfo  //Класс Порт (информация)
    {
        public int PortNumber { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public string State { get; set; }

        public PortInfo()
        {
            PortNumber = 0;
            Local = "";
            Remote = "";
            State = "";
        }

        public PortInfo(int i, string local, string remote, string state)
        {
            PortNumber = i;
            Local = local;
            Remote = remote;
            State = state;
        }
    }
}
