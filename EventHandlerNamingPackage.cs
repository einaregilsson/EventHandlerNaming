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
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using System.ComponentModel.Composition;
using System.Collections.Generic;

namespace EinarEgilsson.EventHandlerNaming
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0.1", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [Guid("2bdc9c28-e8c4-44cd-974b-19fded0abbe4")]
    public sealed class EventHandlerNamingPackage : Package
    {
        private CodeWindowHandler _codeWindowHandler;
        private DesignerCreationListener _designerHandler;

        protected override void Initialize()
        {
            base.Initialize();
            _designerHandler = new DesignerCreationListener((IDesignerEventService)GetService(typeof(IDesignerEventService)));
            _codeWindowHandler = new CodeWindowHandler((DTE2) GetGlobalService(typeof(EnvDTE.DTE)), new PatternNameProvider());
            InitializeMenuCommands();
        }

        private void InitializeMenuCommands()
        {
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID menuCommandID = new CommandID(new Guid("a1bea695-5c2a-4a47-a7c5-6430a32e702c"), 0x100);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);
            }
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            new OptionsForm().ShowDialog();
        }
    }
}
