using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_With_Id_Identity
{
    public partial class Form1 : Form
    {

        int i;
        string cs = ConfigurationManager.ConnectionStrings["csdb"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
            BindGridView();
        }

        private void buttonIsertData_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "insert into customer values (@name, @catagory, @company, @price, @stalk)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@company", textBoxCompany.Text);
            cmd.Parameters.AddWithValue("@catagory", textBoxCatagory.Text);
            cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
            cmd.Parameters.AddWithValue("@stalk", textBoxStalk.Text);

            conn.Open();

            int n = cmd.ExecuteNonQuery();

            if (n > 0)
            {

                MessageBox.Show("Insertion Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else {


                MessageBox.Show("Insertion Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();



        }

        public void BindGridView() {


            SqlConnection conn = new SqlConnection(cs);
            string query = "select * from Customer";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridViewShowData.DataSource = data;
            dataGridViewShowData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        public void ResetControls() {

            textBoxName.Clear();
            textBoxCompany.Clear();
            textBoxCatagory.Clear();
            textBoxPrice.Clear();
            textBoxStalk.Clear();

        }

    
        private void dataGridViewShowData_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            i = Convert.ToInt32(dataGridViewShowData.SelectedRows[0].Cells[0].Value.ToString());
            textBoxName.Text = dataGridViewShowData.SelectedRows[0].Cells[1].Value.ToString();
            textBoxCompany.Text = dataGridViewShowData.SelectedRows[0].Cells[2].Value.ToString();
            textBoxCatagory.Text = dataGridViewShowData.SelectedRows[0].Cells[3].Value.ToString();
            textBoxPrice.Text = dataGridViewShowData.SelectedRows[0].Cells[4].Value.ToString();
            textBoxStalk.Text = dataGridViewShowData.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void buttonUpdateData_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(cs);
            string query = "update Customer set Name = @name, Company = @company, Catagory = @catagory, Price = @price, Stalk = @stalk where Id = @i";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@i", i);
            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@company", textBoxCompany.Text);
            cmd.Parameters.AddWithValue("@catagory", textBoxCatagory.Text);
            cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
            cmd.Parameters.AddWithValue("@stalk", textBoxStalk.Text);

            conn.Open();

            int n = cmd.ExecuteNonQuery();

            if (n > 0)
            {

                MessageBox.Show("Updation Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else
            {


                MessageBox.Show("Updation Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }

        private void buttonDeleteData_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(cs);
            string query = "delete from Customer where Id = @i";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@i", i);
            conn.Open();

            int n = cmd.ExecuteNonQuery();

            if (n > 0)
            {

                MessageBox.Show("Deletion Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControls();
            }
            else
            {


                MessageBox.Show("Deletion Failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }
    }
}
