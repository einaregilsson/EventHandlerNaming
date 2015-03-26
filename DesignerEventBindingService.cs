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

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace EinarEgilsson.EventHandlerNaming
{
    internal class DesignerEventBindingService : IEventBindingService
    {
        private readonly IEventBindingService _realService;
        private readonly PatternNameProvider _nameProvider;

        public DesignerEventBindingService(IEventBindingService realService, PatternNameProvider nameProvider)
        {
            _realService = realService;
            _nameProvider = nameProvider;
        }

        public string CreateUniqueMethodName(IComponent component, EventDescriptor e)
        {
            return _nameProvider.CreateEventHandlerName(component.Site.Name, e.Name, component.Site.Container.Components[0].Site.Name, _realService.CreateUniqueMethodName(component, e));
        }

        public ICollection GetCompatibleMethods(EventDescriptor e)
        {
            return _realService.GetCompatibleMethods(e);
        }

        public EventDescriptor GetEvent(PropertyDescriptor property)
        {
            return _realService.GetEvent(property);
        }

        public PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events)
        {
            return _realService.GetEventProperties(events);
        }

        public PropertyDescriptor GetEventProperty(EventDescriptor e)
        {
            return _realService.GetEventProperty(e);
        }

        public bool ShowCode()
        {
            return _realService.ShowCode();
        }

        public bool ShowCode(int lineNumber)
        {
            return _realService.ShowCode(lineNumber);
        }

        public bool ShowCode(IComponent component, EventDescriptor e)
        {
            return _realService.ShowCode(component, e);
        }

    }
}
