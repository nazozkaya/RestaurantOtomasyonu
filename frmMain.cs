
using RM;
using RM.Model;
using RM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    public partial class frmMain: Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        static frmMain _obj;

            public static frmMain Instance
        {
            get {  if (_obj == null) { _obj = new frmMain(); } return _obj; }
        }
        public  void AddControls(Form f)

        {
            CenterPanel.Controls.Clear(); 
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenview());
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView());
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           lblKullanıcı.Text = MainClass.USER;  
            _obj = this;
        }

        private void btnEv_Click(object sender, EventArgs e)
        {
            AddControls(new frmEv());
        }

        private void CenterPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMasalar_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }
    }
}
