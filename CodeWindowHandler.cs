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
using EnvDTE80;
using EnvDTE;
using System.Text.RegularExpressions;

namespace EinarEgilsson.EventHandlerNaming
{
    internal class CodeWindowHandler
    {
        private readonly TextDocumentKeyPressEvents _textDocKeyPressEvents;
        private readonly DTE2 _application;
        private readonly PatternNameProvider _nameProvider;
        private readonly Options _options;
        
        internal CodeWindowHandler(DTE2 application, PatternNameProvider nameProvider, Options options)
        {
            _application = application;
            _textDocKeyPressEvents = ((Events2)application.Events).TextDocumentKeyPressEvents[null];
            _textDocKeyPressEvents.AfterKeyPress += AfterTextDocumentKeyPress;
            _textDocKeyPressEvents.BeforeKeyPress += BeforeTextDocumentKeyPress;
            _nameProvider = nameProvider;
            _options = options;
        }

        //Variables that hold state between beforekeypress and afterkeypress
        private bool _isRenameCandidate;
        private bool _isExpandingCandidate;
        private string _eventName;
        private EditPoint _delegateStart;
        private bool _openedUndoContext;

        void BeforeTextDocumentKeyPress(string keypress, TextSelection selection, bool inStatementCompletion, ref bool cancelKeypress)
        {
            if (keypress != "\t" || _application.ActiveDocument.Language != "CSharp")
            {
                return;
            }
            if (HandleCreateDelegateTemplate(selection))
            {
                return;
            }

            HandleExpandDelegateTemplate(selection);

        }

        private void StartUndoOperation()
        {
            if (!_application.UndoContext.IsOpen)
            {
                _application.UndoContext.Open("EventHandlerNaming");
                _openedUndoContext = true;
            }
        }

        private bool HandleCreateDelegateTemplate(TextSelection selection)
        {
            var editPoint = selection.TopPoint.CreateEditPoint();
            editPoint.StartOfLine();
            string previousLine = editPoint.GetText(selection.TopPoint);

            Match matchCreatingEventHandler = Regex.Match(previousLine, @"\.(\w+)\s*\+=\s*$");
            _isRenameCandidate = matchCreatingEventHandler.Success;
            if (_isRenameCandidate)
            {
                _eventName = matchCreatingEventHandler.Groups[1].Value;
                StartUndoOperation();
                return true;
            }
            return false;
        }

        private void HandleExpandDelegateTemplate(TextSelection selection)
        {
            if (!_options.UseDelegateInference)
            {
                return;
            }
            var editPoint = selection.TopPoint.CreateEditPoint();
            editPoint.StartOfLine();
            string previousLine = editPoint.GetText(selection.TopPoint);

            Match matchExpandingEventHandler = Regex.Match(previousLine, @"\.\w+\s*\+=\s*new\s+[a-zA-Z0-9_\.]+\s*\((\w*)$");
            _isExpandingCandidate = matchExpandingEventHandler.Success;
            if (_isExpandingCandidate)
            {
                StartUndoOperation();
                _delegateStart = selection.TopPoint.CreateEditPoint();
                _delegateStart.CharLeft(matchExpandingEventHandler.Groups[1].Value.Length); //We are now right after the (
            }
        }

        private void AfterTextDocumentKeyPress(string keypress, TextSelection selection, bool inStatementCompletion)
        {
            if (!_isRenameCandidate && !_isExpandingCandidate)
            {
                return;
            }

            try
            {
                if (_isRenameCandidate)
                {
                    DoRename(selection);
                }
                if (_isExpandingCandidate)
                {
                    DoExpand(selection);
                }
                if (_openedUndoContext)
                {
                    _application.UndoContext.Close();
                }

            }
            finally
            {
                _isRenameCandidate = false;
                _isExpandingCandidate = false;
                _openedUndoContext = false;
            }
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
            var classElement = selection.ActivePoint.CodeElement[vsCMElement.vsCMElementClass];
            string name = _nameProvider.CreateEventHandlerName(siteName, eventName, classElement.Name, selection.Text);

            selection.TopPoint.CreateEditPoint().ReplaceText(selection.BottomPoint, name, 0);
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
                delPoint.GetText(-1);
                delPoint.WordLeft(3);
                
                delPoint.Delete(_delegateStart);
                _delegateStart.WordRight();
                _delegateStart.Delete(1);
            }
            _delegateStart = null;
        }

    }
}
