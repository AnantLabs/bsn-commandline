// bsn CommandLine Library
// -----------------------
// 
// Copyright 2014 by Arsène von Wyss - avw@gmx.ch
// 
// Development has been supported by Sirius Technologies AG, Basel
// 
// Source:
// 
// https://bsn-commandline.googlecode.com/hg/
// 
// License:
// 
// The library is distributed under the GNU Lesser General Public License:
// http://www.gnu.org/licenses/lgpl.html
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
using System;
using System.Collections;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public class ContextCommandList<TExecutionContext>: IEnumerable<CommandBase<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly ContextCommandList<TExecutionContext> child;
		private readonly SortedDictionary<string, CommandBase<TExecutionContext>> commands = new SortedDictionary<string, CommandBase<TExecutionContext>>(StringComparer.OrdinalIgnoreCase);
		private readonly ContextCommandList<TExecutionContext> parent;

		public ContextCommandList(ContextBase<TExecutionContext> context): this(context, null) {}

		private ContextCommandList(ContextBase<TExecutionContext> context, ContextCommandList<TExecutionContext> child) {
			if (context == null) {
				throw new ArgumentNullException("context");
			}
			this.child = child;
			foreach (CommandBase<TExecutionContext> command in context.GetAvailableCommands()) {
				if (!IsNameUsedByChild(command.Name)) {
					commands.Add(command.Name, command);
				}
			}
			if (context.ParentContext != null) {
				parent = new ContextCommandList<TExecutionContext>(context.ParentContext, this);
			}
		}

		public bool IsTop {
			get {
				return child == null;
			}
		}

		public ContextCommandList<TExecutionContext> Parent {
			get {
				return parent;
			}
		}

		private bool IsNameUsedByChild(string name) {
			return (child != null) && (child.commands.ContainsKey(name) || child.IsNameUsedByChild(name));
		}

		public IEnumerator<CommandBase<TExecutionContext>> GetEnumerator() {
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
