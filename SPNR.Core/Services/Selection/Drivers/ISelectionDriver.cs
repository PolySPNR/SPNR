﻿using System.Threading.Tasks;
using SPNR.Core.Models;

namespace SPNR.Core.Services.Selection.Drivers
{
    public interface ISelectionDriver
    {
        Task<string> Search(SearchInfo info);
    }
}