using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp37
{
    public partial class Form1 : Form
    {
        Mutex mutex = new Mutex();
        int count = 20;
        int count1 = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void CountUp()
        {
            mutex.WaitOne();

            for (int i = 0; i <= count; i++)
            {
                AppendText($"Thread 1: {i}");
                Thread.Sleep(100);
            }

            mutex.ReleaseMutex();
        }

        private void CountDown()
        {
            mutex.WaitOne();

            for (int i = count1; i >= 0; i--)
            {
                AppendText($"Thread 2: {i}");
                Thread.Sleep(100);
            }

            mutex.ReleaseMutex();
        }

        private void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendText), text);
                return;
            }

           textBox1.AppendText(text + Environment.NewLine);
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            Thread firstThread = new Thread(CountUp);
            Thread secondThread = new Thread(CountDown);

            firstThread.Start();
            secondThread.Start();
        }
    }
}
