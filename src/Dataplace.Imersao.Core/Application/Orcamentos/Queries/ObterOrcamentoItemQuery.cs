﻿using Dataplace.Core.Domain.Query;
using Dataplace.Imersao.Core.Application.Orcamentos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Application.Orcamentos.Queries
{
    internal class ObterOrcamentoItemQuery : QueryRefeshItem<OrcamentoItemViewModel>, IQueryRefeshItem<OrcamentoItemViewModel>
    {
        public int NumOrcamento { get; set; }
    }
}
