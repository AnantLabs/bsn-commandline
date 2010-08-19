using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace bsn.CommandLine.Context {
	public abstract class CommandBase: IContextItem {
		public static IEnumerable<T> Filter<T>(IEnumerable<T> items, string startsWith) where T: IContextItem {
			foreach (T item in items) {
				if (string.IsNullOrEmpty(startsWith) || item.Name.StartsWith(startsWith, StringComparison.OrdinalIgnoreCase)) {
					yield return item;
				}
			}
		}

		private readonly ContextBase parentContext;

		protected CommandBase(ContextBase parentContext) {
			this.parentContext = parentContext;
		}

		public ContextBase ParentContext {
			get {
				return parentContext;
			}
		}

		public virtual void Execute(IExecutionContext executionContext) {
			WriteCommandHelp(executionContext.Output);
		}

		protected internal void WriteNameLine(TextWriter writer, string prefix) {
			int padding = 14;
			if (!string.IsNullOrEmpty(prefix)) {
				writer.Write(prefix);
				writer.Write(' ');
				padding -= (prefix.Length+1);
			}
			writer.Write(Name);
			padding -= Name.Length;
			while (padding-- > 0) {
				writer.Write(' ');
			}
			writer.Write(" - ");
			writer.WriteLine(Description);
		}

		public abstract string Name {
			get;
		}

		public abstract string Description {
			get;
		}

		public virtual void WriteCommandHelp(TextWriter writer) {
			writer.WriteLine(Description);
			using (IEnumerator<CommandBase> enumerator = AvailableCommands().GetEnumerator()) {
				if (enumerator.MoveNext()) {
					writer.WriteLine();
					writer.WriteLine("Available commands:");
					do {
						CommandBase current = enumerator.Current;
						Debug.Assert(current != null);
						current.WriteNameLine(writer, Name);
					} while (enumerator.MoveNext());
				}
			}
		}

		public virtual IEnumerable<CommandBase> AvailableCommands() {
			yield return new CommandHelpCommand(this, "?");
			yield return new CommandHelpCommand(this, "help");
		}
	}
}
