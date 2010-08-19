using System;

namespace bsn.CommandLine {
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class NamedItemAttribute: Attribute {
		private readonly string description;
		private readonly string name;
		private string remarks;

		public NamedItemAttribute(string name, string description) {
			if (string.IsNullOrEmpty(name)) {
				throw new ArgumentNullException("name");
			}
			if (description == null) {
				throw new ArgumentNullException("description");
			}
			this.name = name;
			this.description = description;
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

		public string Remarks {
			get {
				return remarks;
			}
			set {
				remarks = value;
			}
		}
	}
}