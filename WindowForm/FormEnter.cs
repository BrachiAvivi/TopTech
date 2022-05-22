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
    public partial class FormEnter : Form
    {
        
        public FormEnter()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox1.Text == "60306")
                {
                    Manager m = new Manager();
                    m.Show();
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ClsDB db = ClsDB.Instance;
                dataGridView1.DataSource = db.ShowDestinations(textBox2.Text,"60306").ToList();
            }
        }
    }
}
