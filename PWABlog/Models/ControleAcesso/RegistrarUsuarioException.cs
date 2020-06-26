using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PWABlog.Models.ControleAcesso
{
    public class RegistrarUsuarioException : Exception
    {
        
        public IEnumerable<IdentityError> Erros { get; set; }
        
        public RegistrarUsuarioException(IEnumerable<IdentityError> erros)
        {
            Erros = erros;
        }
        
    }
}