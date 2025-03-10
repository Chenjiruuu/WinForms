using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Edit_Page : Form
    {
        private Student_Page studentPage;
        private ComboBox CourseCmb, YearCmb;
        private TextBox[] TextBoxes;
        private System.Windows.Forms.Label[] Labels;

        private string[] FieldNames =
        {
            "Name", "Age", "Address", "Contact Number", "Email Address",
            "Course", "Year", "Guardian/Parent", "Guardian Contact", "Hobbies", "Nickname"
        };

        public Button SaveBtn { get; private set; }

        public Edit_Page(Student_Page studentPage)
        {
            this.studentPage = studentPage;
            InitializeCustomComponents();
            LoadStudentData();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Edit Student Profile";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Labels = new System.Windows.Forms.Label[FieldNames.Length]; // Initialize Labels array
            TextBoxes = new TextBox[FieldNames.Length - 2]; // Excluding Course & Year (ComboBoxes)

            int yOffset = 20;
            for (int i = 0; i < FieldNames.Length; i++)
            {
                Labels[i] = new System.Windows.Forms.Label
                {
                    Text = FieldNames[i] + ":",
                    Location = new Point(20, yOffset),
                    AutoSize = true
                };
                Controls.Add(Labels[i]);

                if (i == 5) // Course Dropdown
                {
                    CourseCmb = new ComboBox
                    {
                        Location = new Point(180, yOffset - 3),
                        Width = 250,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    CourseCmb.Items.AddRange(new string[] { "ABEL", "BSBA", "BSIT", "BPA" });
                    Controls.Add(CourseCmb);
                }
                else if (i == 6) // Year Dropdown
                {
                    YearCmb = new ComboBox
                    {
                        Location = new Point(180, yOffset - 3),
                        Width = 250,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    YearCmb.Items.AddRange(new string[] { "First", "Second", "Third", "Fourth" });
                    Controls.Add(YearCmb);
                }
                else // Normal Textboxes
                {
                    TextBoxes[i > 6 ? i - 2 : i] = new TextBox
                    {
                        Location = new Point(180, yOffset - 3),
                        Width = 250
                    };

                    // Validate Age and Contact Numbers (Numbers Only)
                    if (i == 1 || i == 3 || i == 8)
                    {
                        TextBoxes[i > 6 ? i - 2 : i].KeyPress += NumericOnly_KeyPress;
                    }

                    Controls.Add(TextBoxes[i > 6 ? i - 2 : i]);
                }

                yOffset += 35;
            }

            // Save Button
            SaveBtn = new Button
            {
                Text = "Save/Update",
                Location = new Point(180, yOffset + 10),
                Width = 120
            };
            SaveBtn.Click += SaveBtn_Click; // Attach click event
            Controls.Add(SaveBtn);
        }

        private void LoadStudentData()
        {
            TextBoxes[0].Text = studentPage.StudentName;
            TextBoxes[1].Text = studentPage.StudentAge.ToString();
            TextBoxes[2].Text = studentPage.Address;
            TextBoxes[3].Text = studentPage.ContactNumber;
            TextBoxes[4].Text = studentPage.Email;
            CourseCmb.SelectedItem = studentPage.Course;
            YearCmb.SelectedItem = studentPage.Year;
            TextBoxes[5].Text = studentPage.Guardian;
            TextBoxes[6].Text = studentPage.GuardianContact;
            TextBoxes[7].Text = studentPage.Hobbies;
            TextBoxes[8].Text = studentPage.Nickname;
        }

        private void SaveStudentData()
        {
            studentPage.StudentName = TextBoxes[0].Text;
            studentPage.StudentAge = int.Parse(TextBoxes[1].Text);
            studentPage.Address = TextBoxes[2].Text;
            studentPage.ContactNumber = TextBoxes[3].Text;
            studentPage.Email = TextBoxes[4].Text;
            studentPage.Course = CourseCmb.SelectedItem.ToString();
            studentPage.Year = YearCmb.SelectedItem.ToString();
            studentPage.Guardian = TextBoxes[5].Text;
            studentPage.GuardianContact = TextBoxes[6].Text;
            studentPage.Hobbies = TextBoxes[7].Text;
            studentPage.Nickname = TextBoxes[8].Text;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(TextBoxes[0].Text) ||  // Name
                string.IsNullOrWhiteSpace(TextBoxes[1].Text) ||  // Age
                string.IsNullOrWhiteSpace(TextBoxes[2].Text) ||  // Address
                string.IsNullOrWhiteSpace(TextBoxes[3].Text) ||  // Contact Number
                string.IsNullOrWhiteSpace(TextBoxes[4].Text) ||  // Email
                CourseCmb.SelectedIndex == -1 ||                // Course
                YearCmb.SelectedIndex == -1 ||                  // Year
                string.IsNullOrWhiteSpace(TextBoxes[5].Text) ||  // Guardian
                string.IsNullOrWhiteSpace(TextBoxes[6].Text))    // Guardian Contact
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveStudentData();

            // Show success message
            MessageBox.Show("Profile successfully updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh Student_Page data
            studentPage.LoadStudentData();

            // Close Edit_Page and return to Student_Page
            studentPage.Show();
            this.Close();
        }


        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are allowed in this field.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
