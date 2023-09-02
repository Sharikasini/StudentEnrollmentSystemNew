using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace StudentEnrollmentSystemNew
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SHARIKASINI;Initial Catalog=Student;Integrated Security=True");

        private void HomePage_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }

            else
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You Are About To Delete This Record? Click 'YES' to delete the record", "Confirmation for Deleting a Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int regNo = int.Parse(comboBoxRN.Text);

                    string query_delete = "DELETE FROM [RegistrationTable] WHERE regNo = @regNo";

                    using (SqlCommand cmd = new SqlCommand(query_delete, Con))
                    {
                        cmd.Parameters.AddWithValue("@regNo", regNo);

                        Con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data Deleted Successfully", "Successfully Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("No records were deleted.", "Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    // Clear the form fields here
                    comboBoxRN.ResetText();
                    txtbxFN.Clear();
                    txtbxLN.Clear();
                    dateTimePicker1.ResetText();
                    txtbxage.Clear();
                    rbmale.Checked = false;
                    rbfemale.Checked = false;
                    txtbxad.Clear();
                    txtbxemail.Clear();
                    txtbxmp.Clear();
                    txtbxhp.Clear();
                    txtbxpn.Clear();
                    txtbxnic.Clear();
                    txtbxcn.Clear();

                    Register.Enabled = true;
                }
                else
                {
                    // Nothing will happen
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Con.Close();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxRN.Text == "" || txtbxFN.Text == "" || txtbxLN.Text == "" || txtbxad.Text == "" || txtbxemail.Text == "" || txtbxmp.Text == "" || txtbxnic.Text == "")
            {
                MessageBox.Show("Complete the missing data", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int regNo = int.Parse(comboBoxRN.Text);
                    string firstName = txtbxFN.Text;
                    string lastName = txtbxLN.Text;
                    DateTime dateOfBirth = dateTimePicker1.Value;
                    int Age = int.Parse(txtbxage.Text);
                    string gender;
                    if (rbmale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    string address = txtbxad.Text;
                    string email = txtbxemail.Text;
                    int mobliePhone = int.Parse(txtbxmp.Text);
                    int homePhone = int.Parse(txtbxhp.Text);
                    string parentName = txtbxpn.Text;
                    string nic = txtbxnic.Text;
                    int contactNo = int.Parse(txtbxcn.Text);


                    string query_insert = "Insert into [RegistrationTable] values ('" + regNo + "','" + firstName + "','" + lastName + "','" + dateOfBirth + "','" + gender + "','" + address + "', '" + email + "','" + mobliePhone + "','" + homePhone + "','" + parentName + "','" + nic + "','" + contactNo + "','" + Age + "')";
                    SqlCommand cmnd = new SqlCommand(query_insert, Con);
                    Con.Open();
                    if (int.Parse(txtbxage.Text) > 18)
                    {
                        cmnd.ExecuteNonQuery();
                        MessageBox.Show("Student Inserted successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (int.Parse(txtbxage.Text) == 0)
                    {


                    }
                    else
                    {
                        MessageBox.Show("Cannot Enroll - Below 18 years!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error while Registering!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime DateofBirth = dateTimePicker1.Value;
            DateTime CurrentDate = DateTime.Now;
            TimeSpan Age = CurrentDate - DateofBirth;
            double days = Age.TotalDays;
            txtbxage.Text = (days / 365).ToString("0");

            if (int.Parse(txtbxage.Text) > 18)
            {
                // nothing will happen
            }
            else if (int.Parse(txtbxage.Text) == 0)
            {
                Register.Enabled = false;

            }
            else
            {
                MessageBox.Show("Cannot Enroll-Below 18 years", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Register.Enabled = true;
                dateTimePicker1.ResetText();
                txtbxage.Clear();
                txtbxage.Enabled = false;
                Register.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            comboBoxRN.ResetText();
            txtbxFN.Clear();
            txtbxLN.Clear();
            dateTimePicker1.ResetText();
            txtbxage.Clear();
            rbmale.Checked = false;
            rbfemale.Checked = false;
            txtbxad.Clear();
            txtbxemail.Clear();
            txtbxmp.Clear();
            txtbxhp.Clear();
            txtbxpn.Clear();
            txtbxnic.Clear();
            txtbxcn.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do You Want to Update the data? Click 'YES' to Update the data", "Confirmation for Updating a Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int regNo = int.Parse(comboBoxRN.Text);
                    string firstName = txtbxFN.Text;
                    string lastName = txtbxLN.Text;
                    DateTime dateOfBirth = dateTimePicker1.Value;
                    int age = int.Parse(txtbxage.Text);
                    string gender = rbmale.Checked ? "Male" : "Female";
                    string address = txtbxad.Text;
                    string email = txtbxemail.Text;
                    int mobilePhone = int.Parse(txtbxmp.Text);
                    int homePhone = int.Parse(txtbxhp.Text);
                    string parentName = txtbxpn.Text;
                    string nic = txtbxnic.Text;
                    int contactNo = int.Parse(txtbxcn.Text);

                    string query_update = "UPDATE [RegistrationTable] SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, gender = @gender, address = @address, email = @email, mobilePhone = @mobilePhone, homePhone = @homePhone, parentName = @parentName, nic = @nic, contactNo = @contactNo, Age = @age WHERE regNo = @regNo";

                    using (SqlCommand cmd = new SqlCommand(query_update, Con))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@mobilePhone", mobilePhone);
                        cmd.Parameters.AddWithValue("@homePhone", homePhone);
                        cmd.Parameters.AddWithValue("@parentName", parentName);
                        cmd.Parameters.AddWithValue("@nic", nic);
                        cmd.Parameters.AddWithValue("@contactNo", contactNo);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@regNo", regNo);

                        Con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No records were updated.", "Not Updated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    // Clear the form fields here
                    comboBoxRN.ResetText();
                    txtbxFN.Clear();
                    txtbxLN.Clear();
                    dateTimePicker1.ResetText();
                    txtbxage.Clear();
                    rbmale.Checked = false;
                    rbfemale.Checked = false;
                    txtbxad.Clear();
                    txtbxemail.Clear();
                    txtbxmp.Clear();
                    txtbxhp.Clear();
                    txtbxpn.Clear();
                    txtbxnic.Clear();
                    txtbxcn.Clear();
                }
                else
                {
                    // Nothing will happen
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Con.Close();
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtbxmp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
