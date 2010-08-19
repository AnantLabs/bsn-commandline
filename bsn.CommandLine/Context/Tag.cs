using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace bsn.CommandLine.Context {
	public class Tag<TValue>: ITagItem {
		private readonly string description;
		private readonly string name;
		private readonly bool optional;
		private readonly string patternHelp;

		public Tag(string name, string description, bool optional): this(name, description, optional, null) {}

		public Tag(string name, string description, bool optional, string patternHelp) {
			if (string.IsNullOrEmpty(name)) {
				throw new ArgumentNullException("name");
			}
			if (description == null) {
				throw new ArgumentNullException("description");
			}
			if (string.IsNullOrEmpty(patternHelp)) {
				StringBuilder patternHelpBuilder = new StringBuilder();
				if (optional) {
					patternHelpBuilder.Append('[');
				}
				patternHelpBuilder.Append('<');
				patternHelpBuilder.Append(typeof(TValue).Name.ToLowerInvariant());
				patternHelpBuilder.Append('>');
				if (optional) {
					patternHelpBuilder.Append(']');
				}
				this.patternHelp = patternHelpBuilder.ToString();
			} else {
				this.patternHelp = patternHelp;
			}
			this.name = name;
			this.description = description;
			this.optional = optional;
		}

		public virtual TValue ParseValue(string stringValue) {
			TValue value;
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
			if (converter == null) {
				Debug.WriteLine("No type converter found");
				value = (TValue)Convert.ChangeType(stringValue, typeof(TValue), CultureInfo.InvariantCulture);
			} else {
				value = (TValue)converter.ConvertFromInvariantString(stringValue);
			}
			return value;
		}

		public string Description {
			get {
				return description;
			}
		}

		public string Name {
			get {
				return name;
			}
		}

		public string PatternHelp {
			get {
				return patternHelp;
			}
		}

		public bool Optional {
			get {
				return optional;
			}
		}

		object ITagItem.ParseValue(string value) {
			return ParseValue(value);
		}
	}
}