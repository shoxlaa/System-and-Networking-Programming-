using System.Collections.Generic;
using System.Linq;

using EncryptDecrypt.ViewModels;

namespace EncryptDecrypt.Services
{
	public class MainViewModelCreator : ViewModelCreator
	{
		public override BaseViewModel Create(IEnumerable<BaseViewModel> viewModels)
		{
			return viewModels.ElementAt(1);
		}
	}
}
