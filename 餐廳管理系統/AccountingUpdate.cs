using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 餐廳管理系統
{
    public partial class AccountingUpdate : Form
    {
        public AccountingUpdate()
        {
            InitializeComponent();
        }

        bool IsToHome = false; //紀錄是否要回到Form1
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private BindingSource bindingSource1 = new BindingSource();

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

        private void GetData(string selectCommand)
        {
            try
            {
                // Specify a connection string.  
                // Replace <SQL Server> with the SQL Server for your Northwind sample database.
                // Replace "Integrated Security=True" with user login information if necessary.
                String connectionString = @"Data Source=4aee\sqlexpress;Initial Catalog=餐廳;Integrated Security=True";

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. 
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void AccountingUpdate_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            GetData("SELECT * FROM 銷售財務表 ORDER By 銷售財務表日期 DESC");
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = connection();
            try
            {
                string strcolumn = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                string strrow = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string value = dataGridView1.CurrentCell.Value.ToString();
                string strcomm = "UPDATE 銷售財務表 SET " + strcolumn + "='" + value + "'where 銷售財務表id = " + strrow;
                conn.Open();
                SqlCommand comm = new SqlCommand(strcomm, conn);
                comm.ExecuteNonQuery();
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

        private SqlConnection connection()
        {
            string strconn = @"Data Source=4aee\sqlexpress;Initial Catalog=餐廳;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            return conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("請選擇欄位", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(MessageBox.Show("確定刪除", "提示訊息", MessageBoxButtons.OKCancel,MessageBoxIcon.Error) == DialogResult.OK)
            {
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    SqlConnection conn = connection();
                    try
                    {
                        string strrow = dataGridView1.Rows[item.Index].Cells[0].Value.ToString();
                        string strcomm = "DELETE FROM 銷售財務表 WHERE 銷售財務表id = " + strrow;
                        conn.Open();
                        SqlCommand comm = new SqlCommand(strcomm, conn);
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }
        }
    }
}
