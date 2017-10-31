using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.CoreShared;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Core;
using GestaoBuilder.Core.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoBuilder_WebCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ModuloController : Controller
    {
        private readonly IModuloServiceCore service;
        private readonly ICoreSupportService core;

        public ModuloController(IModuloServiceCore service, ICoreSupportService core)
        {
            this.service = service;
            this.core = core;
        }

        [HttpPost]
        [Route("processar_async")]
        public async Task<IActionResult> ProcessarChamadaAsync([FromQuery]string pagina, [FromQuery]string componente, [FromQuery]string modulo, [FromBody]JToken token)
        {
            core.AddUsuarioLogado(new Usuario
            {
                Codigo = "S0001",
                Nome = "",
                SegundosLoginExpirar = 1000,
                EmpresaEmUSo = new Empresa
                {
                    ConnectionStringDb = "mongodb://localhost:27017",
                    DatabaseName = "GestaoSocial"
                }
            });
            core.AddUsuarioEntry(core.UsuarioLogado);
            var result = await service.ExecutarModuloCore(modulo, new Entrada(token, core.UsuarioEntry, TipoOperacaoEnum.Novo));
            if (result.HasError)
            {
                foreach (var item in result.Messages)
                {
                    ModelState.AddModelError(item.ChaveMessage, item.Message);
                }
                return BadRequest(ModelState);
            }
            return Ok(result.ResultJson());
        }
    }
}
