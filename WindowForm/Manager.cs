using System;
using Bll;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowForm
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan openTime = new TimeSpan(open.DecimalPlaces, 0, 0);
            TimeSpan closeTime = new TimeSpan(close.DecimalPlaces, 0, 0);
            OpenBusinessDay algoritem = new OpenBusinessDay(openTime, closeTime);
            algoritem.OpenDay();
            
        }
    }
}
