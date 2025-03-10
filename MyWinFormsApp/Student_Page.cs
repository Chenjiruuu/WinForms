using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Student_Page : Form
    {
        private PictureBox StudentPicture;
        private Button AddImageBtn, ChangeImageBtn, EditBtn;
        private Label[] Labels;
        private TextBox[] TextBoxes;

        // Properties to store student data
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public string Year { get; set; }
        public string Guardian { get; set; }
        public string GuardianContact { get; set; }
        public string Hobbies { get; set; }
        public string Nickname { get; set; }

        private string[] FieldNames =
        {
            "Name", "Age", "Address", "Contact Number", "Email Address",
            "Course", "Year", "Parent/Guardian", "Parent/Guardian Contact Number",
            "Hobbies", "Nickname"
        };

        public Student_Page()
        {
            StudentName = "Charles Quitaneg";
            StudentAge = 20;
            Address = "Warey, Malasiqui, Pangasinan";
            ContactNumber = "09518934608";
            Email = "quitanegcharles@gmail.com";
            Course = "BSIT";
            Year = "Fourth";
            Guardian = "Ricardo Quitaneg";
            GuardianContact = "0912345678";
            Hobbies = "Basketball";
            Nickname = "Chen";

            InitializeCustomComponents();
            LoadStudentData(); // Load data when opening
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Student Page";
            this.Size = new Size(500, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            StudentPicture = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(StudentPicture);

            AddImageBtn = new Button
            {
                Text = "Add Image",
                Location = new Point(160, 40),
                Width = 100
            };
            AddImageBtn.Click += AddImageBtn_Click;
            Controls.Add(AddImageBtn);

            ChangeImageBtn = new Button
            {
                Text = "Change Image",
                Location = new Point(160, 80),
                Width = 100
            };
            ChangeImageBtn.Click += AddImageBtn_Click;
            Controls.Add(ChangeImageBtn);

            Labels = new Label[FieldNames.Length];
            TextBoxes = new TextBox[FieldNames.Length];

            int yOffset = 150;
            for (int i = 0; i < FieldNames.Length; i++)
            {
                Labels[i] = new Label
                {
                    Text = FieldNames[i] + ":",
                    Location = new Point(20, yOffset),
                    AutoSize = true
                };
                Controls.Add(Labels[i]);

                TextBoxes[i] = new TextBox
                {
                    Location = new Point(180, yOffset - 3),
                    Width = 250,
                    ReadOnly = true
                };
                Controls.Add(TextBoxes[i]);

                yOffset += 35;
            }

            EditBtn = new Button
            {
                Text = "Edit/Update",
                Location = new Point(180, yOffset + 10),
                Width = 100
            };
            EditBtn.Click += EditBtn_Click;
            Controls.Add(EditBtn);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Edit_Page editForm = new Edit_Page(this); // Pass current form
            editForm.Show();
            this.Hide();
        }

        public void LoadStudentData()
        {
            TextBoxes[0].Text = StudentName;
            TextBoxes[1].Text = StudentAge.ToString();
            TextBoxes[2].Text = Address;
            TextBoxes[3].Text = ContactNumber;
            TextBoxes[4].Text = Email;
            TextBoxes[5].Text = Course;
            TextBoxes[6].Text = Year;
            TextBoxes[7].Text = Guardian;
            TextBoxes[8].Text = GuardianContact;
            TextBoxes[9].Text = Hobbies;
            TextBoxes[10].Text = Nickname;
        }

        private void AddImageBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StudentPicture.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }
    }
}
