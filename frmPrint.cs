using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    public partial class frmPrint: Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        public int MainID = 0;
        private DataTable billData;

        private void frmPrint_Load(object sender, EventArgs e)
        {
            btnMax.PerformClick();
            LoadBillData();
            PrintBill();
        }

        private void LoadBillData()
        {
            if (MainID == 0) return;

            string qry = @"Select * from tblMain m 
                             inner join tblDetails d on m.MainID = d.MainID
                             inner join products p on p.pID = d.proID 
                             Where m.MainID = " + MainID;

            billData = new DataTable();
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(billData);
        }

        private void PrintBill()
        {
            if (billData == null || billData.Rows.Count == 0)
            {
                MessageBox.Show("Fatura verisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
            
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (billData == null || billData.Rows.Count == 0) return;

            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font normalFont = new Font("Arial", 10);
            Font smallFont = new Font("Arial", 8);

            float yPos = 50;
            float leftMargin = 50;
            float rightMargin = e.MarginBounds.Right;

            // Başlık
            string title = "FATURA";
            g.DrawString(title, titleFont, Brushes.Black, leftMargin, yPos);
            yPos += 40;

            // Fatura Bilgileri
            DataRow firstRow = billData.Rows[0];
            string tableName = firstRow["TableName"].ToString();
            string waiterName = firstRow["WaiterName"].ToString();
            string orderType = firstRow["orderType"].ToString();
            string orderDate = Convert.ToDateTime(firstRow["aDate"]).ToString("dd.MM.yyyy");
            string orderTime = firstRow["aTime"].ToString();
            double total = Convert.ToDouble(firstRow["total"]);

            g.DrawString("Tarih: " + orderDate + "  Saat: " + orderTime, normalFont, Brushes.Black, leftMargin, yPos);
            yPos += 25;

            if (!string.IsNullOrEmpty(tableName))
            {
                g.DrawString("Masa: " + tableName, normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 25;
            }

            if (!string.IsNullOrEmpty(waiterName))
            {
                g.DrawString("Garson: " + waiterName, normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 25;
            }

            if (!string.IsNullOrEmpty(orderType))
            {
                g.DrawString("Sipariş Türü: " + orderType, normalFont, Brushes.Black, leftMargin, yPos);
                yPos += 25;
            }

            yPos += 10;

            // Ürün Listesi Başlığı
            g.DrawString("Ürün Adı", headerFont, Brushes.Black, leftMargin, yPos);
            g.DrawString("Adet", headerFont, Brushes.Black, leftMargin + 200, yPos);
            g.DrawString("Fiyat", headerFont, Brushes.Black, leftMargin + 280, yPos);
            g.DrawString("Tutar", headerFont, Brushes.Black, leftMargin + 360, yPos);
            yPos += 30;

            // Çizgi
            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Ürünler
            foreach (DataRow row in billData.Rows)
            {
                string productName = row["pName"].ToString();
                int qty = Convert.ToInt32(row["qty"]);
                double price = Convert.ToDouble(row["price"]);
                double amount = Convert.ToDouble(row["amount"]);

                g.DrawString(productName, normalFont, Brushes.Black, leftMargin, yPos);
                g.DrawString(qty.ToString(), normalFont, Brushes.Black, leftMargin + 200, yPos);
                g.DrawString(price.ToString("N2") + " TL", normalFont, Brushes.Black, leftMargin + 280, yPos);
                g.DrawString(amount.ToString("N2") + " TL", normalFont, Brushes.Black, leftMargin + 360, yPos);
                yPos += 25;
            }

            yPos += 10;
            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 20;

            // Toplam
            g.DrawString("TOPLAM: " + total.ToString("N2") + " TL", headerFont, Brushes.Black, leftMargin + 280, yPos);
        }
    }
}
