using EncryptDecryptMvvm.ViewModels;
using System;

namespace EncryptDecryptMvvm.Services
{
    public class ViewModelFactory
    {
        public IBaseViewModel Create(ViewModelType type)
        {
            return type switch
            {
                ViewModelType.StartViewModel => App.p_container.GetInstance<StartViewModel>(),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
    }

}
