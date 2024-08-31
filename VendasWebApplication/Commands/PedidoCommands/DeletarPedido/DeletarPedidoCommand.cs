﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebApplication.Commands.PedidoCommands.DeletarPedido
{
    public class DeletarPedidoCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
