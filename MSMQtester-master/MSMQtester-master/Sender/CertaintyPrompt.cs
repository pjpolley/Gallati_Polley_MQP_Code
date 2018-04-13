using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    public partial class CertaintyPrompt : Form
    {
        public bool Continue;
        public CertaintyPrompt()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            Continue = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Continue = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
