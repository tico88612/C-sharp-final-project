using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace 餐廳管理系統
{
    public partial class Order : Form
    {
        static int b = 0, p = 0, c = 0, n = 0, s = 0;
        static int total = 0;
        static String bill = "";
        public Order()
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
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked &&
            !checkBox4.Checked && !checkBox5.Checked)
                MessageBox.Show("尚未點餐！");
            else if ((checkBox1.Checked && (int.TryParse(textBox1.Text, out b) == false || int.Parse(textBox1.Text) <= 0)) ||
               (checkBox2.Checked && (int.TryParse(textBox2.Text, out p) == false || int.Parse(textBox2.Text) <= 0)) ||
               (checkBox3.Checked && (int.TryParse(textBox3.Text, out c) == false || int.Parse(textBox3.Text) <= 0)) ||
               (checkBox4.Checked && (int.TryParse(textBox4.Text, out n) == false || int.Parse(textBox4.Text) <= 0)) ||
               (checkBox5.Checked && (int.TryParse(textBox5.Text, out s) == false || int.Parse(textBox5.Text) <= 0)))
                MessageBox.Show("份數輸入錯誤！");
            else
            {
                order();
                bill += "\r\n總共 " + total.ToString() + "元";
                label6.Text = "日期：" + DateTime.Now.ToShortDateString() + "　時間：" + DateTime.Now.ToShortTimeString() + "\r\n\r\n" + bill;
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bill = "";
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked &&
               !checkBox4.Checked && !checkBox5.Checked)
            {
                MessageBox.Show("已取消所有餐點！");
                label6.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else if ((checkBox1.Checked && (int.TryParse(textBox1.Text, out b) == false || int.Parse(textBox1.Text) <= 0)) ||
                (checkBox2.Checked && (int.TryParse(textBox2.Text, out p) == false || int.Parse(textBox2.Text) <= 0)) ||
                (checkBox3.Checked && (int.TryParse(textBox3.Text, out c) == false || int.Parse(textBox3.Text) <= 0)) ||
                (checkBox4.Checked && (int.TryParse(textBox4.Text, out n) == false || int.Parse(textBox4.Text) <= 0)) ||
                (checkBox5.Checked && (int.TryParse(textBox5.Text, out s) == false || int.Parse(textBox5.Text) <= 0)))
                MessageBox.Show("份數輸入錯誤！");
            else
            {
                order();
                bill += "\r\n總共 " + total.ToString() + "元";
                label6.Text = "日期：" + DateTime.Now.ToShortDateString() + "　時間：" + DateTime.Now.ToShortTimeString() + "\r\n\r\n" + bill;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //To-Do SQL
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label6.Text = "";
            bill = "";
        }
        private void order()
        {
            Total();
            if (checkBox1.Checked)
            {
                bill += "牛排　　 " + textBox1.Text + " 份 " + b.ToString() + " 元\r\n";
            }
            if (checkBox2.Checked)
            {
                bill += "豬排　　 " + textBox2.Text + " 份 " + p.ToString() + " 元\r\n";
            }
            if (checkBox3.Checked)
            {
                bill += "雞排　　 " + textBox3.Text + " 份 " + c.ToString() + " 元\r\n";
            }
            if (checkBox4.Checked)
            {
                bill += "鐵板麵　 " + textBox4.Text + " 份 " + n.ToString() + " 元\r\n";
            }
            if (checkBox5.Checked)
            {
                bill += "酥皮濃湯 " + textBox5.Text + " 份 " + s.ToString() + "元\r\n";
            }
            Discount();
        }

        private void Discount()
        {
            if (total >= 1000)
            {
                bill += "＊滿 1000 元優惠折 150 元\r\n";
                total -= 150;
            }
            else if (total >= 500)
            {
                bill += "＊滿 500 元優惠折 70 元\r\n";
                total -= 70;
            }
        }

        private void Total()
        {
            b = 0; p = 0; c = 0; n = 0; s = 0;
            total = 0;
            if (checkBox1.Checked)
            {
                b = 160 * int.Parse(textBox1.Text);
            }
            if (checkBox2.Checked)
            {
                p = 150 * int.Parse(textBox2.Text);
            }
            if (checkBox3.Checked)
            {
                c = 140 * int.Parse(textBox3.Text);
            }
            if (checkBox4.Checked)
            {
                n = 80 * int.Parse(textBox4.Text);
            }
            if (checkBox5.Checked)
            {
                s = 60 * int.Parse(textBox5.Text);
            }
            total = b + p + c + n + s;
        }
    }
}
