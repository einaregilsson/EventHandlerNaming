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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EinarEgilsson.EventHandlerNaming
{
    internal class PatternNameProvider : IEventHandlerNameProvider
    {
        public string CreateEventHandlerName(string siteName, string eventName, string containingClassName, string suggestedName)
        {
            string name = Options.Instance.Pattern;
            siteName = TransformString(siteName, Options.Instance.SiteNameTransform);
            eventName = TransformString(eventName, Options.Instance.EventNameTransform);

            if (Options.Instance.OmitSiteNameForOwnEvents && siteName == containingClassName)
            {
                name = Regex.Replace(name, @"\$\(sitename\)", "", RegexOptions.IgnoreCase);
            }

            name = Regex.Replace(name, @"\$\(sitename\)", siteName, RegexOptions.IgnoreCase);
            name = Regex.Replace(name, @"\$\(eventname\)", eventName, RegexOptions.IgnoreCase);
            name = Regex.Replace(name, @"\$\(classname\)", containingClassName?? "", RegexOptions.IgnoreCase);
            return name;
        }

        private string TransformString(string s, Transform transform)
        {
            s = Regex.Replace(s, "^_*", ""); //Cut off leading underscores
            switch (transform)
            {
                case Transform.NoChange:
                    return s;
                case Transform.Lowercase:
                    return s.ToLower();
                case Transform.Uppercase:
                    return s.ToUpper();
                case Transform.CamelCase:
                    return Char.ToLower(s[0]) + s.Substring(1);
                case Transform.PascalCase:
                    return Char.ToUpper(s[0]) + s.Substring(1);
                default:
                    throw new ArgumentException("Unrecognized Transform: " + transform);
            }
        }
    }
}
