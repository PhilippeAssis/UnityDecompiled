using System;

namespace Unity.IL2CPP.Metadata.Fields
{
	public class FieldDefaultValue
	{
		public readonly int _fieldIndex;

		public readonly int _typeIndex;

		public readonly int _dataIndex;

		public FieldDefaultValue(int fieldIndex, int typeIndex, int dataIndex)
		{
			this._fieldIndex = fieldIndex;
			this._typeIndex = typeIndex;
			this._dataIndex = dataIndex;
		}

		public override string ToString()
		{
			return string.Format("{{ {0}, {1}, {2} }}", this._fieldIndex, this._typeIndex, this._dataIndex);
		}
	}
}
