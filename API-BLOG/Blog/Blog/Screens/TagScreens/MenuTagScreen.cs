using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Screens.TagScreens
{
    public static class MenuTagScreen
    {
        public static void Load()
        {
            Clear();
            WriteLine("Gestâo de tags");
            WriteLine("--------------");
            WriteLine("O que deseja fazer?");
            WriteLine();
            WriteLine("1 - Listar tags");
            WriteLine("2 - Cadastrar tags");
            WriteLine("2 - Atualizar tags");
            WriteLine("2 - Excluir tags");
            WriteLine();
            WriteLine();
            var option = short.Parse(ReadLine());

            switch (option)
            {
                case 1:
                    ListTagsScreen.Load();
                    break;
                case 2:
                    UpdateTagScreen.Load();
                    break;
                case 3:
                    CreateTagScreen.Load();
                    break;
                case 4:
                    DeleteTagScreen.Load();
                    break;
                default:
                    break;
            }
        }
    }
}
