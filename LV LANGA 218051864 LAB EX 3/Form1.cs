using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Business_Layer;
// LV LANGA 218051864
namespace LV_LANGA_218051864_LAB_EX_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         


       
        private void Form1_Load(object sender, EventArgs e)
        {
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsMySQLOperations objReadOps = new clsMySQLOperations();  // CREATING A LOCAL INSTANCE OF THE BUSINESS LAYER CLASS AND ITS MYSQL METHODS 

            var dt = objReadOps.testQry(); // CREATING A DATASOURCE FOR THE GRIDVIEW 

            dataGridView1.DataSource = dt; // ASSIGNING THE GRIDVIEW

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); 
            clsMyLINQOperations objReadLinqOps = new clsMyLINQOperations(); // CREATING A LOCAL INSTANCE OF THE BUSINESS LAYER CLASS AND ITS MYSQL METHODS 

            var tocycle = objReadLinqOps.testQry(); // CREATING AN OBJECT LIST FROM THE BUSINESS LAYER

            foreach (var item in tocycle)
            {
                listBox1.Items.Add(item.details); // CYCLING THROUGH THE DATA FROM THE BUSINESS LAYER
            }
        }
    }
}
