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
    public partial class AccountingAdd : Form
    {
        public AccountingAdd()
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

        private SqlConnection connection()
        {
            string strconn = @"Data Source=4aee\sqlexpress;Initial Catalog=餐廳;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            return conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = connection();
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO 銷售財務表 (銷售財務表memo, 銷售財務表類別, 銷售財務表營業額, 銷售財務表日期) VALUES(@memo, @type, @money, GETDATE())", conn);
                conn.Open();
                cmd.Parameters.Add("@memo", SqlDbType.NVarChar, 255).Value = textBox2.Text;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 50).Value = comboBox1.SelectedItem.ToString();
                cmd.Parameters.Add("@money", SqlDbType.NVarChar, 50).Value = textBox1.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                comboBox1.SelectedIndex = 0;
                textBox1.Text = "";
                textBox2.Text = "";
                MessageBox.Show("新增成功", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void AccountingAdd_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
