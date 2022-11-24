using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceCase.Domain.Enums
{
    public enum MaintenanceType
    {
        AtualizarComentarioJira, 
        EnviarSolicitacaoAx, 
        AtualizarStatusSolicitacaoJira,
        EnviarArquivoAnexoAx,
        AtualizarKmAtualAx, 
        CriaDadosAtendimento,
        CriaDadosAtendimentoApp
    }
}
