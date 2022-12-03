using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;          //Library
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.OleDb;

namespace Alpha_Foodie_POS
{
   
    public partial class register_form : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
        );
        public register_form()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data SOurce=db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();


        private void register_form_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            new Login().Show();
            Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                password.PasswordChar = '\0';
                confirmpswd.PasswordChar = '\0';

            }
            else
            {
                password.PasswordChar = '●';
                confirmpswd.PasswordChar = '●';

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(name.Text=="" && password.Text == "" && confirmpswd.Text=="")
            {
                MessageBox.Show("Username and Password field are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if(password.Text==confirmpswd.Text)
            {
                con.Open();
                string register= "INSERT INTO tbl_users VALUES('"+name.Text+ "','" + password.Text + "')";
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();

                name.Text = "";
                password.Text = "";
                confirmpswd.Text = "";

                MessageBox.Show("Your Account has been Successfully Created", "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Password doesnot match, Please Re-Enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                password.Text = "";
                confirmpswd.Text = "";
                password.Focus();
            }


        }
    }
}
