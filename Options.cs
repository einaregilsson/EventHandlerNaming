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
using Microsoft.Win32;

namespace EinarEgilsson.EventHandlerNaming
{
    public class Options
    {
        private const string DefaultPrefixes = "txt;lbl;btn;cbo;grp;chk;prg;rdo;grd;lst;edt";
        
        public Options(RegistryKey registryRoot, string extensionName)
        {
            Pattern = "On$(SiteName)$(EventName)";
            OmitSiteNameForOwnEvents = true;
            SiteNameTransform = Transform.PascalCase;
            EventNameTransform = Transform.NoChange;
            UseDelegateInference = true;
            RemovePrefixes = DefaultPrefixes;
            _registryRoot = registryRoot;
            _extensionName = extensionName;
        }

        private readonly RegistryKey _registryRoot;
        private readonly string _extensionName;

        public string Pattern { get; set; }
        public Transform SiteNameTransform { get; set; }
        public Transform EventNameTransform { get; set; }
        public bool OmitSiteNameForOwnEvents { get; set; }
        public bool UseDelegateInference { get; set; }
        public string RemovePrefixes { get; set; }
        
        public void Load()
        {
            RegistryKey extensionKey = _registryRoot.OpenSubKey(_extensionName);
            if (extensionKey == null)
            {
                return;
            }
            Pattern = (string) extensionKey.GetValue("Pattern", Pattern);
            SiteNameTransform = (int)extensionKey.GetValue("SiteNameTransform", SiteNameTransform);
            EventNameTransform = (int)extensionKey.GetValue("EventNameTransform", EventNameTransform);
            OmitSiteNameForOwnEvents = ((int) extensionKey.GetValue("OmitSiteNameForOwnEvents", OmitSiteNameForOwnEvents)) == 1 ? true : false;
            UseDelegateInference = ((int)extensionKey.GetValue("UseDelegateInference", UseDelegateInference)) == 1 ? true : false;
            RemovePrefixes = (string) extensionKey.GetValue("RemovePrefixes", RemovePrefixes);
        }

        public void Save()
        {
            RegistryKey extensionKey = _registryRoot.OpenSubKey(_extensionName, true) ??
                                       _registryRoot.CreateSubKey(_extensionName);
            if (extensionKey == null)
            {
                throw new Exception("Could not create registry key for options!");
            }

            extensionKey.SetValue("Pattern", Pattern, RegistryValueKind.String);
            extensionKey.SetValue("SiteNameTransform", SiteNameTransform.Value, RegistryValueKind.DWord);
            extensionKey.SetValue("EventNameTransform", EventNameTransform.Value, RegistryValueKind.DWord);
            extensionKey.SetValue("OmitSiteNameForOwnEvents", OmitSiteNameForOwnEvents ? 1:0, RegistryValueKind.DWord);
            extensionKey.SetValue("UseDelegateInference", UseDelegateInference ? 1 : 0, RegistryValueKind.DWord);
            extensionKey.SetValue("RemovePrefixes", RemovePrefixes, RegistryValueKind.String);
        }
    }
}
