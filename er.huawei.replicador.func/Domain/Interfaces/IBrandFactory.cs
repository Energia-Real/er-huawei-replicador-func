using System;

namespace er.huawei.replicador.func.Domain.Interfaces
{
    public interface IBrandFactory
    {
        IBrand Create(string brand);
    }
}
