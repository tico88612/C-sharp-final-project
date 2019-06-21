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
    public partial class WarehousingAdd : Form
    {
        public WarehousingAdd()
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
            SqlConnection conn = new SqlConnection(@"Data Source=4aee\sqlexpress;Initial Catalog=餐廳;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM 食材 WHERE 食材名稱 = @foodName", conn);
            cmd.Parameters.Add("@foodName", SqlDbType.NVarChar, 50).Value = textBox1.Text;
            SqlDataReader sqlDataReader1 = cmd.ExecuteReader();
            cmd.Dispose();
            string nowNum = textBox2.Text;
            if (sqlDataReader1.HasRows)
            {
                sqlDataReader1.Read();
                int change = Convert.ToInt32(sqlDataReader1.GetInt32(2)) + Convert.ToInt32(textBox2.Text);
                sqlDataReader1.Close();
                SqlCommand cmd2 = new SqlCommand("UPDATE 食材 SET 食材數量 = @foodNum WHERE 食材名稱 = @foodName", conn);
                cmd2.Parameters.Add("@foodName", SqlDbType.NVarChar, 50).Value = textBox1.Text;
                cmd2.Parameters.Add("@foodNum", SqlDbType.Int, 4).Value = change;
                nowNum = change.ToString();
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
            }
            else
            {
                sqlDataReader1.Close();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO 食材 (食材名稱, 食材數量) VALUES (@foodName, @foodNum)", conn);
                cmd2.Parameters.Add("@foodName", SqlDbType.NVarChar, 50).Value = textBox1.Text;
                cmd2.Parameters.Add("@foodNum", SqlDbType.Int, 4).Value = textBox2.Text;
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
            }
            SqlCommand cmd3 = new SqlCommand("INSERT INTO 銷售財務表 (銷售財務表memo, 銷售財務表類別, 銷售財務表營業額, 銷售財務表日期) VALUES(@memo, '支出', @money, GETDATE())", conn);
            cmd3.Parameters.Add("@money", SqlDbType.Int).Value = textBox3.Text;
            cmd3.Parameters.Add("@memo", SqlDbType.NVarChar, 50).Value = textBox1.Text + " 進貨了 " + textBox2.Text + " 個";
            cmd3.ExecuteNonQuery();
            cmd3.Dispose();
            MessageBox.Show("新增成功，"+ textBox1.Text.ToString() + " 有 "+ nowNum + " 個", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
