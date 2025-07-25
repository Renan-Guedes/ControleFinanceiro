﻿using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class BancoUseCase : IBancoUseCase
{
    private readonly IBancoRepository _bancoRepository;

    public BancoUseCase(IBancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }
    
    public void Criar(BancoModel bancoModel) 
        => _bancoRepository.Criar(bancoModel);

    public void Atualizar(BancoModel bancoModel) 
        => _bancoRepository.Atualizar(bancoModel);
    
    public void Deletar(int bancoId, int usuarioId) 
        => _bancoRepository.Deletar(bancoId, usuarioId);
    
    public List<BancoModel> ListarTodos(int usuarioId) 
        => _bancoRepository.ListarTodos(usuarioId);

    public BancoModel? BuscarPorId(int bancoId, int usuarioId) 
        => _bancoRepository.BuscarPorId(bancoId, usuarioId);



}
