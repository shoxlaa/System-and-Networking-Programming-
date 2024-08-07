using System.Collections.Generic;
using HTTP_protocol__project.ViewModels;

namespace HTTP_protocol__project.Services;

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