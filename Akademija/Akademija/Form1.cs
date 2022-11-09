using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akademija
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void prikaziStudente()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\NovaAkademija.xls;Extended Properties=\"Excel 8.0\"";
            String strSQL = "Select * from Studenti;";
            OleDbCommand newComm = new OleDbCommand(strSQL, conn);
            OleDbDataReader reader;
            conn.Open();
            reader = newComm.ExecuteReader();
            int i = 0;
            this.lvStudenti.Items.Clear();
            while (reader.Read())
            {
                this.lvStudenti.Items.Add(reader["id"].ToString());

                this.lvStudenti.Items[i].SubItems.Add(reader["Ime"].ToString());
                this.lvStudenti.Items[i].SubItems.Add(reader["Prezime"].ToString());
                i++;
            }
            conn.Close();
        }
        private void prikaziPredmete()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\NovaAkademija.xls;Extended Properties=\"Excel 8.0\"";
            String strSQL = "Select * from Predmeti;";
            OleDbCommand newComm = new OleDbCommand(strSQL, conn);
            OleDbDataReader reader;
            conn.Open();
            reader = newComm.ExecuteReader();
            int i = 0;
            this.lvPredmeti.Items.Clear();
            while (reader.Read())
            {
                this.lvPredmeti.Items.Add(reader["id"].ToString());

                this.lvPredmeti.Items[i].SubItems.Add(reader["Naziv"].ToString());
                i++;
            }
            conn.Close();
        }
        private void prikayiIspite()
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            NoviStudent f = new NoviStudent();
            if(f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            NoviPredmet f = new NoviPredmet();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                f.Dispose();
                this.Form1_Load(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrijavaIspita f = new PrijavaIspita();
            f.ShowDialog(this);
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.prikaziStudente();
                this.prikaziPredmete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: "+ex.Message);
            }
        }

        private void lvStudenti_Click(object sender, EventArgs e)
        {
            try
            {
                var item = this.lvStudenti.SelectedItems[0];
                string id = item.SubItems[0].Text;
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\NovaAkademija.xls;Extended Properties=\"Excel 8.0\"";
                String strSQL = "SELECT Predmeti.Naziv " +
                    "            FROM (Ispiti INNER JOIN Studenti  ON Ispiti.Student_id=Studenti.id)  " +
                    "            INNER JOIN Predmeti " +
                    "            ON Predmeti.id=Ispiti.Predmet_id  " +
                    "            WHERE Studenti.id=" + id;

                OleDbCommand newComm = new OleDbCommand(strSQL, conn);
                OleDbDataReader reader;
                conn.Open();
                reader = newComm.ExecuteReader();
                int i = 0;
                this.lbIspiti.Items.Clear();
                while (reader.Read())
                {
                    lbIspiti.Items.Add(reader["Naziv"].ToString());
                    i++;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }
    }
}
