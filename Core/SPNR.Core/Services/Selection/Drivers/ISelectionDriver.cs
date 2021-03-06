﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SPNR.Core.Models;
using SPNR.Core.Models.Works;

namespace SPNR.Core.Services.Selection.Drivers
{
    public interface ISelectionDriver
    {
        Task Initialize();
        Task<List<ScientificWork>> Search(SearchInfo info);
    }
}