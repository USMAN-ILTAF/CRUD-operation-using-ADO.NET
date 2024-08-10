using Microsoft.Data.SqlClient;
using System.Data;

namespace StudentApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=scholarshipDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public int id { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentInfo();
        }
        private void GetStudentInfo()
        {
            SqlCommand cmd = new SqlCommand("select * from StudentInfo", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            RecorddataGridView.DataSource= dt;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentInfo VALUES (@name,@RegNumber,@Phone )",con);
                cmd.CommandType= CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@RegNumber", textBoxRegNo.Text);
                cmd.Parameters.AddWithValue("@Phone", textBoxPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close() ;
                MessageBox.Show("New student Added","saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                GetStudentInfo();
                Clear();
            }
        }
        private bool Isvalid()
        {
            if(textBoxName.Text == "")
            {
                MessageBox.Show("Name is Required","failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            return false;
            }
            if (textBoxRegNo.Text == "")
            {
                MessageBox.Show("Registration number  is Required", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                if (textBoxPhone.Text=="")
            {
                MessageBox.Show("Number is Required", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            
            return true;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            textBoxName.Clear();
            textBoxPhone.Clear();
            textBoxRegNo.Clear();
            id = 0;
            textBoxName.Focus();
        }

        private void RecorddataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(RecorddataGridView.SelectedRows[0].Cells[0].Value);
            textBoxName.Text = RecorddataGridView.SelectedRows[0].Cells[1].Value.ToString();
            textBoxRegNo.Text = RecorddataGridView.SelectedRows[0].Cells[2].Value.ToString();
            textBoxPhone.Text = RecorddataGridView.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentInfo SET Name=@name,Reg_Number=@RegNumber,Phone= @Phone WHERE Id= @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@RegNumber", textBoxRegNo.Text);
                cmd.Parameters.AddWithValue("@Phone", textBoxPhone.Text);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Student Updated!!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentInfo();
                Clear();
            }
            else
            {
                MessageBox.Show("Please select a student to Update", "?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(id> 0) {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentInfo  WHERE Id= @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Student Deleted!!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentInfo();
                Clear();
            }
            else
            {
                MessageBox.Show("Please select a student to Delete", "?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}