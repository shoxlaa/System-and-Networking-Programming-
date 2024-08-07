using System.Collections.Generic;
using System.Linq;
using HTTP_protocol__project.ViewModels;

namespace HTTP_protocol__project.Services;

public class DowladingViewModelCreator : ViewModelCreator
{
    public override BaseViewModel Create(IEnumerable<BaseViewModel> viewModels)
    {
        return viewModels.ElementAt(0);
    }
}