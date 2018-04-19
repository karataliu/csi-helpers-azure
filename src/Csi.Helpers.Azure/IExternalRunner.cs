﻿using System.Threading.Tasks;

namespace Csi.Helpers.Azure
{
    public interface IExternalRunner
    {
        Task RunExecutable(string path, params string[] arguments);
    }
}
