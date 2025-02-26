using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppGroupe2.Model;
namespace AppGroupe2.View
{
    public partial class frmPatient : Form
    {
        BdRvMedicalContexe db = new BdRvMedicalContexe();
        public frmPatient()
        {
            InitializeComponent();
        }
        

        public object CurrentRow { get; private set; }

        private void ResetForm()
        {
            txtNomPrenom.Text=string.Empty;
            txtAdresse.Text = string.Empty;
            txtEmail.Text=string.Empty;
            txtGroupeSanguin.Text = string.Empty;
            txtPoid.Text = string.Empty;
            txtTaille.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            dgPatient.DataSource = db.Patients.ToList();
            txtNomPrenom.Focus();

        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.NomPrenom = txtNomPrenom.Text;
            p.Adresse = txtAdresse.Text;
            p.Tel=txtTelephone.Text;
            p.Email=txtEmail.Text;
            p.Poids=float.Parse(txtTaille.Text);
            p.Taille = float.Parse(txtTaille.Text);
            p.GroupSanguin=txtGroupeSanguin.Text;
            p.DateNaissance = DateTime.Parse(txtDateNaissance.Text);
            db.Patients.Add(p);
            db.SaveChanges();
            ResetForm();


        }

        private void frmPatient_Load(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnChoisir_Click(object sender, EventArgs e)
        {
            txtNomPrenom.Text = dgPatient.CurrentRow.Cells[5].Value.ToString();
            txtAdresse.Text= dgPatient.CurrentRow.Cells[6].Value.ToString();
            txtEmail.Text=dgPatient.CurrentRow.Cells [7].Value.ToString();
            txtTelephone.Text = dgPatient.CurrentRow.Cells[8].Value.ToString();
            txtGroupeSanguin.Text = dgPatient.CurrentRow.Cells[0].Value.ToString() ;
            txtPoid.Text = dgPatient.CurrentRow.Cells[1].Value.ToString();
            txtTaille.Text = dgPatient.CurrentRow.Cells[2].Value.ToString();
            txtDateNaissance.Value = !string.IsNullOrEmpty(dgPatient.CurrentRow.Cells[3].Value?.ToString())
    ? Convert.ToDateTime(dgPatient.CurrentRow.Cells[3].Value)
    : DateTime.Now;




        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgPatient.CurrentRow.Cells[3].Value.ToString());
            if(id.HasValue)
            {
                var p = db.Patients.Find(id);
                p.NomPrenom = txtNomPrenom.Text;
                p.Adresse = txtAdresse.Text;
                p.Tel = txtTelephone.Text;
                p.Email = txtEmail.Text;
                p.Poids = float.Parse(txtTaille.Text);
                p.GroupSanguin = txtGroupeSanguin.Text;
                db.SaveChanges();
                ResetForm();

            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgPatient.CurrentRow.Cells[3].Value.ToString());
            if (id.HasValue)
            {
                var p = db.Patients.Find(id);
                db.Patients.Remove(p);
                db.SaveChanges();
                ResetForm();

            }
        }
    }
    
}
