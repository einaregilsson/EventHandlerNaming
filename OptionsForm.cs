#region License
/* 
EventHandler Naming Visual Studio Extension
Copyright (C) 2010 Einar Egilsson
http://einaregilsson.com/better-eventhandler-names-in-visual-studio-2010/

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
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace EinarEgilsson.EventHandlerNaming
{
    public partial class OptionsForm : Form
    {
        public OptionsForm(Options options)
        {
            InitializeComponent();
            _options = options;
            cboSiteNameTransform.DataSource = Transform.Values.ToList();
            cboEventNameTransform.DataSource = Transform.Values.ToList();
            txtPattern.Text = _options.Pattern;
            chkOmitSiteNameForOwnEvents.Checked = _options.OmitSiteNameForOwnEvents;
            cboEventNameTransform.SelectedItem = _options.EventNameTransform;
            cboSiteNameTransform.SelectedItem = _options.SiteNameTransform;
            chkUseDelegateInference.Checked = _options.UseDelegateInference;
            txtRemovePrefixes.Text = _options.RemovePrefixes;
        }

        private readonly Options _options;

        private void OnOKClick(object sender, EventArgs e)
        {
            _options.Pattern = txtPattern.Text;
            _options.OmitSiteNameForOwnEvents = chkOmitSiteNameForOwnEvents.Checked;
            _options.EventNameTransform = (Transform)cboEventNameTransform.SelectedItem;
            _options.SiteNameTransform = (Transform)cboSiteNameTransform.SelectedItem;
            _options.UseDelegateInference = chkUseDelegateInference.Checked;
            _options.RemovePrefixes = txtRemovePrefixes.Text;
            _options.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://einaregilsson.com/better-eventhandler-names-in-visual-studio-2010/");
        }

    }
}
