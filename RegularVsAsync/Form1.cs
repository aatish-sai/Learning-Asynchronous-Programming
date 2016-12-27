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

namespace RegularVsAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class NormalTask
        {
            public int Method()
            {
                Thread.Sleep(4000);
                return new Random().Next(1, 5000);
            }
        }

        class AsyncTask
        {
            public Task<int> MethodAsync()
            {
                Task<int> task = new Task<int>(Method);
                task.Start();

                return task;
            }

            public int Method()
            {
                Thread.Sleep(4000);
                return new Random().Next(1, 5000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Please wait";
            label1.Text = new NormalTask().Method().ToString();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Please wait";

            try
            {
                int result = await new AsyncTask().MethodAsync();
                label1.Text = result.ToString();
            }
            catch(System.Exception ex)
            {
                label1.Text = ex.Message + Thread.CurrentThread.Name;
            }
        }
    }
}
