using System;
using System.Drawing;
using System.Windows.Forms;
using GUI.Utils;

namespace GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var user = UserSession.CurrentUser;
            if (user != null)
            {
                lblUserInfo.Text = $"👤 {user.HoTen}\n📍 {user.TenDonVi}\n🎖️ {user.TenQuyen}";
            }
        }

        private void LoadForm(Form childForm)
        {
            panelContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContent.Controls.Add(childForm);
            childForm.Show();
        }

        private void btnMyApplications_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Đơn của tôi đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmMyApplications());
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Tạo đơn mới đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmNewApplication());
        }

        private void btnApproval_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Duyệt đơn đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmApproval());
        }

        private void btnManagePeriod_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý đợt phép đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmManagePeriod());
        }

        private void btnIssuePermit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Cấp giấy phép đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmIssuePermit());
        }

        private void btnManagePersonnel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Quản lý quân nhân đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmManagePersonnel());
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thống kê báo cáo đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmStatistics());
        }

        private void btnTrackLeave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Theo dõi nghỉ phép đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // LoadForm(new frmTrackLeave());
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Đổi mật khẩu đang phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // frmChangePassword frm = new frmChangePassword();
            // frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.CurrentUser = null;
                this.Hide();
                frmLogin loginForm = new frmLogin();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }
    }
}