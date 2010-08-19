using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace bsn.CommandLine.Context {
	public abstract class ContextItem<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private static readonly Dictionary<Type, NamedItemAttribute> nameAttributes = new Dictionary<Type, NamedItemAttribute>();

		protected static bool TryGetNameAttribute(Type type, out NamedItemAttribute nameAttribute) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			lock (nameAttributes) {
				if (!nameAttributes.TryGetValue(type, out nameAttribute)) {
					foreach (NamedItemAttribute attribute in type.GetCustomAttributes(typeof(NamedItemAttribute), true)) {
						nameAttribute = attribute;
						break;
					}
					nameAttributes.Add(type, nameAttribute);
				}
				return nameAttribute != null;
			}
		}

		public static IEnumerable<TItem> Filter<TItem>(IEnumerable<TItem> items, string startsWith) where TItem: INamedItem {
			foreach (TItem item in items) {
				if (string.IsNullOrEmpty(startsWith) || item.Name.StartsWith(startsWith, StringComparison.OrdinalIgnoreCase)) {
					yield return item;
				}
			}
		}

		public virtual void WriteItemHelp(TextWriter writer) {
			WriteNameLine(writer, null);
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

		public virtual string Name {
			get {
				NamedItemAttribute attribute;
				if (!TryGetNameAttribute(GetType(), out attribute)) {
					Debug.Fail("No name attribute on "+GetType().FullName);
					return GetType().Name;
				}
				return attribute.Name;
			}
		}

		public virtual string Description {
			get {
				NamedItemAttribute attribute;
				if (!TryGetNameAttribute(GetType(), out attribute)) {
					return string.Empty;
				}
				return attribute.Description;
			}
		}
	}
}