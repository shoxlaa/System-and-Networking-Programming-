using System.Collections.Generic;
using EncryptDecrypt.ViewModels;

namespace EncryptDecrypt.Services
{
	public abstract class ViewModelCreator
	{
		public abstract BaseViewModel Create(IEnumerable<BaseViewModel> viewModels);
	}
}
