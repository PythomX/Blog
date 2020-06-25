﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models.ControleAcesso;
using Microsoft.AspNetCore.Mvc;
using PWABlog.RequestModels.ControleDeAcesso;
using PWABlog.ViewModels.ControleDeAcesso;

namespace Blog.ViewMoldels
{
    public class ControleDeAcessoController : Controller
    {
        
        private readonly ControleDeAcessoService controleDeAcessoService;

        public ControleDeAcessoController(
            ControleDeAcessoService controleDeAcessoService
        )
        {
            this.controleDeAcessoService = controleDeAcessoService;
        }


        [HttpGet]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            ControleDeAcessoLoginViewModel model = new ControleDeAcessoLoginViewModel();

            model.MensagemRegistro = (string) TempData["registrar-msg"];
            model.MensagemLogin = (string) TempData["login-msg"];
            model.Destino = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<RedirectResult> Login(ControleDeAcessoLoginRequestModel request)
        {
            var usuario = request.Usuario;
            var senha = request.Senha;
            var destinoAposSucessoNoLogin = request.Destino ?? "admin";

            var loginUrl = "acesso/login?ReturnUrl=" + request.Destino;

            if (usuario.Equals("")) {
                TempData["login-msg"] = "Por favor informe um nome de usuário";
                return Redirect(loginUrl);
            }

            if (senha.Equals("")) {
                TempData["login-msg"] = "Por favor informe uma senha";
                return Redirect(loginUrl);
            }

            try {
                await controleDeAcessoService.AutenticarUsuario(usuario, senha);
                return Redirect(destinoAposSucessoNoLogin);
            } catch (Exception exception) {
                TempData["login-msg"] = exception.Message;
                return Redirect(loginUrl);
            }
        }

        [HttpGet]
        public RedirectToActionResult Logout()
        {
            controleDeAcessoService.DeslogarUsuario();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            ControleDeAcessoRegistrarViewModel model = new ControleDeAcessoRegistrarViewModel();

            model.ErrosRegistro = (string[]) TempData["erros-registro"];
            model.Erro = (string) TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Registrar(ControleDeAcessoRegistrarRequestModel request)
        {
            var email = request.Email;
            var apelido = request.Apelido;
            var senha = request.Senha;
            var token = request.Token ?? "";

            if (apelido.Equals("")) {
                TempData["erro-msg"] = "Por favor informe um apelido";
                return RedirectToAction("Registrar");
            }

            if (email.Equals("")) {
                TempData["erro-msg"] = "Por favor informe um email";
                return RedirectToAction("Registrar");
            }

            if (senha.Equals("")) {
                TempData["erro-msg"] = "Por favor informe uma senha";
                return RedirectToAction("Registrar");
            }

            if (!token.Equals("BLOG-PWA-2020")) {
                TempData["erro-msg"] = "Token Inválido!";
                return RedirectToAction("Registrar");
            }

            try {
                await controleDeAcessoService.RegistrarUsuario(email, apelido, senha);

                TempData["registrar-msg"] = "Usuário Registrado!";
                return RedirectToAction("Login");
                
            } catch (RegistrarUsuarioException exception) {
                var listaDeErros = new List<string>();

                foreach (var erro in exception.Erros) {
                    listaDeErros.Add(erro.Description);
                }

                TempData["erros-registro"] = listaDeErros;

                return RedirectToAction("Registrar");
            }
        }
        
        
    }

    
}