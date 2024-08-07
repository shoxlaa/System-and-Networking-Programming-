using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using EncryptDecrypt.Services;

namespace EncryptDecrypt.Models
{
	[INotifyPropertyChanged]
	public partial class Model : ICloneable<Model>
	{
		[ObservableProperty]
		private int? _id;
		[ObservableProperty]
		private string? _name;
		[ObservableProperty]
		private int? _value;

		public Model Clone()
		{
			return new()
			{
				Id = _id,
				Name = _name,
				Value = _value,
			};
		}
	}
}
