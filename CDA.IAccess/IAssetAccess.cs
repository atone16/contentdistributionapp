﻿using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IAccess
{
    public interface IAssetAccess : IBaseAccess<Asset>
    {
        Task<Asset> GetByAssetId(string assetId);
    }
}
