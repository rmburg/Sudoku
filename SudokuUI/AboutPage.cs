using System.Windows.Forms;

namespace SudokuUI
{
    public partial class AboutPage : Form
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Dieser Link führt eigentlich zu meinem GitHub-repo, in dieser Version des Programms wurde der Link aber bewusst entfernt.");
        }
    }
}
