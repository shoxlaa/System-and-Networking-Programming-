using System.Collections.Generic;
using EncryptDecrypt.ViewModels;
namespace EncryptDecrypt.Services
{
	public class ViewModelFactory
	{
		private readonly IEnumerable<BaseViewModel> _viewModels;

		public ViewModelFactory(IEnumerable<BaseViewModel> viewModels)
		{
			_viewModels = viewModels;
		}

		public BaseViewModel Create(ViewModelCreator creator)
		{
			return creator.Create(_viewModels);
		}
	}
}
