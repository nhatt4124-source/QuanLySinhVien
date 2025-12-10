using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using tan.Models;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        private StudentContext context;

        public Form1()
        {
            InitializeComponent();
            context = new StudentContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                List<Faculty> listFacultys = context.Faculties.ToList();
                FillFacultyCombobox(listFacultys);

                
                List<Student> listStudent = context.Students.ToList();
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void FillFacultyCombobox(List<Faculty> listFacultys)
        {
            this.comboBox1.DataSource = listFacultys;
            this.comboBox1.DisplayMember = "FacultyName";
            this.comboBox1.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!ValidateInputs())
                {
                    return;
                }

                string studentID = textBox1.Text.Trim();

              
                Student existingStudent = context.Students.FirstOrDefault(p => p.StudentID == studentID);
                if (existingStudent != null)
                {
                    MessageBox.Show("Mã sinh viên này đã tồn tại!", "Lỗi");
                    return;
                }

                
                Student newStudent = new Student()
                {
                    StudentID = studentID,
                    FullName = textBox2.Text.Trim(),
                    FacultyID = (int)comboBox1.SelectedValue,
                    AverageScore = double.Parse(textBox3.Text.Trim())
                };

                
                context.Students.Add(newStudent);
                context.SaveChanges();

                
                List<Student> listStudent = context.Students.ToList();
                BindGrid(listStudent);

                ClearInputs();
                MessageBox.Show("Thêm mới dữ liệu thành công!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!ValidateInputs())
                {
                    return;
                }

                string studentID = textBox1.Text.Trim();

                
                Student dbUpdate = context.Students.FirstOrDefault(p => p.StudentID == studentID);
                if (dbUpdate == null)
                {
                    MessageBox.Show("Không tìm thấy MSSV cần sửa!", "Lỗi");
                    return;
                }

               
                dbUpdate.FullName = textBox2.Text.Trim();
                dbUpdate.FacultyID = (int)comboBox1.SelectedValue;
                dbUpdate.AverageScore = double.Parse(textBox3.Text.Trim());

               
                context.SaveChanges();

                
                List<Student> listStudent = context.Students.ToList();
                BindGrid(listStudent);

                ClearInputs();
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng nhập MSSV!", "Lỗi");
                    return;
                }

                string studentID = textBox1.Text.Trim();

              
                Student dbDelete = context.Students.FirstOrDefault(p => p.StudentID == studentID);
                if (dbDelete == null)
                {
                    MessageBox.Show("Không tìm thấy MSSV cần xóa!", "Lỗi");
                    return;
                }

               
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                   
                    context.Students.Remove(dbDelete);
                    context.SaveChanges();

                   
                    List<Student> listStudent = context.Students.ToList();
                    BindGrid(listStudent);

                    ClearInputs();
                    MessageBox.Show("Xóa sinh viên thành công!", "Thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private bool ValidateInputs()
        {
            
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi");
                return false;
            }

           
            string studentID = textBox1.Text.Trim();
            if (studentID.Length != 10)
            {
                MessageBox.Show("Mã số sinh viên phải có 10 kí tự!", "Lỗi");
                return false;
            }

            
            if (!double.TryParse(textBox3.Text.Trim(), out double score))
            {
                MessageBox.Show("Điểm TB phải là một số!", "Lỗi");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                
                textBox1.Text = row.Cells[0].Value?.ToString() ?? "";
                textBox2.Text = row.Cells[1].Value?.ToString() ?? "";

              
                string studentID = textBox1.Text;
                Student student = context.Students.FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    comboBox1.SelectedValue = student.FacultyID;
                }

                textBox3.Text = row.Cells[3].Value?.ToString() ?? "";
            }
        }
    }
}
