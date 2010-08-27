using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace bsn.CommandLine.Context {
	public class Tag<TExecutionContext, TValue>: ITagItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public delegate TValue GetDefaultDelegate(TExecutionContext executionContext) ;
		public delegate bool GetOptionalDelegate(TExecutionContext executionContext);

		private readonly string description;
		private readonly string name;
		private readonly string patternHelp;
		private GetDefaultDelegate getDefault;
		private GetOptionalDelegate getOptional;

		public Tag(string name, string description): this(name, description, null) {}

		public Tag(string name, string description, string patternHelp) {
			if (string.IsNullOrEmpty(name)) {
				throw new ArgumentNullException("name");
			}
			if (description == null) {
				throw new ArgumentNullException("description");
			}
			this.patternHelp = string.IsNullOrEmpty(patternHelp) ? string.Format("<{0}>", typeof(TValue).Name.ToLowerInvariant()) : patternHelp;
			this.name = name;
			this.description = description;
		}

		private Exception BuildError(string baseMessage, string stringValue, IEnumerable<TValue> values) {
			StringBuilder error = new StringBuilder();
			error.AppendFormat(baseMessage, stringValue);
			foreach (TValue potentialMatch in values) {
				error.AppendLine();
				error.AppendFormat(" {0}", potentialMatch);
			}
			return new InvalidOperationException(error.ToString());
		}

		public virtual TValue ParseValue(TExecutionContext executionContext, string stringValue) {
			TValue value;
			if (typeof(TValue).IsEnum) {
				List<TValue> potentialMatches = new List<TValue>();
				foreach (TValue item in Enum.GetValues(typeof(TValue))) {
					if (item.ToString().StartsWith(stringValue, StringComparison.OrdinalIgnoreCase)) {
						potentialMatches.Add(item);
					}
				}
				switch (potentialMatches.Count) {
				case 0:
					throw BuildError("The value '{0}' is not valid, possible values are:", stringValue, (IEnumerable<TValue>)Enum.GetValues(typeof(TValue)));
				case 1:
					value = potentialMatches[0];
					break;
				default:
					throw BuildError("The value '{0}' is ambiguous:", stringValue, potentialMatches);
				}
			} else {
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
				if (converter == null) {
					Debug.WriteLine("No type converter found");
					value = (TValue)Convert.ChangeType(stringValue, typeof(TValue), CultureInfo.InvariantCulture);
				} else {
					value = (TValue)converter.ConvertFromInvariantString(stringValue);
				}
			}
			return value;
		}

		public Tag<TExecutionContext, TValue> SetDefault(GetDefaultDelegate getDefault) {
			this.getDefault = getDefault;
			return this;
		}

		public Tag<TExecutionContext, TValue> SetOptional(GetOptionalDelegate getOptional) {
			this.getOptional = getOptional;
			return this;
		}

		bool ITagItem<TExecutionContext>.TryGetDefault(TExecutionContext executionContext, out object value) {
			if (getDefault != null) {
				value = getDefault(executionContext);
				return true;
			}
			value = null;
			return false;
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

		public bool GetOptional(TExecutionContext executionContext) {
			if (getOptional != null) {
				return getOptional(executionContext);
			}
			return false;
		}

		object ITagItem<TExecutionContext>.ParseValue(TExecutionContext executionContext, string stringValue) {
			return ParseValue(executionContext, stringValue);
		}
	}
}