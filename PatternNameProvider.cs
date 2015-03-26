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
using System.Text.RegularExpressions;

namespace EinarEgilsson.EventHandlerNaming
{
    internal class PatternNameProvider
    {
        private readonly Options _options;
        internal PatternNameProvider(Options options)
        {
            _options = options;
        }

        public string CreateEventHandlerName(string siteName, string eventName, string containingClassName, string suggestedName)
        {
            string name = _options.Pattern;
            if (!string.IsNullOrEmpty(_options.RemovePrefixes))
            {
                string removePrefix = "^(" + string.Join("|", _options.RemovePrefixes.Split(';')) + ")";
                siteName = Regex.Replace(siteName, removePrefix, "");
            }

            siteName = _options.SiteNameTransform.Execute(siteName);
            eventName = _options.EventNameTransform.Execute(eventName);

            if (_options.OmitSiteNameForOwnEvents && siteName == containingClassName)
            {
                name = Regex.Replace(name, @"\$\(sitename\)", "", RegexOptions.IgnoreCase);
            }

            name = Regex.Replace(name, @"\$\(sitename\)", siteName, RegexOptions.IgnoreCase);
            name = Regex.Replace(name, @"\$\(eventname\)", eventName, RegexOptions.IgnoreCase);
            name = Regex.Replace(name, @"\$\(classname\)", containingClassName?? "", RegexOptions.IgnoreCase);
            return name; 
        }
    }
}
