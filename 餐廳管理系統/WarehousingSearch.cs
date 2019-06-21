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
    public partial class WarehousingSearch : Form
    {
        public WarehousingSearch()
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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = connection();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM 食材 WHERE 食材名稱 LIKE @foodName", conn);
                conn.Open();
                cmd.Parameters.Add("@foodName", SqlDbType.NVarChar, 50).Value = "%" + textBox1.Text + "%";
                SqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                cmd.Dispose();
                if (sqlDataReader1.HasRows)
                {
                    comboBox1.Items.Clear();
                    while (sqlDataReader1.Read())
                    {
                        comboBox1.Items.Add(new ComboboxItem(sqlDataReader1.GetInt32(0).ToString(), sqlDataReader1.GetString(1)));
                    }
                    //comboBox1.SelectedIndex = 0;
                    MessageBox.Show("搜尋完畢", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("查無食材", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                }
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

        private class ComboboxItem
        {
            public ComboboxItem(string value, string text)
            {
                Value = value;
                Text = text;
            }
            public string Value
            {
                get;
                set;
            }
            public string Text
            {
                get;
                set;
            }
            public override string ToString()
            {
                return Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int query = Convert.ToInt32((comboBox1.SelectedItem as ComboboxItem).Value.ToString());
            SqlConnection conn = connection();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT 食材數量 FROM 食材 WHERE 食材id = @foodId", conn);
                conn.Open();
                cmd.Parameters.Add("@foodId", SqlDbType.Int).Value = query;
                SqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                cmd.Dispose();
                if (sqlDataReader1.HasRows)
                {
                    label1.Text = "";
                    sqlDataReader1.Read();
                    label1.Text = "剩下 " + sqlDataReader1.GetInt32(0).ToString() + " 個";
                }
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
