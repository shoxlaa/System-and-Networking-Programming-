using System.Collections.Generic;
using HTTP_protocol__project.ViewModels;

namespace HTTP_protocol__project.Services;

public abstract class ViewModelCreator
{
    public abstract BaseViewModel Create(IEnumerable<BaseViewModel> viewModels);
}