using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public void Reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            //dataGridView1.ColumnCount = 0;
            //dataGridView2.ColumnCount = 0;
            //dataGridView3.ColumnCount = 0;
            //label6.Visible = false;
            //label7.Visible = false;


            //dataGridView1.Rows[0].Cells[0].Value = "1";
            //dataGridView1.Rows[0].Cells[1].Value = "1000";
            //dataGridView1.Rows[1].Cells[0].Value = "2";
            //dataGridView1.Rows[1].Cells[1].Value = "1200";

            //dataGridView1.Rows[0].Cells[0].Value = "1";
            //dataGridView1.Rows[0].Cells[1].Value = "10";
            //dataGridView1.Rows[1].Cells[0].Value = "4";
            //dataGridView1.Rows[1].Cells[1].Value = "2";

            //dataGridView2.Rows[0].Cells[0].Value = "3";
            //dataGridView2.Rows[0].Cells[1].Value = "0";
            //dataGridView2.Rows[1].Cells[0].Value = "4";
            //dataGridView2.Rows[1].Cells[1].Value = "0";
            //dataGridView2.Rows[2].Cells[0].Value = "5";
            //dataGridView2.Rows[2].Cells[1].Value = "-800";
            //dataGridView2.Rows[3].Cells[0].Value = "6";
            //dataGridView2.Rows[3].Cells[1].Value = "-900";


            //dataGridView2.Rows[0].Cells[0].Value = "2";
            //dataGridView2.Rows[0].Cells[1].Value = "0";
            //dataGridView2.Rows[1].Cells[0].Value = "4";
            //dataGridView2.Rows[1].Cells[1].Value = "2";
            //dataGridView2.Rows[2].Cells[0].Value = "5";
            //dataGridView2.Rows[2].Cells[1].Value = "0";
            //dataGridView2.Rows[3].Cells[0].Value = "6";
            //dataGridView2.Rows[3].Cells[1].Value = "-1";
            //dataGridView2.Rows[4].Cells[0].Value = "7";
            //dataGridView2.Rows[4].Cells[1].Value = "0";

            //dataGridView3.Rows[0].Cells[0].Value = "5";
            //dataGridView3.Rows[0].Cells[1].Value = "-800";
            //dataGridView3.Rows[1].Cells[0].Value = "6";
            //dataGridView3.Rows[1].Cells[1].Value = "-900";
            //dataGridView3.Rows[2].Cells[0].Value = "7";
            //dataGridView3.Rows[2].Cells[1].Value = "-500";

            //dataGridView3.Rows[0].Cells[0].Value = "3";
            //dataGridView3.Rows[0].Cells[1].Value = "-3";
            //dataGridView3.Rows[1].Cells[0].Value = "6";
            //dataGridView3.Rows[1].Cells[1].Value = "-1";
            //dataGridView3.Rows[2].Cells[0].Value = "8";
            //dataGridView3.Rows[2].Cells[1].Value = "-8";
        }

        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 2;
            int checkResult;
            if (textBox1.Text == "" || !int.TryParse(textBox1.Text, out checkResult))
            {
                dataGridView1.RowCount = 0;
               
            }
            else if (Convert.ToInt32(textBox1.Text) > 50)
            {
                dataGridView1.RowCount = 50;
            }
            else
            {
                dataGridView1.RowCount = (Convert.ToInt32(textBox1.Text));
            }
           
            dataGridView1.Columns[0].HeaderText = "Назва";
            dataGridView1.Columns[1].HeaderText = "К-ть запасів";
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.ColumnCount = 2;
            int checkResult;
            if (textBox2.Text == "" || !int.TryParse(textBox2.Text, out checkResult))
            {
                dataGridView2.RowCount = 0;

            }
            else if (Convert.ToInt32(textBox2.Text) > 50)
            {
                dataGridView2.RowCount = 50;
            }
            else
            {
                dataGridView2.RowCount = (Convert.ToInt32(textBox2.Text));
            }

            dataGridView2.Columns[0].HeaderText = "Назва";
            dataGridView2.Columns[1].HeaderText = "К-ть запасів";
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            label6.Visible = true;

            dataGridView2.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView3.ColumnCount = 2;
            int checkResult;
            if (textBox3.Text == "" || !int.TryParse(textBox3.Text, out checkResult))
            {
                dataGridView3.RowCount = 0;
            }
            else if (Convert.ToInt32(textBox3.Text) > 50)
            {
                dataGridView3.RowCount = 50;
            }
            else
            {
                dataGridView3.RowCount = (Convert.ToInt32(textBox3.Text));
            }

            dataGridView3.Columns[0].HeaderText = "Назва";
            dataGridView3.Columns[1].HeaderText = "К-ть потреб";
            dataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            label7.Visible = true;
            dataGridView3.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

            var dictionary1 = new SortedDictionary<string, int>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dictionary1.Add(dataGridView1.Rows[i].Cells[0].Value.ToString(), int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
            }

            var bufer = dictionary1.Sum(x => x.Value);

            var dictionary2 = new SortedDictionary<string, int>();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dictionary2.Add(dataGridView2.Rows[i].Cells[0].Value.ToString(), int.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()));
            }

            var dictionary3 = new SortedDictionary<string, int>();
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                dictionary3.Add(dataGridView3.Rows[i].Cells[0].Value.ToString(), int.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString()));
            }

            var dictionary4 = new SortedDictionary<string, int>();
            foreach (var item in dictionary1)
            {
                dictionary4.Add(item.Key, item.Value);
            }
            foreach (var item in dictionary2)
            {

                if (dictionary1.Any(x => x.Value == item.Value) || dictionary3.Any(x => x.Value == item.Value))
                {
                    if (dictionary1.Any(x => x.Value == item.Value))
                    {
                        dictionary4[item.Key] = item.Value + bufer;
                    }
                    else {
                        dictionary4.Add(item.Key, item.Value + bufer);
                    }
                }
                else
                {
                    dictionary4.Add(item.Key, bufer);
                }
            }


            var dictionary5 = new SortedDictionary<string, int>();
            foreach (var item in dictionary2)
            {
                dictionary5.Add(item.Key, bufer);
            }
            foreach (var item in dictionary3)
            {
                if (dictionary2.Any(x => x.Value == item.Value))
                {
                    if (dictionary2.Any(x => x.Value == item.Value))
                    {
                        dictionary5[item.Key] = bufer;
                    }
                    else {
                        dictionary5.Add(item.Key, bufer);
                    }
                }
                else
                { dictionary5.Add(item.Key, Math.Abs(item.Value)); }
            }

            //dataGridView6.Columns[dataGridView6.ColumnCount - 1].HeaderText = "Запаси";
            //dataGridView6.Rows[dataGridView6.RowCount - 1].HeaderCell.Value = "Потреби";

            //for (int i = 0; i < dictionary4.Count; i++)
            //{
            //    dataGridView6.Rows[i].Cells[dataGridView6.ColumnCount - 1].Value = dictionary4.Values.ElementAt(i).ToString();
            //}

            //for (int i = 0; i < dictionary5.Count; i++)
            //{
            //    dataGridView6.Rows[dataGridView6.RowCount - 1].Cells[i].Value = dictionary5.Values.ElementAt(i).ToString();
            //}

            dataGridView6.ColumnCount = dictionary5.Count + 1;
            dataGridView6.RowCount = dictionary4.Count + 1;

            for (int i = 0; i < dictionary5.Count; i++)
            {
                dataGridView6.Columns[i].HeaderText = dictionary5.Keys.ElementAt(i).ToString();

            }
            for (int i = 0; i < dictionary4.Count; i++)
            {
                dataGridView6.Rows[i].HeaderCell.Value = dictionary4.Keys.ElementAt(i).ToString();
            }
           
            dataGridView6.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView6.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;           

            if (dictionary5.Values.Sum() < dictionary4.Values.Sum())
            {
                dataGridView6.ColumnCount++;
                dataGridView6.Rows[dataGridView6.RowCount - 1].Cells[dataGridView6.ColumnCount - 2].Value = dictionary4.Values.Sum() - dictionary5.Values.Sum();
                dataGridView6.Columns[dataGridView6.ColumnCount -2].HeaderCell.Value = "Fictitious";
            }
            else if (dictionary5.Values.Sum() > dictionary4.Values.Sum())
            {
                dataGridView6.RowCount++;
               
                dataGridView6.Rows[dataGridView6.RowCount - 2].Cells[dataGridView6.ColumnCount - 1].Value = dictionary5.Values.Sum() - dictionary4.Values.Sum();
                dataGridView6.Rows[dataGridView6.RowCount - 2].HeaderCell.Value = "Fictitious";
            }

            dataGridView6.Columns[dataGridView6.ColumnCount - 1].HeaderText = "Запаси";
            dataGridView6.Rows[dataGridView6.RowCount - 1].HeaderCell.Value = "Потреби";

            for (int i = 0; i < dictionary4.Count; i++)
            {
                dataGridView6.Rows[i].Cells[dataGridView6.ColumnCount - 1].Value = dictionary4.Values.ElementAt(i).ToString();
            }

            for (int i = 0; i < dictionary5.Count; i++)
            {
                dataGridView6.Rows[dataGridView6.RowCount - 1].Cells[i].Value = dictionary5.Values.ElementAt(i).ToString();
            }

            var NeedCount = dataGridView6.ColumnCount - 1;
            var StockCount = dataGridView6.RowCount - 1;
            var Needs = new double[NeedCount];
            var Stocks = new double[StockCount];
            var Matrix = new double[StockCount, NeedCount];
            for (int i = 0; i < NeedCount; i++)
            {
                for (int j = 0; j < StockCount; j++)
                {
                    if (dataGridView6.Columns[i].HeaderText == dataGridView6.Rows[j].HeaderCell.Value.ToString())
                    {
                        dataGridView6.Rows[j].Cells[i].Value = "0";
                    }
                    else
                    {
                        dataGridView6.Rows[j].Cells[i].Value = "M";
                    }

                }
            }


            //dataGridView6.Rows[0].Cells[0].Value = "3";
            //dataGridView6.Rows[1].Cells[0].Value = "0";
            //dataGridView6.Rows[1].Cells[1].Value = "6";
            //dataGridView6.Rows[2].Cells[1].Value = "2";
            //dataGridView6.Rows[2].Cells[2].Value = "0";
            //dataGridView6.Rows[3].Cells[2].Value = "7";
            //dataGridView6.Rows[1].Cells[3].Value = "9";
            //dataGridView6.Rows[2].Cells[3].Value = "2";
            //dataGridView6.Rows[3].Cells[3].Value = "0";
            //dataGridView6.Rows[3].Cells[4].Value = "8";
            //dataGridView6.Rows[4].Cells[4].Value = "0";
            //dataGridView6.Rows[2].Cells[5].Value = "1";
            //dataGridView6.Rows[4].Cells[5].Value = "5";
            //dataGridView6.Rows[5].Cells[5].Value = "0";
            //dataGridView6.Rows[5].Cells[6].Value = "4";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var NeedCount = dataGridView6.ColumnCount - 1;
            var StockCount = dataGridView6.RowCount - 1;
            var Needs = new double[NeedCount];
            var Stocks = new double[StockCount];
            var Matrix = new double[StockCount, NeedCount];

            for (int i = 0; i < StockCount; i++)
            {
                Stocks[i] = double.Parse(dataGridView6.Rows[i].Cells[NeedCount].Value.ToString());
            }
            for (int i = 0; i < NeedCount; i++)
            {
                Needs[i] = double.Parse(dataGridView6.Rows[StockCount].Cells[i].Value.ToString());
            }
            for (int i = 0; i < NeedCount; i++)
            {
                for (int j = 0; j < StockCount; j++)
                {
                    if (dataGridView6.Rows[j].Cells[i].Value.ToString() == "M")
                    {
                        Matrix[j, i] = 999;
                    }
                    else
                    {
                        Matrix[j, i] = double.Parse(dataGridView6.Rows[j].Cells[i].Value.ToString());
                    }
                }

            }

            var m = new Matrix(StockCount, NeedCount, Stocks, Needs, Matrix);
            m.BuildFirstSupportingPlan();
            m.Calculate();

            for (int i = 0; i < StockCount; i++)
            {
                for (int j = 0; j < NeedCount; j++)
                {
                    if (dataGridView6.Rows[i].Cells[j].Value.ToString() != "M")
                    {
                        dataGridView6.Rows[i].Cells[j].Value = dataGridView6.Rows[i].Cells[j].Value.ToString() + "[" + m.Numbers[i, j].ToString() + "]";
                    }
                }
            }
            label4.Visible = false;
            label5.Visible = true;
            label5.Text = $"Оптимальний план: {m.res}";
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Load("Images/Restart1.png");
            pictureBox1.Focus();
            Reset();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Load("Images/RestartDown.png");
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Load("Images/RestartOver.png");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Load("Images/Restart1.png");
        }
    }
}