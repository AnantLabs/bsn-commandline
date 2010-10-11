// bsn CommandLine Library
// -----------------------
// 
// Copyright 2010 by Arsène von Wyss - avw@gmx.ch
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
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	[NamedItem("?", "Displays a list of the available commands.")]
	internal class ContextHelpCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly string name;

		public ContextHelpCommand(ContextBase<TExecutionContext> owner, string name): base(owner) {
			this.name = name;
		}

		public override string Name {
			get {
				return name;
			}
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			object commandName;
			if (tags.TryGetValue("command", out commandName)) {
				bool commandFound = false;
				foreach (CommandBase<TExecutionContext> command in Filter(ParentContext.GetAvailable<CommandBase<TExecutionContext>>(), (string)commandName)) {
					command.WriteItemHelp(executionContext.Output, executionContext);
					commandFound = true;
				}
				if (!commandFound) {
					executionContext.Output.WriteLine("Unknown command: {0}", commandName);
				}
			} else {
				ParentContext.WriteItemHelp(executionContext.Output, executionContext);
			}
		}

		public override IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands() {
			yield break;
		}

		public override IEnumerable<ITagItem<TExecutionContext>> GetCommandTags() {
			yield return new Tag<TExecutionContext, string>("command", "The command name to get help for.").SetOptional(context => true);
		}
	}
}
