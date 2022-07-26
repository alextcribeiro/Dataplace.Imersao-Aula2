using Dapper;
using Dataplace.Imersao.Core.Application.Orcamentos.ViewModels;
using dpLibrary05;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Application.Orcamentos.Queries
{
    internal class OrcamentoItemQueryHandler : IRequestHandler<ObterOrcamentoItemQuery, OrcamentoItemViewModel>
    { 
    private readonly IDataAccess _dataAccess;
    private readonly Domain.Orcamentos.Repositories.IOrcamentoRepository _orcamentoRepository;

        public OrcamentoItemQueryHandler(Domain.Orcamentos.Repositories.IOrcamentoRepository orcamentoRepository, IDataAccess dataAccess)
        {
            _orcamentoRepository = orcamentoRepository;
            _dataAccess = dataAccess;
        }

        public async Task<OrcamentoItemViewModel> Handle(ObterOrcamentoItemQuery query, CancellationToken cancellationToken)
        {
            var sql = $@"
            SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
            SELECT 
            OrcamentoItem.CdEmpresa,
            OrcamentoItem.CdFilial,
            OrcamentoItem.NumOrcamento,
            OrcamentoItem.Seq,
            OrcamentoItem.TpRegistro,
            OrcamentoItem.CdProduto,
            OrcamentoItem.DsProduto,
            OrcamentoItem.Quantidade,
            OrcamentoItem.PrecoTabela,
            OrcamentoItem.PercAltPreco,
            OrcamentoItem.PrecoVenda,
            OrcamentoItem.Total,
            OrcamentoItem.Status
            FROM OrcamentoItem
            /**where**/	
            ";

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(sql);


            builder.Where("orcamento.NumOrcamento = @NumOrcamento", new { query.NumOrcamento });
            var cmd = new CommandDefinition(selector.RawSql, selector.Parameters, flags: CommandFlags.NoCache);
            return _dataAccess.Connection.QueryFirstOrDefault<OrcamentoItemViewModel>(cmd);


        }
    }
}
