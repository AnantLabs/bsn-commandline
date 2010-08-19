using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace bsn.CommandLine.Context {
	public abstract class CommandBase<TExecutionContext>: IContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public static IEnumerable<TItem> Filter<TItem>(IEnumerable<TItem> items, string startsWith) where TItem: INamedItem {
			foreach (TItem item in items) {
				if (string.IsNullOrEmpty(startsWith) || item.Name.StartsWith(startsWith, StringComparison.OrdinalIgnoreCase)) {
					yield return item;
				}
			}
		}

		private readonly ContextBase<TExecutionContext> parentContext;

		protected CommandBase(ContextBase<TExecutionContext> parentContext) {
			this.parentContext = parentContext;
		}

		public ContextBase<TExecutionContext> ParentContext {
			get {
				return parentContext;
			}
		}

		public abstract void Execute(TExecutionContext executionContext, IDictionary<string, object> tags);

		public virtual IEnumerable<ITagItem> GetCommandTags() {
			yield break;
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

		internal void ExecuteInternal(TExecutionContext executionContext, IDictionary<string, string> namedTags, IList<string> unnamedTags) {
			Dictionary<string, object> tags = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			foreach (ITagItem tag in GetCommandTags()) {
				string stringValue;
				if (namedTags.TryGetValue(tag.Name, out stringValue)) {
					namedTags.Remove(tag.Name);
				} else if (unnamedTags.Count > 0) {
					stringValue = unnamedTags[0];
					unnamedTags.RemoveAt(0);
				}
				if (!(tag.Optional && string.IsNullOrEmpty(stringValue))) {
					tags.Add(tag.Name, tag.ParseValue(stringValue));
				}
			}
			if ((namedTags.Count+unnamedTags.Count) > 0) {
				StringBuilder message = new StringBuilder("Excess tags received:");
				foreach (string unnamedTag in unnamedTags) {
					message.Append(" \"");
					message.Append(unnamedTag.Replace("\"", "\"\""));
					message.Append("\"");
				}
				foreach (KeyValuePair<string, string> namedTag in namedTags) {
					message.Append(' ');
					message.Append(namedTag.Key);
					message.Append("=\"");
					message.Append(namedTag.Value.Replace("\"", "\"\""));
					message.Append("\"");
				}
				throw new InvalidOperationException(message.ToString());
			}
			Execute(executionContext, tags);
		}

		public abstract string Name {
			get;
		}

		public abstract string Description {
			get;
		}

		public virtual void WriteCommandHelp(TextWriter writer) {
			writer.WriteLine(Description);
			using (IEnumerator<CommandBase<TExecutionContext>> enumerator = GetAvailableCommands().GetEnumerator()) {
				if (enumerator.MoveNext()) {
					writer.WriteLine();
					writer.WriteLine("Available commands:");
					do {
						CommandBase<TExecutionContext> current = enumerator.Current;
						Debug.Assert(current != null);
						current.WriteNameLine(writer, Name);
					} while (enumerator.MoveNext());
				}
			}
		}

		public virtual IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands() {
			yield return new CommandHelpCommand<TExecutionContext>(this, "?");
			yield return new CommandHelpCommand<TExecutionContext>(this, "help");
		}
	}
}