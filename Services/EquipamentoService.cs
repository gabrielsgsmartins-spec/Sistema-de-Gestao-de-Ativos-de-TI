using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;

namespace SistemadeGestãodeAtivosdeTI.Services
{
    public class EquipamentoService
    {
        private readonly IManutencaoRepositorio _manutencaoRepositorio;
        private readonly IEquipamentoRepositorio _equipamentoRepositorio;

        public EquipamentoService(IEquipamentoRepositorio equipamentoRepositorio, IManutencaoRepositorio manutencaoRepositorio)
        {
            _equipamentoRepositorio = equipamentoRepositorio;
            _manutencaoRepositorio = manutencaoRepositorio;
        }

        public void Cadastrar(EquipamentoModel equipamento)
        {
            var equipamentoExistente = _equipamentoRepositorio.BuscarPorNumeroSerie(equipamento.NumeroSerie);

            if (equipamentoExistente != null)
            {
                throw new Exception("Já existe um equipamento com esse número de série.");
            }

            equipamento.Status = StatusEquipamentoEnum.Disponível;

            _equipamentoRepositorio.Adicionar(equipamento);
        }

        public void Atribuir(int equipamentoId, int funcionarioId)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status != StatusEquipamentoEnum.Disponível)
            {
                throw new Exception("O equipamento não está disponível para ser atribuído.");
            }

            if (equipamento.FuncionarioId != null)
            {
                throw new Exception("O equipamento já está atribuído a outro funcionário.");
            }

            equipamento.FuncionarioId = funcionarioId;
            equipamento.Status = StatusEquipamentoEnum.EmUso;

            _equipamentoRepositorio.Editar(equipamento);
        }

        public void Devolver(int equipamentoId)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status != StatusEquipamentoEnum.EmUso)
            {
                throw new Exception("Esse equipamento não está em uso.");
            }

            equipamento.FuncionarioId = null;
            equipamento.Status = StatusEquipamentoEnum.Disponível;

            _equipamentoRepositorio.Editar(equipamento);
        }

        public void EnviarParaManutencao(int equipamentoId)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status == StatusEquipamentoEnum.EmManutencao)
            {
                throw new Exception("O equipamento já está em manutenção.");
            }

            if (equipamento.FuncionarioId != null)
            {
                throw new Exception("O equipamento está atribuído a um funcionário.");
            }

            equipamento.Status = StatusEquipamentoEnum.EmManutencao;

            _equipamentoRepositorio.Editar(equipamento);
        }

        public void FinalizarManutencao(int equipamentoId)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status != StatusEquipamentoEnum.EmManutencao)
            {
                throw new Exception("O equipamento não está em manutenção.");
            }

            equipamento.Status = StatusEquipamentoEnum.Disponível;

            _equipamentoRepositorio.Editar(equipamento);
        }

        public void Excluir(int equipamentoId)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status == StatusEquipamentoEnum.EmUso)
            {
                throw new Exception("Não é possível excluir um equipamento em uso.");
            }

            if (equipamento.Status == StatusEquipamentoEnum.EmManutencao)
            {
                throw new Exception("Não é possível excluir um equipamento em manutenção.");
            }

            _equipamentoRepositorio.Excluir(equipamentoId);
        }

        public void Atualizar(EquipamentoModel equipamento)
        {
            var equipamentoExistente = _equipamentoRepositorio.BuscarPorId(equipamento.Id);

            if (equipamentoExistente == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            var outroEquipamentoComMesmoNumeroSerie = _equipamentoRepositorio.BuscarPorNumeroSerie(equipamento.NumeroSerie);

            if (outroEquipamentoComMesmoNumeroSerie != null && outroEquipamentoComMesmoNumeroSerie.Id != equipamento.Id)
            {
                throw new Exception("Já existe outro equipamento com esse número de série.");
            }

            equipamentoExistente.Marca = equipamento.Marca;
            equipamentoExistente.Modelo = equipamento.Modelo;
            equipamentoExistente.NumeroSerie = equipamento.NumeroSerie;

            _equipamentoRepositorio.Editar(equipamentoExistente);
        }
    }
}