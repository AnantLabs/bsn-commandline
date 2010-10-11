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

		public virtual void WriteItemHelp(TextWriter writer, TExecutionContext executionContext) {
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

		protected IEnumerable<T> Merge<T>(IEnumerable<T> existingItems, IEnumerable<T> newItems) {
			if (existingItems != null) {
				foreach (T item in existingItems) {
					yield return item;
				}
			}
			if (newItems != null) {
				foreach (T item in newItems) {
					yield return item;
				}
			}
		}

		protected IEnumerable<T> Merge<T>(IEnumerable<T> existingItems, params T[] newItems) {
			return Merge(existingItems, (IEnumerable<T>)newItems);
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
