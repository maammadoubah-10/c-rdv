using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppGroupe2.Model;

namespace AppGroupe2.View
{
    public partial class frmAgenda : Form
    {
        public int idMedecin;
        private BdRvMedicalContexe db = new BdRvMedicalContexe();

        public frmAgenda()
        {
            InitializeComponent();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            var m = db.Medecins.Find(idMedecin);
            lblMedecin.Text = $"N ordre: {m.NumeroOrdre}, Nom prenom: {m.NomPrenom}";
            lblIdMedecin.Text = m.IDU.ToString();
            lblIdMedecin.Visible = false;

            // Remplir le ComboBox des créneaux (10 min d'intervalle entre 8h00 et 11h00)
            //cbCrenau.Items.Clear();
            //for (int i = 8 * 60; i <= 11 * 60; i += 10)
            //{
            //    cbCrenau.Items.Add(TimeSpan.FromMinutes(i).ToString(@"hh\:mm")); // Format correct pour TimeSpan


            //}
            //cbCrenau.SelectedIndex = 0;

            ResetForm();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {

            //string timeString = cbCreneau.ToString();
            //timeString = timeString.Replace("h", ":").Trim(); // Remplace "h" par ":" pour correspondre au format TimeSpan
            //TimeSpan heureDebut = TimeSpan.Parse(timeString);
            Agenda a = new Agenda();

            a.Creaneau = cbCreneau.Text;
            a.HeureDebut = txtHeureDebut.Text;
            a.HeureFin = txtHeureFin.Text;
            a.IdMedecin = idMedecin;
            a.DatePlanifier = txtDateAgenda.Value;
            a.Statut = "brouillon";
            a.lieu = txtLieu.Text;
            

            db.Agenda.Add(a);
            db.SaveChanges();
            ResetForm();
        }

        private void ResetForm()
        {
            dgAgenda.DataSource = db.Agenda.Where(a => a.DatePlanifier >= DateTime.Now && a.IdMedecin == idMedecin).ToList();
        }
    }
}
