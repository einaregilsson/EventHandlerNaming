#region License
/* 
EventHandler Naming Visual Studio Extension
Copyright (C) 2010 Einar Egilsson
http://tech.einaregilsson.com/2010/12/22/better-eventhandler-names-in-visual-studio-2010/

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

$Id$ 
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace EinarEgilsson.EventHandlerNaming
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            cboSiteNameTransform.DataSource = Enum.GetValues(typeof(Transform));
            cboEventNameTransform.DataSource = Enum.GetValues(typeof(Transform));
            txtPattern.Text = Options.Instance.Pattern;
            chkOmitSiteNameForOwnEvents.Checked = Options.Instance.OmitSiteNameForOwnEvents;
            cboEventNameTransform.SelectedItem = Options.Instance.EventNameTransform;
            cboSiteNameTransform.SelectedItem = Options.Instance.SiteNameTransform;
            chkUseDelegateInference.Checked = Options.Instance.UseDelegateInference;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Options.Instance.Pattern = txtPattern.Text;
            Options.Instance.OmitSiteNameForOwnEvents = chkOmitSiteNameForOwnEvents.Checked;
            Options.Instance.EventNameTransform = (Transform)cboEventNameTransform.SelectedItem;
            Options.Instance.SiteNameTransform= (Transform)cboSiteNameTransform.SelectedItem;
            Options.Instance.UseDelegateInference = chkUseDelegateInference.Checked;
            Options.Instance.Save();
            Close();
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://tech.einaregilsson.com/2010/12/22/better-eventhandler-names-in-visual-studio-2010/");
        }

    }
}
