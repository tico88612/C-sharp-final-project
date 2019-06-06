using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 餐廳管理系統
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order Form2 = new Order();
            switch (Form2.ShowDialog(this))
            {
                case DialogResult.Yes: //Form2中按下ToForm1按鈕
                    this.Show(); //顯示父視窗
                    break;
                case DialogResult.No: //Form2中按下關閉鈕
                    this.Close();  //關閉父視窗 (同時結束應用程式)
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Warehousing Form2 = new Warehousing();
            switch (Form2.ShowDialog(this))
            {
                case DialogResult.Yes: //Form2中按下ToForm1按鈕
                    this.Show(); //顯示父視窗
                    break;
                case DialogResult.No: //Form2中按下關閉鈕
                    this.Close();  //關閉父視窗 (同時結束應用程式)
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Accounting Form2 = new Accounting();
            switch (Form2.ShowDialog(this))
            {
                case DialogResult.Yes: //Form2中按下ToForm1按鈕
                    this.Show(); //顯示父視窗
                    break;
                case DialogResult.No: //Form2中按下關閉鈕
                    this.Close();  //關閉父視窗 (同時結束應用程式)
                    break;
                default:
                    break;
            }
        }
    }
}
