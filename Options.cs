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
using System.IO;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;

namespace EinarEgilsson.EventHandlerNaming
{

    public enum Transform
    {
        NoChange = 0,
        Lowercase = 1,
        Uppercase = 2,
        CamelCase = 3,
        PascalCase = 4
    }

    public class Options
    {
        public Options()
        {
            Pattern = "On$(SiteName)$(EventName)";
            OmitSiteNameForOwnEvents = true;
            SiteNameTransform = Transform.PascalCase;
            UseDelegateInference = true;
        }

        public string Pattern { get; set; }
        public Transform SiteNameTransform { get; set; }
        public Transform EventNameTransform { get; set; }
        public Transform ClassNameTransform { get; set; }
        public bool OmitSiteNameForOwnEvents { get; set; }
        public bool UseDelegateInference { get; set; }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer (typeof (Options));
            using (var writer = new StreamWriter(_filename, false, Encoding.UTF8)){
                serializer.Serialize(writer, this);
            }
        }

        private static readonly string _filename = Path.ChangeExtension(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath, "xml");

        private static Options _instance;
        public static Options Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (File.Exists(_filename)){
                        try {

                            XmlSerializer serializer = new XmlSerializer ( typeof (Options ));
                        
                            using (var reader = new StreamReader(_filename, Encoding.UTF8, false)){
                                _instance = (Options)serializer.Deserialize(reader);
                            }
                        } catch (Exception){
                            _instance = new Options(); //Never fail.
                        }
                    }
                    else {
                        _instance = new Options();
                    }
                }
                return _instance;
            }
        }
    }
}
