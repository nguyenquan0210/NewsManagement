﻿using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog
{
    public class CatalogVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }

        public bool Hot { get; set; }


    }
}
