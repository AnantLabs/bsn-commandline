using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

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

		public virtual TValue ParseValue(TExecutionContext executionContext, string stringValue) {
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