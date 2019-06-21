using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 餐廳管理系統
{
    public partial class Accounting : Form
    {
        public Accounting()
        {
            InitializeComponent();
        }

        bool IsToHome = false; //紀錄是否要回到Form1
        private void Return_Click(object sender, EventArgs e)
        {
            IsToHome = true;
            this.Close(); //強制關閉Form2
        }

        protected override void OnClosing(CancelEventArgs e) //在視窗關閉時觸發
        {
            base.OnClosing(e);
            if (IsToHome) //判斷是否要回到Form1
            {
                this.DialogResult = DialogResult.Yes; //利用DialogResult傳遞訊息
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AccountingAdd Form2 = new AccountingAdd();
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
            AccountingUpdate Form2 = new AccountingUpdate();
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

        private SqlConnection connection()
        {
            string strconn = @"Data Source=4aee\sqlexpress;Initial Catalog=餐廳;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            return conn;
        }

        private void Accounting_Load(object sender, EventArgs e)
        {
            SqlConnection conn = connection();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM 銷售財務表 WHERE CAST(銷售財務表日期 AS DATE) = CAST(GETDATE() AS DATE)", conn);
                conn.Open();
                SqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                cmd.Dispose();
                if (sqlDataReader1.HasRows)
                {
                    int Total = 0;
                    while (sqlDataReader1.Read())
                    {
                        if(sqlDataReader1.GetString(2) == "收入")
                        {
                            Total += sqlDataReader1.GetInt32(3);
                        }
                        else
                        {
                            Total -= sqlDataReader1.GetInt32(3);
                        }
                    }
                    //comboBox1.SelectedIndex = 0;
                    label1.Text = "今日總營收：\n" + Total.ToString() + "元";
                    if(Total > 0)
                    {
                        pictureBox2.Visible = true;
                        pictureBox3.Visible = false;
                    }
                    else
                    {
                        pictureBox2.Visible = false;
                        pictureBox3.Visible = true;
                    }
                }
                else
                {
                    label1.Text = "今日總營收：\n0元";
                }
                sqlDataReader1.Close();
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM 銷售財務表 WHERE MONTH(CAST(銷售財務表日期 AS DATE)) = MONTH(CAST(GETDATE() AS DATE)) AND YEAR(CAST(銷售財務表日期 AS DATE)) = YEAR(CAST(GETDATE() AS DATE))", conn);
                SqlDataReader sqlDataReader2 = cmd2.ExecuteReader();
                cmd2.Dispose();
                if (sqlDataReader2.HasRows)
                {
                    int Total = 0;
                    while (sqlDataReader2.Read())
                    {
                        if (sqlDataReader2.GetString(2) == "收入")
                        {
                            Total += sqlDataReader2.GetInt32(3);
                        }
                        else
                        {
                            Total -= sqlDataReader2.GetInt32(3);
                        }
                    }
                    //comboBox1.SelectedIndex = 0;
                    label2.Text = "本月總營收：\n" + Total.ToString() + "元";
                }
                else
                {
                    label2.Text = "本月總營收：\n0元";
                }
                sqlDataReader2.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
