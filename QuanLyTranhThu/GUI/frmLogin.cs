using System;
using System.Windows.Forms;
using BLL;
using DTO;

namespace QuanLyTranhThu.GUI
{
    public partial class frmLogin : Form
    {
        private NguoiDungBLL bll = new NguoiDungBLL();

        public frmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Text = "Đăng nhập - Quản lý Tranh thủ";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel chính
            Panel panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 30, 40, 30)
            };

            // Label tiêu đề
            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ TRANH THỦ",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(0, 122, 204),
                Location = new System.Drawing.Point(40, 30),
                Size = new System.Drawing.Size(370, 40),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            // Label tên đăng nhập
            Label lblUsername = new Label
            {
                Text = "Tên đăng nhập:",
                Location = new System.Drawing.Point(40, 100),
                Size = new System.Drawing.Size(120, 25),
                Font = new System.Drawing.Font("Segoe UI", 10F)
            };

            // TextBox tên đăng nhập
            txtUsername = new TextBox
            {
                Location = new System.Drawing.Point(40, 130),
                Size = new System.Drawing.Size(370, 30),
                Font = new System.Drawing.Font("Segoe UI", 11F)
            };

            // Label mật khẩu
            Label lblPassword = new Label
            {
                Text = "Mật khẩu:",
                Location = new System.Drawing.Point(40, 170),
                Size = new System.Drawing.Size(120, 25),
                Font = new System.Drawing.Font("Segoe UI", 10F)
            };

            // TextBox mật khẩu
            txtPassword = new TextBox
            {
                Location = new System.Drawing.Point(40, 200),
                Size = new System.Drawing.Size(370, 30),
                Font = new System.Drawing.Font("Segoe UI", 11F),
                PasswordChar = '●'
            };

            // Button đăng nhập
            btnLogin = new Button
            {
                Text = "Đăng nhập",
                Location = new System.Drawing.Point(40, 260),
                Size = new System.Drawing.Size(170, 40),
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += btnLogin_Click;

            // Button thoát
            Button btnExit = new Button
            {
                Text = "Thoát",
                Location = new System.Drawing.Point(240, 260),
                Size = new System.Drawing.Size(170, 40),
                Font = new System.Drawing.Font("Segoe UI", 11F),
                BackColor = System.Drawing.Color.FromArgb(224, 224, 224),
                ForeColor = System.Drawing.Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Click += (s, e) => Application.Exit();

            // Thêm controls vào form
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnExit);

            // Set Enter key cho login
            this.AcceptButton = btnLogin;
        }

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private System.ComponentModel.IContainer components = null;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = bll.Login(username, password);

            if (result.success)
            {
                // Lưu thông tin người dùng vào session
                UserSession.CurrentUser = result.user;

                MessageBox.Show($"Xin chào {result.user.HoTen}!\nĐăng nhập thành công.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở form chính
                this.Hide();
                frmMain mainForm = new frmMain();
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(result.message, "Lỗi đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    // Class lưu thông tin session người dùng
    public static class UserSession
    {
        public static UserSessionDTO CurrentUser { get; set; }
    }
}