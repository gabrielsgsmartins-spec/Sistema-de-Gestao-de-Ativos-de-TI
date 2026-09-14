using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGestãodeAtivosdeTI.Enums;
using SistemadeGestãodeAtivosdeTI.Models;
using SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces;
using SistemaGestaoAtivos.Models;

namespace SistemadeGestãodeAtivosdeTI.Repositorios.Interfaces
{
    public class EquipamentoService
    {
        private readonly IEquipamentoRepositorio _equipamentoRepositorio;

        public EquipamentoService(IEquipamentoRepositorio equipamentoRepositorio)
        {
            _equipamentoRepositorio = equipamentoRepositorio;
        }

        // CADASTRAR
        public void Cadastrar(EquipamentoModel equipamento)
        {
            // Verificar se o número de série já existe
            var equipamentoExistente =
                _equipamentoRepositorio.BuscarPorNumeroSerie(equipamento.NumeroSerie);

            if (equipamentoExistente != null)
            {
                throw new Exception("Já existe um equipamento com esse número de série.");
            }

            // Equipamento novo começa como disponível
            equipamento.Status = StatusEquipamentoEnum.Disponível;

            _equipamentoRepositorio.Adicionar(equipamento);
        }


        // ATRIBUIR EQUIPAMENTO PARA FUNCIONÁRIO
        public void Atribuir(int equipamentoId, int funcionarioId)
        {
            var equipamento =
                _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            // Só pode atribuir equipamento disponível
            if (equipamento.Status != StatusEquipamentoEnum.Disponível)
            {
                throw new Exception(
                    "O equipamento não está disponível para ser atribuído.");
            }

            // Aqui você coloca a propriedade que liga
            // o equipamento ao funcionário
            equipamento.FuncionarioId = funcionarioId;

            equipamento.Status = StatusEquipamentoEnum.EmUso;

            _equipamentoRepositorio.Editar(equipamento);
        }


        // DEVolver equipamednto
        public void Devolver(int equipamentoId)
        {
            var equipamento =
                _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status != StatusEquipamentoEnum.EmUso)
            {
                throw new Exception(
                    "Esse equipamento não está em uso.");
            }

            // Remove o funcionário qu ta reponsavel pelo equipamento
            equipamento.FuncionarioId = null;

            equipamento.Status = StatusEquipamentoEnum.Disponível;

            _equipamentoRepositorio.Editar(equipamento);
        }


        // ENVIAR PARA MANUTENÇÃO
        public void EnviarParaManutencao(int equipamentoId)
        {
            var equipamento =
                _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status == StatusEquipamentoEnum.EmManutencao)
            {
                throw new Exception(
                    "O equipamento já está em manutenção.");
            }

            // Não pode mandar um equipamento atribuído
            // diretamente para manutenção
            if (equipamento.FuncionarioId != null)
            {
                throw new Exception(
                    "O equipamento está atribuído a um funcionário.");
            }

            equipamento.Status =
                StatusEquipamentoEnum.EmManutencao;

            _equipamentoRepositorio.Editar(equipamento);
        }


        // FINALIZAR MANUTENÇÃO
        public void FinalizarManutencao(int equipamentoId)
        {
            var equipamento =
                _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.Status != StatusEquipamentoEnum.EmManutencao)
            {
                throw new Exception(
                    "O equipamento não está em manutenção.");
            }

            equipamento.Status =
                StatusEquipamentoEnum.Disponível;

            _equipamentoRepositorio.Editar(equipamento);
        }


        // EXCLUIR
        public void Excluir(int equipamentoId)
        {
            var equipamento = _equipamentoRepositorio.BuscarPorId(equipamentoId);

            if (equipamento == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }

            // Não pode excluir equipamento em uso
            if (equipamento.Status == StatusEquipamentoEnum.EmUso)
            {
                throw new Exception("Não é possível excluir um equipamento em uso.");
            }

            // Não pode excluir equipamento em manutenção
            if (equipamento.Status == StatusEquipamentoEnum.EmManutencao)
            {
                throw new Exception("Não é possível excluir um equipamento em manutenção.");
            }

            // Passa o ID do equipamento (int) em vez do objeto inteiro
            _equipamentoRepositorio.Excluir(equipamentoId);
        }


        //ATUALIZAR

        public void Atualizar(EquipamentoModel equipamento)
        {
            var equipamentoExistente =
                _equipamentoRepositorio.BuscarPorId(equipamento.Id);
            if (equipamentoExistente == null)
            {
                throw new Exception("Equipamento não encontrado.");
            }
            // Verifico se o número de série já existe em outro equipamento
            var outroEquipamentoComMesmoNumeroSerie =
                _equipamentoRepositorio.BuscarPorNumeroSerie(equipamento.NumeroSerie);
            if (outroEquipamentoComMesmoNumeroSerie != null &&
                outroEquipamentoComMesmoNumeroSerie.Id != equipamento.Id)
            {
                throw new Exception("Já existe outro equipamento com esse número de série.");
            }
            // Atualiza os campos do equipamento existente
            equipamentoExistente.Marca = equipamento.Marca;
            equipamentoExistente.Modelo = equipamento.Modelo;
            equipamentoExistente.NumeroSerie = equipamento.NumeroSerie;
            _equipamentoRepositorio.Editar(equipamentoExistente);
        }
    }
}