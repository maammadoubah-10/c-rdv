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
using AppGroupe2.Report;
namespace AppGroupe2.View
{

    public partial class frmPrintTicket : Form
    {
        private int _idRv;

        // Constructor qui accepte l'ID du rendez-vous
        public frmPrintTicket(int idRv)
        {
            InitializeComponent();
            _idRv = idRv;
        }
        BdRvMedicalContexe bd = new BdRvMedicalContexe();

        private void frmPrintTicket_Load(object sender, EventArgs e)
        {
            rptTicketRV objRpt = new rptTicketRV();
            objRpt.SetDataSource(GetTableTicket(_idRv));
            crystalReportViewer1.ReportSource = objRpt;
            crystalReportViewer1.Refresh();
        }

        public DataTable GetTableTicket(int idRv)
        {
            DataTable table = new DataTable();
            table.Columns.Add("NomPrenom", typeof(string));
            table.Columns.Add("DateNaissance", typeof(DateTime));
            table.Columns.Add("DateRv", typeof(DateTime));
            table.Columns.Add("Medecin", typeof(string));
            table.Columns.Add("HeureRv", typeof(string));
            table.Columns.Add("DataQr", typeof(byte[])); // Changement de type en byte[]

            var leRv = bd.RendezVous.Where(a => a.IdRv == idRv).FirstOrDefault();
            if (leRv != null)
            {
                // Ajouter les données du rendez-vous dans le DataTable
                table.Rows.Add(leRv.patient.NomPrenom, leRv.patient.DateNaissance, leRv.DateRv, leRv.Medecin.NomPrenom, leRv.DateRv.ToString("HH:mm")); // Assurez-vous d'avoir une propriété QrCode ou une méthode pour générer le QR
            }
            else
            {
                // Si aucun rendez-vous n'est trouvé, ajouter des valeurs par défaut
                table.Rows.Add("NomPrenom", DateTime.Now, DateTime.Now, "NomPrenom", DateTime.Now.ToString("HH:mm"), new byte[0]);
            }
            return table;
        }

    }
}
