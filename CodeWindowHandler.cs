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
using EnvDTE80;
using EnvDTE;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EinarEgilsson.EventHandlerNaming
{
    internal class CodeWindowHandler
    {
        private readonly TextDocumentKeyPressEvents _textDocKeyPressEvents;
        private readonly DTE2 _application;
        private readonly IEventHandlerNameProvider _nameProvider;

        internal CodeWindowHandler(DTE2 application, IEventHandlerNameProvider nameProvider)
        {
            _application = application;
            _textDocKeyPressEvents = ((Events2)application.Events).get_TextDocumentKeyPressEvents(null);
            _textDocKeyPressEvents.AfterKeyPress += OnAfterTextDocumentKeyPress;
            _textDocKeyPressEvents.BeforeKeyPress += new _dispTextDocumentKeyPressEvents_BeforeKeyPressEventHandler(OnBeforeTextDocumentKeyPress);
            _nameProvider = nameProvider;
        }

        //temp variables
        private bool _isRenameCandidate;
        private bool _isExpandingCandidate;
        private string _eventName;
        private EditPoint _delegateStart;

        private bool KeyPressIsNotCandidate(string keypress)
        {
            return keypress != "\t" || _application.ActiveDocument.Language != "CSharp";
        }

        void OnBeforeTextDocumentKeyPress(string keypress, TextSelection selection, bool inStatementCompletion, ref bool cancelKeypress)
        {
            if (KeyPressIsNotCandidate(keypress))
            {
                return;
            }
            if (HandleCreateDeletgateTemplate(selection))
            {
                return;
            }

            HandleExpandDeletgateTemplate(selection);

        }

        private bool HandleCreateDeletgateTemplate(TextSelection selection)
        {
            var editPoint = selection.TopPoint.CreateEditPoint();
            editPoint.StartOfLine();
            string previousLine = editPoint.GetText(selection.TopPoint);

            Match creatingEventHandler = Regex.Match(previousLine, @"\.(\w+)\s*\+=\s*$");
            _isRenameCandidate = creatingEventHandler.Success;
            if (creatingEventHandler.Success)
            {
                _eventName = creatingEventHandler.Groups[1].Value;
                return true;
            }
            return false;
        }

        private void HandleExpandDeletgateTemplate(TextSelection selection)
        {
            if (!Options.Instance.UseDelegateInference)
            {
                return;
            }
            var editPoint = selection.TopPoint.CreateEditPoint();
            editPoint.StartOfLine();
            string previousLine = editPoint.GetText(selection.TopPoint);

            Match expandingEventHandler = Regex.Match(previousLine, @"\.\w+\s*\+=\s*new\s+[a-zA-Z0-9_\.]+\s*\(\w*$");
            _isExpandingCandidate = expandingEventHandler.Success;
            if (_isExpandingCandidate)
            {
                _delegateStart = selection.TopPoint.CreateEditPoint();
            }
        }

        private void OnAfterTextDocumentKeyPress(string keypress, TextSelection selection, bool inStatementCompletion)
        {
            if (!_isRenameCandidate && !_isExpandingCandidate)
            {
                return;
            }

            if (_isRenameCandidate)
            {
                DoRename(selection);
            }
            if (_isExpandingCandidate)
            {
                DoExpand(selection);
            }
            _isRenameCandidate = false;
            _isExpandingCandidate = false;
            
        }

        private void DoRename(TextSelection selection)
        {
            if (selection.Text.Length == 0 || !selection.Text.Contains("_" + _eventName))
            {
                return;
            }
            //Now look in more detail and figure out if this actually is what we're looking for
            string original = selection.Text;
            int lastPost = original.LastIndexOf('_');
            string siteName = original.Substring(0, lastPost);
            string eventName = original.Substring(lastPost + 1);
            var classElement = _application.ActiveDocument.ProjectItem.FileCodeModel.CodeElementFromPoint((TextPoint)selection.ActivePoint, vsCMElement.vsCMElementClass);
            string name = _nameProvider.CreateEventHandlerName(siteName, eventName, classElement.Name, selection.Text);

            bool wasOpen = _application.UndoContext.IsOpen;
            if (!wasOpen)
            {
                _application.UndoContext.Open("EventHandlerNaming.InsertEventHandler", false);
            }

            selection.TopPoint.CreateEditPoint().ReplaceText(selection.BottomPoint, name, 0);

            if (!wasOpen)
            {
                _application.UndoContext.Close();
            }
        }

        private void DoExpand(TextSelection selection)
        {
            if (_delegateStart == null)
            {
                return;
            }
            var point = selection.BottomPoint.CreateEditPoint();
            point.EndOfLine();
            var point2 = selection.BottomPoint.CreateEditPoint();
            string line = point2.GetText(point);
            if (line.Trim() == "throw new NotImplementedException();")
            {
                var delPoint = _delegateStart.CreateEditPoint();
                string prevChar = delPoint.GetText(-1);
                if (prevChar == "(")
                {
                    delPoint.WordLeft(2);
                }
                else
                {
                    delPoint.WordLeft(3);
                }
                
                delPoint.Delete(_delegateStart);
                _delegateStart.WordRight(1);
                _delegateStart.Delete(1);
            }
            _delegateStart = null;

        }

    }
}
