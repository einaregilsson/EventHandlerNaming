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
using System.ComponentModel.Design;

namespace EinarEgilsson.EventHandlerNaming
{
    internal class DesignerCreationListener
    {
        internal DesignerCreationListener(IDesignerEventService designerEvents, Options options) {
            designerEvents.DesignerCreated += (s, e) => e.Designer.LoadComplete += OnDesignerLoaded;
            _options = options;
        }

        private Options _options;
        private void OnDesignerLoaded(object sender, EventArgs e)
        {
            var host = sender as IDesignerHost;
            if (host == null)
            {
                return;
            }
            host.LoadComplete -= OnDesignerLoaded;

            Type type = typeof(IEventBindingService);
            var originalService = (IEventBindingService) host.GetService(type);
            if (originalService == null)
            {
                return;
            }

            host.RemoveService(type);
            host.AddService(type, new DesignerEventBindingService(originalService, new PatternNameProvider(_options)));
        }
    }
}
