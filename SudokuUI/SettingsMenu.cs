using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuUI
{
    public partial class SettingsMenu : Form
    {
        public SettingsMenu()
        {
            InitializeComponent();
            try
            {
                checkBox_ColorHelp.Checked = (bool)Properties.Settings.Default["ColorHelpEnabled"];
                Properties.Settings.Default.Save();
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
                Properties.Settings.Default["ColorHelpEnabled"] = true;
                checkBox_ColorHelp.Checked = true;
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_SaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["ColorHelpEnabled"] = checkBox_ColorHelp.Checked;
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings saved.");
            Close();
        }
    }
}
