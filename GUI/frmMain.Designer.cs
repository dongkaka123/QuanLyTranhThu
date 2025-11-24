namespace GUI
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Panel panelUser;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.panelUser = new System.Windows.Forms.Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.panelLogo.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(281, 1000);
            this.panelMenu.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(281, 0);
            this.panelContent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelContent.Size = new System.Drawing.Size(1294, 1000);
            this.panelContent.TabIndex = 1;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(56)))));
            this.panelLogo.Controls.Add(this.lblAppName);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 100);
            this.panelLogo.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(0, 0);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(250, 100);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "QUẢN LÝ\r\nTRANH THỦ";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelUser
            // 
            this.panelUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(61)))), ((int)(((byte)(76)))));
            this.panelUser.Controls.Add(this.lblUserInfo);
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUser.Location = new System.Drawing.Point(0, 100);
            this.panelUser.Name = "panelUser";
            this.panelUser.Padding = new System.Windows.Forms.Padding(10);
            this.panelUser.Size = new System.Drawing.Size(250, 80);
            this.panelUser.TabIndex = 1;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserInfo.ForeColor = System.Drawing.Color.White;
            this.lblUserInfo.Location = new System.Drawing.Point(10, 10);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(230, 60);
            this.lblUserInfo.TabIndex = 0;
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1575, 1000);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Tranh thủ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panelLogo.ResumeLayout(false);
            this.panelUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            // Thêm panel Logo và User vào Menu
            this.panelMenu.Controls.Add(this.panelUser);
            this.panelMenu.Controls.Add(this.panelLogo);

            // Tạo menu buttons
            CreateMenuButtons();

            // Thêm welcome message
            AddWelcomeMessage();
        }

        private void CreateMenuButtons()
        {
            var user = UserSession.CurrentUser;
            int yPos = 180;

            // Menu cho tất cả user
            AddMenuButton("📝 Đơn của tôi", btnMyApplications_Click, ref yPos);
            AddMenuButton("➕ Tạo đơn mới", btnNewApplication_Click, ref yPos);

            // Menu cho admin
            if (user != null && user.IDQuyenHT == 1)
            {
                AddMenuButton("✅ Duyệt đơn", btnApproval_Click, ref yPos);
                AddMenuButton("📋 Quản lý đợt phép", btnManagePeriod_Click, ref yPos);
                AddMenuButton("🎫 Cấp giấy phép", btnIssuePermit_Click, ref yPos);
                AddMenuButton("👥 Quản lý quân nhân", btnManagePersonnel_Click, ref yPos);
                AddMenuButton("📊 Thống kê báo cáo", btnStatistics_Click, ref yPos);
            }

            AddMenuButton("📖 Theo dõi nghỉ phép", btnTrackLeave_Click, ref yPos);
            AddMenuButton("🔐 Đổi mật khẩu", btnChangePassword_Click, ref yPos);

            // Button đăng xuất ở cuối
            System.Windows.Forms.Button btnLogout = new System.Windows.Forms.Button();
            btnLogout.Text = "🚪 Đăng xuất";
            btnLogout.Size = new System.Drawing.Size(250, 50);
            btnLogout.Location = new System.Drawing.Point(0, this.panelMenu.Height - 60);
            btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogout.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            btnLogout.ForeColor = System.Drawing.Color.White;
            btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F);
            btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnLogout.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += new System.EventHandler(btnLogout_Click);
            this.panelMenu.Controls.Add(btnLogout);
        }

        private void AddMenuButton(string text, System.EventHandler clickHandler, ref int yPos)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            btn.Text = text;
            btn.Location = new System.Drawing.Point(0, yPos);
            btn.Size = new System.Drawing.Size(250, 50);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.BackColor = System.Drawing.Color.FromArgb(41, 50, 65);
            btn.ForeColor = System.Drawing.Color.White;
            btn.Font = new System.Drawing.Font("Segoe UI", 11F);
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(52, 61, 76);
            btn.MouseLeave += (s, e) => btn.BackColor = System.Drawing.Color.FromArgb(41, 50, 65);
            btn.Click += clickHandler;
            this.panelMenu.Controls.Add(btn);
            yPos += 50;
        }

        private void AddWelcomeMessage()
        {
            System.Windows.Forms.Label lblWelcome = new System.Windows.Forms.Label();
            lblWelcome.Text = "Chào mừng đến với Hệ thống Quản lý Tranh thủ";
            lblWelcome.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblWelcome.ForeColor = System.Drawing.Color.FromArgb(41, 50, 65);
            lblWelcome.Location = new System.Drawing.Point(50, 50);
            lblWelcome.AutoSize = true;
            this.panelContent.Controls.Add(lblWelcome);

            System.Windows.Forms.Label lblSubtitle = new System.Windows.Forms.Label();
            lblSubtitle.Text = "Chọn chức năng từ menu bên trái để bắt đầu";
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            lblSubtitle.Location = new System.Drawing.Point(50, 100);
            lblSubtitle.AutoSize = true;
            this.panelContent.Controls.Add(lblSubtitle);
        }
    }
}