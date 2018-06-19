using System.Threading.Tasks;
using XF.Contatos.Models;

namespace XF.Contatos.Global
{
    public interface IContatoHelper
    {
        Task<bool> GetContatoListAsync();

        bool LigarParaContato(Contato contato);
    }
}
