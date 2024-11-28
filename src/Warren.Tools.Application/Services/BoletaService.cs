using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Com.Warren.Services.Response;
using Warren.Tools.Application.Util;
using Warren.Tools.Domain.DTO.Request;
using Warren.Tools.Domain.DTO.Response;
using Warren.Tools.Domain.Entities;
using Warren.Tools.Domain.Interfaces.Repositories;
using Warren.Tools.Domain.Interfaces.Services;
using Warren.Tools.Domain.Models;
using Warren.Tools.Domain.Services;

namespace Warren.Tools.Application.Services
{
    public class BoletaService : IBoletaService
    {
        private readonly IBoletaRepository _boletaRepository;
        private readonly IPuRepository _puRepository;
        private IMapper _mapper;
        private readonly IRoboService _roboService;

        public BoletaService(IBoletaRepository boletaRepository, IMapper mapper, IPuRepository puRepository, IRoboService roboService)
        {
            _boletaRepository = boletaRepository;
            _puRepository = puRepository;
            _mapper = mapper;
            _roboService = roboService;
        }

        public async Task<List<BoletaEntity>> GetAllBoletas() //boletas de compra intradias
        {
            var dataInicio = DateTime.Now.AddDays(-1).Date;
            var dataFim = DateTime.Now;
            var dataInicioUtil = DateUtil.DataValida(dataInicio);
            var dataFimUtil = DateUtil.DataValida(dataFim);

            var boletaList = await _boletaRepository.GetAll(dataInicioUtil, dataFimUtil);

            var boletasNaoLiquidadas = boletaList
                .Where(b => b.Cancelled == false && b.Debit == false && b.Liquidated == false
                        && !b.AssociationCommand.Equals("")
                        && (b.IdMessage != 6 && b.IdMessage != 7)
                        && (b.SellerSelicAccount == "071200309" || b.BuyerSelicAccount == "071200309")).ToList();
            
            return boletasNaoLiquidadas;
        }

        public async Task<NecessidadeCaixaResponse> CalculoNecessidadeCaixaTodasBoletas()
        {
            var todasBoletasIntradias = await GetAllBoletas();
            
            var response =  await CalculoNecessidadeCaixaInterno(todasBoletasIntradias);
            
            return response;
        }

        public async Task<NecessidadeCaixaResponse> CalculoNecessidadeCaixaPorLista(List<BoletasIds> request)
        {
            var todasBoletasIntradias = await GetAllBoletas();

            List<BoletaEntity> boletasSelecionadas = new List<BoletaEntity>();

            if (todasBoletasIntradias != null)
            {
                foreach (var boleta in todasBoletasIntradias)
                {
                    foreach (var boletaId in request)
                    {
                        if (boleta.IdBoleta == boletaId.IdBoleta)
                        {
                            boletasSelecionadas.Add(boleta);
                        }
                    }
                }
            }

            return await CalculoNecessidadeCaixaInterno(boletasSelecionadas);
        }

        private async Task<NecessidadeCaixaResponse> CalculoNecessidadeCaixaInterno(List<BoletaEntity> todasBoletasIntradias)
        {
            try
            {
                var now = DateTime.Now;
                var dataHoje = DateUtil.DataValida(now);
                var startOfYesterday = now.AddDays(-1).Date;
                var dataOntem = DateUtil.DataValida(startOfYesterday);

                var pu550List = await _puRepository.GetAll(dataOntem, dataHoje);

                var valorCaixaJd = _roboService.IniciarRobo();
                var valorFormatado = valorCaixaJd.Replace(".", "").Replace(",",".");
                var valorCaixaAtual = double.Parse(valorFormatado, CultureInfo.InvariantCulture);

                //var valorCaixaAtual = 0.0;
                var valorAPagar = 0.0;
                var porcentagem = 0.60;
                var porcentagemConsumida = 0.0;

                foreach (var boleta in todasBoletasIntradias)
                {
                    try
                    {
                        var bondCodeBoleta = int.Parse(boleta.BondCode);
                        var dateLiquidation = boleta.DateLiquidation;
                        var maturityBoleta = boleta.BondMaturity;
                        var quantityBoleta = int.Parse(boleta.BondQuantity);
                        var unitPriceBoleta = double.Parse(boleta.UnitPrice);

                        if (pu550List != null)
                        {
                            foreach (var pu550 in pu550List)
                            {
                                
                                if (bondCodeBoleta.Equals(pu550.BondCode) &&
                                    dateLiquidation.Equals(pu550.MovementDate) &&
                                    maturityBoleta.Equals(pu550.Maturity))
                                {
                                    double unitPricePu550 = pu550.UnitPrice;
                                    double calculatedAmount = (unitPricePu550 - unitPriceBoleta) * quantityBoleta;
                                    calculatedAmount = Math.Abs(calculatedAmount);

                                    valorAPagar += calculatedAmount;

                                    Console.WriteLine(valorAPagar);
                                }
                            }
                        }
                    }
                    catch (FormatException e)
                    {
                        throw new InvalidDataException("Erro ao converter valores da boleta: " + boleta.IdBoleta, e);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex);
                    }
                }

                porcentagemConsumida = (Math.Abs(valorAPagar) / valorCaixaAtual);
                
                var response = new NecessidadeCaixaResponse(
                    decimal.Round((decimal)valorCaixaAtual, 2, MidpointRounding.AwayFromZero),
                    decimal.Round((decimal)valorAPagar, 2, MidpointRounding.AwayFromZero),
                    porcentagemConsumida , 
                    porcentagem 
                );

                return (response);
            }
            //catch (DataNotFoundException e)
            //{
            //    return new NotFoundObjectResult(new NecessidadeCaixaResponse("Erro: " + e.Message));
            //}
            //catch (InvalidDataException e)
            //{
            //    return new BadRequestObjectResult(new NecessidadeCaixaResponse("Erro: " + e.Message));
            //}
            catch (Exception e)
            {
                return new NecessidadeCaixaResponse("Erro interno no cálculo da necessidade de caixa.");                
            }
        }

        //public async Task<List<Pu550Entity>> GetAllPu()
        //{
        //    var dataInicio = DateTime.Now.AddDays(-1).Date;
        //    var dataFim = DateTime.Now;

        //    var dataInicioUtil = DateUtil.DataValida(dataInicio);
        //    var dataFimUtil = DateUtil.DataValida(dataFim);

        //    var boletaList = await _puRepository.GetAll(dataInicioUtil, dataFimUtil);

        //    return (List<Pu550Entity>)boletaList;
        //}
    }
}
