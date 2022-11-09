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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Akademija
{
    public partial class NoviStudent : Form
    {
        public NoviStudent()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\NovaAkademija.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";
                //conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\NovaAkademija.xls;Extended Properties='Excel 8.0'";
                //MessageBox.Show(conn.ConnectionString);
                int id = Int32.Parse(txtID.Text);
                string Ime = txtIme.Text;
                string Prezime = txtPrezime.Text;
                string querystring = "INSERT INTO Studenti (id,Ime,Prezime)  VALUES (@id,@Ime,@Prezime)";

                conn.Open();
                OleDbCommand cmd = new OleDbCommand(querystring, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Ime", Ime);
                cmd.Parameters.AddWithValue("@Prezime", Prezime);
                // izvrsi sql upit
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Студент успешно унет у базу података!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Постоји проблем: " + ex.Message); ;
            }
        }
    }
}
