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
    public partial class PrijavaIspita : Form
    {
        private List<int> StudentsIds = new List<int>();
        private List<int> PredmetiIds = new List<int>();
        private int selectedId = -1;
        public PrijavaIspita()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int StudentId = this.StudentsIds[this.selectedId];
                if (StudentId == -1)
                    return;

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\tmp\\NovaAkademija.xls;Extended Properties=\"Excel 8.0;ReadOnly=False;HDR=Yes;\"";
                
                for (int x = 0; x < this.lbPredmeti.Items.Count; x++)
                {
                    string s = "";
                    if (this.lbPredmeti.GetItemCheckState(x) == CheckState.Checked)
                    {
                        s = "INSERT INTO Ispiti (Student_id,Predmet_id)  VALUES (";
                        s += StudentId;
                        s += ",";
                        s += this.PredmetiIds[x];
                        s += ") ";
                        s += "; ";
                    }
                    else continue;

                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(s, conn);
                    // izvrsi sql upit
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
             
                MessageBox.Show("Запис о испиту успешно унет у табелу!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
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
            
            this.cbStudenti.Items.Clear();
            this.StudentsIds.Clear();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                this.StudentsIds.Add(id);
                this.cbStudenti.Items.Add(reader["Ime"].ToString()+" "+ reader["Prezime"].ToString());
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
            //int i = 0;
            this.lbPredmeti.Items.Clear();
            this.PredmetiIds.Clear();
            while (reader.Read())
            {
                this.lbPredmeti.Items.Add(reader["Naziv"].ToString());
                int id = Convert.ToInt32(reader["ID"]);
                this.PredmetiIds.Add(id);
            }
            conn.Close();
        }

        private void PrijavaIspita_Load(object sender, EventArgs e)
        {
            try
            {
                this.prikaziStudente();
                this.prikaziPredmete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Postoji problem: " + ex.Message);
            }
        }

        private void cbStudenti_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedId = cbStudenti.SelectedIndex;
            //MessageBox.Show("cbStudenti_SelectedIndexChanged " + this.selectedId);
        }
    }
}
