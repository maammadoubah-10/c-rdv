using AppGroupe2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppGroupe2.View;

namespace AppGroupe2.View
{

    public partial class frmRendezVous : Form
    {

       
        public frmRendezVous()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmRendezVous_Load);
            this.btnImprimer = new System.Windows.Forms.Button();
            this.btnImprimer.Location = new System.Drawing.Point(150, 450); // Ajuster la position
            this.btnImprimer.Name = "btnImprimer";
            this.btnImprimer.Size = new System.Drawing.Size(100, 30);
            this.btnImprimer.Text = "Imprimer";
            this.btnImprimer.UseVisualStyleBackColor = true;
            this.btnImprimer.Click += new System.EventHandler(this.btnImprimer_Click);
            this.Controls.Add(this.btnImprimer);
        }
        BdRvMedicalContexe db = new BdRvMedicalContexe();

        private void frmRendezVous_Load(object sender, EventArgs e)
        {
            // Charger les patients dans le ComboBox
            cbPatient.DataSource = db.Patients.ToList();
            cbPatient.DisplayMember = "NomPrenom"; // Afficher le nom du patient
            cbPatient.ValueMember = "IDU"; // Utiliser l'ID du patient comme valeur

            // Charger les médecins dans le ComboBox
            cbMedecin.DataSource = db.Medecins.ToList();
            cbMedecin.DisplayMember = "NomPrenom"; // Afficher le nom du médecin
            cbMedecin.ValueMember = "IDU"; // Utiliser l'ID du médecin comme valeur

            // Charger les soins dans le ComboBox
            cbSoin.DataSource = db.Soins.ToList();
            cbSoin.DisplayMember = "libelle"; // Afficher le nom du soin
            cbSoin.ValueMember = "IdSoin"; // Utiliser l'ID du soin comme valeur

            // Charger la liste des rendez-vous dans le DataGridView
            var rendezVousList = db.RendezVous
              .Select(rv => new
                  {
                     rv.IdRv,
                     rv.DateRv,
                     rv.Statut,
                     Patient = rv.patient.NomPrenom,  // Remplace l'ID du patient par son nom
                     Medecin = rv.Medecin.NomPrenom,  // Remplace l'ID du médecin par son nom
                     Soin = rv.Soin.libelle         // Remplace l'ID du soin par son libellé
                 })
                     .ToList();

                      dgRendezVous.DataSource = rendezVousList;

        }


        private void btnAjouter_Click(object sender, EventArgs e)
        {
            RendezVous rendezVous = new RendezVous
            {

                DateRv = dtpDateRv.Value,
                Statut = txtStatut.Text,
                IdPatient = (int)cbPatient.SelectedValue,
                IdMedecin = (int)cbMedecin.SelectedValue,
                IdSoin = (int)cbSoin.SelectedValue
            };

            // Ajouter le rendez-vous à la base de données
            db.RendezVous.Add(rendezVous);
            db.SaveChanges();

            // Recharger la liste des rendez-vous
            dgRendezVous.DataSource = db.RendezVous.ToList();
        }

        private void frmRendezVous_Load_1(object sender, EventArgs e)
        {

        }


        private void btnImprimer_Click(object sender, EventArgs e)
        {
            if (dgRendezVous.SelectedRows.Count > 0)
            {
                // Récupérer l'ID du rendez-vous sélectionné
                int idRv = Convert.ToInt32(dgRendezVous.SelectedRows[0].Cells["IdRv"].Value);

                // Ouvrir le formulaire de ticket d'impression
                frmPrintTicket frmTicket = new frmPrintTicket(idRv);
                frmTicket.ShowDialog(); // Utilisation de ShowDialog pour que le formulaire de ticket soit modal (bloque l'accès à d'autres fenêtres tant qu'il est ouvert)
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un rendez-vous pour imprimer le ticket.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
